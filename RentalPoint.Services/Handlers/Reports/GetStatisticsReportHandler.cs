using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Data.Models;
using RentalPoint.Services.Extensions;
using RentalPoint.Services.Queries;

namespace RentalPoint.Services.Handlers.Reports
{
    public class GetStatisticsReportHandler : IRequestHandler<GetListQuery<IEnumerable<StatisticsReport>>,
        IEnumerable<StatisticsReport>>
    {
        private readonly RentalPointContext _context;

        public GetStatisticsReportHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StatisticsReport>> Handle(GetListQuery<IEnumerable<StatisticsReport>> request,
            CancellationToken cancellationToken)
        {
            var currentMonth = DateTime.Now.Month;

            return new List<StatisticsReport>
            {
                new StatisticsReport
                {
                    PreviousMonth = await _context.Orders.CountAsync(x => x.Date.Month == currentMonth - 1, cancellationToken),
                    CurrentMonth = await _context.Orders.CountAsync(x => x.Date.Month == currentMonth, cancellationToken),
                    Total = await _context.Orders.CountAsync(cancellationToken),
                    Type = StatisticsReportType.Orders.GetDescription()
                },
                new StatisticsReport
                {
                    PreviousMonth = await _context.User.CountAsync(x => x.Date.Month == currentMonth - 1, cancellationToken),
                    CurrentMonth = await _context.User.CountAsync(x => x.Date.Month == currentMonth, cancellationToken),
                    Total = await _context.User.CountAsync(cancellationToken),
                    Type = StatisticsReportType.Users.GetDescription()
                },
                new StatisticsReport
                {
                    PreviousMonth = await _context.Promo.CountAsync(x => x.Date.Month == currentMonth - 1, cancellationToken),
                    CurrentMonth = await _context.Promo.CountAsync(x => x.Date.Month == currentMonth, cancellationToken),
                    Total = await _context.Promo.CountAsync(cancellationToken),
                    Type = StatisticsReportType.Promos.GetDescription()
                },
                new StatisticsReport
                {
                    PreviousMonth = await _context.Penalty.CountAsync(x => x.Date.Month == currentMonth - 1, cancellationToken),
                    CurrentMonth = await _context.Penalty.CountAsync(x => x.Date.Month == currentMonth, cancellationToken),
                    Total = await _context.Penalty.CountAsync(cancellationToken),
                    Type = StatisticsReportType.Penalties.GetDescription()
                }
            };
        }
    }
}