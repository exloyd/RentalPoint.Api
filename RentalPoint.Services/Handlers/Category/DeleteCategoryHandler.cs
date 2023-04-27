using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Services.Commands;

namespace RentalPoint.Services.Handlers.Category
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCommand<Data.Entities.Category>, Guid>
    {
        private readonly RentalPointContext _context;

        public DeleteCategoryHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(DeleteCommand<Data.Entities.Category> request, CancellationToken cancellationToken)
        {
            var category = await _context.Category.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (category != null)
            {
                _context.Remove(category);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return category?.Id ?? Guid.Empty;
        }
    }
}