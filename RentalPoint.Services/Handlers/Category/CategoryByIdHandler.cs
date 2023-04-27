using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Services.Queries;

namespace RentalPoint.Services.Handlers.Category
{
    public class CategoryByIdHandler : IRequestHandler<GetByIdQuery<Data.Entities.Category>, Data.Entities.Category>
    {
        private readonly RentalPointContext _context;

        public CategoryByIdHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task<Data.Entities.Category> Handle(GetByIdQuery<Data.Entities.Category> request, CancellationToken cancellationToken)
        {
            return await _context.Category.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}