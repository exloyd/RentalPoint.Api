using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Services.Commands;

namespace RentalPoint.Services.Handlers.Inventory
{
    public class DeleteInventoryHandler : IRequestHandler<DeleteCommand<Data.Entities.Inventory>, Guid>
    {
        private readonly RentalPointContext _context;

        public DeleteInventoryHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(DeleteCommand<Data.Entities.Inventory> request, CancellationToken cancellationToken)
        {
            var item = await _context.Inventory.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (item != null)
            {
                _context.Remove(item);
                await _context.SaveChangesAsync(cancellationToken);
            }
            
            return item?.Id ?? Guid.Empty;
        }
    }
}