using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Services.Queries;

namespace RentalPoint.Services.Handlers.Promo
{
    public class PromoListHandler : IRequestHandler<GetListQuery<IEnumerable<Data.Entities.Promo>>, IEnumerable<Data.Entities.Promo>>
    {
        private readonly RentalPointContext _context;
        
        public PromoListHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Data.Entities.Promo>> Handle(GetListQuery<IEnumerable<Data.Entities.Promo>> request, 
            CancellationToken cancellationToken)
        {
            return await _context.Promo
                .OrderByDescending(x => x.BestBefore)
                .ToListAsync(cancellationToken);
        }
    }
}