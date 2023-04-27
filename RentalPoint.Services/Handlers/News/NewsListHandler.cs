using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Services.Queries;

namespace RentalPoint.Services.Handlers.News
{
    public class NewsListHandler : IRequestHandler<GetListQuery<IEnumerable<Data.Entities.News>>, IEnumerable<Data.Entities.News>>
    {
        private readonly RentalPointContext _context;
        
        public NewsListHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Data.Entities.News>> Handle(GetListQuery<IEnumerable<Data.Entities.News>> request, 
            CancellationToken cancellationToken)
        {
            return await _context.News
                .Include(x => x.User)
                .OrderByDescending(x => x.Date)
                .ToListAsync(cancellationToken);
        }
    }
}