using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Services.Queries;

namespace RentalPoint.Services.Handlers.Order
{
    public class OrderListHandler : IRequestHandler<GetListQuery<IEnumerable<Data.Entities.Order>>, IEnumerable<Data.Entities.Order>>
    {
        private readonly RentalPointContext _context;
        
        public OrderListHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Data.Entities.Order>> Handle(GetListQuery<IEnumerable<Data.Entities.Order>> request, 
            CancellationToken cancellationToken)
        {
            return await _context.Orders
                .Include(x => x.User)
                .Include(x => x.InventoryOrders)
                .ThenInclude(x => x.Inventory)
                .OrderByDescending(x => x.Date)
                .ToListAsync(cancellationToken);
        }
    }
}