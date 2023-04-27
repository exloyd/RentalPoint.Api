using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Services.Queries;

namespace RentalPoint.Services.Handlers.Faq
{
    public class FaqListHandler : IRequestHandler<GetListQuery<IEnumerable<Data.Entities.Faq>>, IEnumerable<Data.Entities.Faq>>
    {
        private readonly RentalPointContext _context;
        
        public FaqListHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Data.Entities.Faq>> Handle(GetListQuery<IEnumerable<Data.Entities.Faq>> request, 
            CancellationToken cancellationToken)
        {
            return await _context.Faq.ToListAsync(cancellationToken);
        }
    }
}