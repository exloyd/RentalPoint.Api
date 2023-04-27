using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Data.Entities;
using RentalPoint.Services.Extensions;
using RentalPoint.Services.Queries;

namespace RentalPoint.Services.Handlers.Authorization
{
    public class AuthorizationHandler : IRequestHandler<AuthorizeByCredentialsQuery, User>
    {
        private readonly RentalPointContext _context;

        public AuthorizationHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task<User> Handle(AuthorizeByCredentialsQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.User
                .Include(x => x.Penalty)
                .FirstOrDefaultAsync(x => x.Login == request.User.Login, cancellationToken);

            if (request.AfterRegister == false && user.Password == request.User.Password.GetHash() ||
                request.AfterRegister && user.Password == request.User.Password)
                return user;

            throw new Exception("Неверный логин или пароль");
        }
    }
}