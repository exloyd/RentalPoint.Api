using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Services.Queries;

namespace RentalPoint.Services.Handlers.Users
{
    public class UsersByIdHandler : IRequestHandler<GetByIdQuery<Data.Entities.User>, Data.Entities.User>
    {
        private readonly RentalPointContext _context;

        public UsersByIdHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task<Data.Entities.User> Handle(GetByIdQuery<Data.Entities.User> request, CancellationToken cancellationToken)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}