using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Services.Commands;

namespace RentalPoint.Services.Handlers.Users
{
    public class DeleteUsersHandler : IRequestHandler<DeleteCommand<Data.Entities.User>, Guid>
    {
        private readonly RentalPointContext _context;

        public DeleteUsersHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(DeleteCommand<Data.Entities.User> request, CancellationToken cancellationToken)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (user != null)
            {
                _context.Remove(user);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return user?.Id ?? Guid.Empty;
        }
    }
}