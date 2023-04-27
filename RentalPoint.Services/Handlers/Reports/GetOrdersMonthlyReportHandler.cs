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
    public class GetOrdersMonthlyReportHandler : IRequestHandler<
        GetMonthlyReportQuery<IEnumerable<MonthlyReport<Data.Entities.Order>>>,
        IEnumerable<MonthlyReport<Data.Entities.Order>>>
    {
        private readonly RentalPointContext _context;
        
        public GetOrdersMonthlyReportHandler(RentalPointContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<MonthlyReport<Data.Entities.Order>>> Handle(
            GetMonthlyReportQuery<IEnumerable<MonthlyReport<Data.Entities.Order>>> request,
            CancellationToken cancellationToken)
        {
            var orders = await _context.Orders 
                .Where(x => x.Date.Year == DateTime.Now.Year)
                .GroupBy(x => x.Date.Month)
                .OrderBy(x => x.Key)
                .Select(x => new MonthlyReport<Data.Entities.Order>
                {
                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(x.Key),
                    MonthNumber = x.Key,
                    Count = x.Count()
                })
                .ToListAsync(cancellationToken);
            
            var missedMonths = Enumerable.Range(1, 12).Except(orders.Select(x => x.MonthNumber).ToList());
            orders.AddRange(missedMonths.Select(month => new MonthlyReport<Data.Entities.Order>
            {
                MonthNumber = month,
                Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month),
                Count = 0
            }));

            return orders.OrderBy(x => x.MonthNumber);
        }
    }
}