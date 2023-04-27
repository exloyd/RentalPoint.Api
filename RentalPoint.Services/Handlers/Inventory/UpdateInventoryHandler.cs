using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Data.Entities;
using RentalPoint.Services.Commands;

namespace RentalPoint.Services.Handlers.Inventory
{
    public class UpdateInventoryHandler : IRequestHandler<UpdateCommand<Data.Entities.Inventory>>
    {
        private readonly RentalPointContext _context;

        public UpdateInventoryHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateCommand<Data.Entities.Inventory> request, CancellationToken cancellationToken)
        {
            var item = await _context.Inventory.FirstOrDefaultAsync(x => x.Id == request.Entity.Id, cancellationToken);
            if (item != null)
            {
                item.Name = request.Entity.Name;
                item.Description = request.Entity.Description;
                item.Image = request.Entity.Image;
                item.Price = request.Entity.Price;
                item.Count = request.Entity.Count;
                
                var categories = _context.InventoryCategory.Where(x => x.InventoryId == item.Id);
                _context.InventoryCategory.RemoveRange(categories);

                foreach (var category in request.Entity.Categories.ToList())
                {
                    var hasSelectedCategory = await _context.Category.AnyAsync(x => x.Id == category.CategoryId, cancellationToken);
                    if (hasSelectedCategory == false)
                    {
                        request.Entity.Categories.Remove(category);
                        continue;
                    }

                    await _context.InventoryCategory.AddAsync(new InventoryCategory
                    {
                        Id = Guid.NewGuid(),
                        CategoryId = category.CategoryId,
                        InventoryId = item.Id
                    }, cancellationToken);
                }
                
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}