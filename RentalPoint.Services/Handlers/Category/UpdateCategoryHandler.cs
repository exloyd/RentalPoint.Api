using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Services.Commands;

namespace RentalPoint.Services.Handlers.Category
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCommand<Data.Entities.Category>>
    {
        private readonly RentalPointContext _context;

        public UpdateCategoryHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateCommand<Data.Entities.Category> request, CancellationToken cancellationToken)
        {
            var category = await _context.Category.FirstOrDefaultAsync(x => x.Id == request.Entity.Id, cancellationToken);
            if (category != null)
            {
                category.Name = request.Entity.Name;
                category.Image = request.Entity.Image;
                
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}