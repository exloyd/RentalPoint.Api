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
    public class CreateUsersHandler : IRequestHandler<CreateCommand<Data.Entities.User>, Guid>
    {
        private readonly RentalPointContext _context;

        public CreateUsersHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateCommand<Data.Entities.User> request, CancellationToken cancellationToken)
        {
            var hasUser = await _context.User.AnyAsync(x => x.Login == request.Entity.Login, cancellationToken);
            if (hasUser)
                throw new Exception("Пользователь с данным email уже существует в системе");

            request.Entity.Password = request.Entity.Password.GetHash();
            request.Entity.Date = DateTime.Now;
            await _context.User.AddAsync(request.Entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return request.Entity.Id;
        }
    }
}