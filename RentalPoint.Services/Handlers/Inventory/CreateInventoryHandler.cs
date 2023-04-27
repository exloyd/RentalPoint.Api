using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Services.Commands;

namespace RentalPoint.Services.Handlers.Inventory
{
    public class CreateInventoryHandler : IRequestHandler<CreateCommand<Data.Entities.Inventory>, Guid>
    {
        private readonly RentalPointContext _context;

        public CreateInventoryHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateCommand<Data.Entities.Inventory> request, CancellationToken cancellationToken)
        {
            await _context.Inventory.AddAsync(request.Entity, cancellationToken);
            
            foreach (var category in request.Entity.Categories.ToList())
            {
                var hasSelectedCategory = await _context.Category.AnyAsync(x => x.Id == category.CategoryId, cancellationToken);
                if (hasSelectedCategory == false)
                    request.Entity.Categories.Remove(category);
            }
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return request.Entity.Id;
        }
    }
}