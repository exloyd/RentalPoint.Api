using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Services.Commands;
using RentalPoint.Services.Extensions;

namespace RentalPoint.Services.Handlers.Users
{
    public class UpdateUsersHandler : IRequestHandler<UpdateCommand<Data.Entities.User>>
    {
        private readonly RentalPointContext _context;

        public UpdateUsersHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateCommand<Data.Entities.User> request, CancellationToken cancellationToken)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Id == request.Entity.Id, cancellationToken);
            if (user != null)
            {
                user.Name = request.Entity.Name;
                user.Login = request.Entity.Login;
                user.Role = request.Entity.Role;
                user.Password = request.Entity.Password.GetHash();

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}