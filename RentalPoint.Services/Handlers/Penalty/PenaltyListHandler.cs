using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Services.Queries;

namespace RentalPoint.Services.Handlers.Penalty
{
    public class PenaltyListHandler : IRequestHandler<GetListQuery<IEnumerable<Data.Entities.Penalty>>, IEnumerable<Data.Entities.Penalty>>
    {
        private readonly RentalPointContext _context;
        
        public PenaltyListHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Data.Entities.Penalty>> Handle(GetListQuery<IEnumerable<Data.Entities.Penalty>> request, 
            CancellationToken cancellationToken)
        {
            return await _context.Penalty.ToListAsync(cancellationToken);
        }
    }
}