using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Services.Queries;

namespace RentalPoint.Services.Handlers.Inventory
{
    public class InventoryListHandler : IRequestHandler<GetListQuery<IEnumerable<Data.Entities.Inventory>>, IEnumerable<Data.Entities.Inventory>>
    {
        private readonly RentalPointContext _context;
        
        public InventoryListHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Data.Entities.Inventory>> Handle(GetListQuery<IEnumerable<Data.Entities.Inventory>> request, 
            CancellationToken cancellationToken)
        {
            return await _context.Inventory
                .Include(x => x.Categories)
                .ToListAsync(cancellationToken);
        }
    }
}