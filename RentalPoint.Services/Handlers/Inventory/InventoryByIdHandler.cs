using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Services.Queries;

namespace RentalPoint.Services.Handlers.Inventory
{
    public class InventoryByIdHandler : IRequestHandler<GetByIdQuery<Data.Entities.Inventory>, Data.Entities.Inventory>
    {
        private readonly RentalPointContext _context;

        public InventoryByIdHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task<Data.Entities.Inventory> Handle(GetByIdQuery<Data.Entities.Inventory> request, CancellationToken cancellationToken)
        {
            return await _context.Inventory
                .Include(x => x.Categories)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}