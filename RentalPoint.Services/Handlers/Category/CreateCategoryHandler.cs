using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RentalPoint.Data;
using RentalPoint.Services.Commands;

namespace RentalPoint.Services.Handlers.Category
{
    public class CreateCategoryHandler : IRequestHandler<CreateCommand<Data.Entities.Category>, Guid>
    {
        private readonly RentalPointContext _context;

        public CreateCategoryHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateCommand<Data.Entities.Category> request, CancellationToken cancellationToken)
        {
            await _context.Category.AddAsync(request.Entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return request.Entity.Id;
        }
    }
}