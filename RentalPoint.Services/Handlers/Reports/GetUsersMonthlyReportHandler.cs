using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Data.Models;
using RentalPoint.Services.Queries;

namespace RentalPoint.Services.Handlers.Reports
{
    public class GetUsersMonthlyReportHandler : IRequestHandler<
        GetMonthlyReportQuery<IEnumerable<MonthlyReport<Data.Entities.User>>>,
        IEnumerable<MonthlyReport<Data.Entities.User>>>
    {
        private readonly RentalPointContext _context;
        
        public GetUsersMonthlyReportHandler(RentalPointContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<MonthlyReport<Data.Entities.User>>> Handle(
            GetMonthlyReportQuery<IEnumerable<MonthlyReport<Data.Entities.User>>> request,
            CancellationToken cancellationToken)
        {
            var orders = await _context.User 
                .Where(x => x.Date.Year == DateTime.Now.Year)
                .GroupBy(x => x.Date.Month)
                .OrderBy(x => x.Key)
                .Select(x => new MonthlyReport<Data.Entities.User>
                {
                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(x.Key),
                    MonthNumber = x.Key,
                    Count = x.Count()
                })
                .ToListAsync(cancellationToken);
            
            var missedMonths = Enumerable.Range(1, 12).Except(orders.Select(x => x.MonthNumber).ToList());
            orders.AddRange(missedMonths.Select(month => new MonthlyReport<Data.Entities.User>
            {
                MonthNumber = month,
                Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month),
                Count = 0
            }));

            return orders.OrderBy(x => x.MonthNumber);
        }
    }
}