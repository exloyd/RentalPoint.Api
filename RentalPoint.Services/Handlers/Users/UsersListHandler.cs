using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Services.Queries;

namespace RentalPoint.Services.Handlers.Users
{
    public class UsersListHandler : IRequestHandler<GetListQuery<IEnumerable<Data.Entities.User>>, IEnumerable<Data.Entities.User>>
    {
        private readonly RentalPointContext _context;
        
        public UsersListHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Data.Entities.User>> Handle(GetListQuery<IEnumerable<Data.Entities.User>> request, 
            CancellationToken cancellationToken)
        {
            return await _context.User
                .Include(x => x.Penalty)
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }
    }
}