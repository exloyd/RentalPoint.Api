using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Services.Queries;

namespace RentalPoint.Services.Handlers.Category
{
    public class CategoryListHandler : IRequestHandler<GetListQuery<IEnumerable<Data.Entities.Category>>, IEnumerable<Data.Entities.Category>>
    {
        private readonly RentalPointContext _context;
        
        public CategoryListHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Data.Entities.Category>> Handle(GetListQuery<IEnumerable<Data.Entities.Category>> request, 
            CancellationToken cancellationToken)
        {
            return await _context.Category.ToListAsync(cancellationToken);
        }
    }
}