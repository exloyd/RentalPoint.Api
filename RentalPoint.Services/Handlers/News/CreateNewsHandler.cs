using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RentalPoint.Data;
using RentalPoint.Services.Commands;

namespace RentalPoint.Services.Handlers.News
{
    public class CreateNewsHandler : IRequestHandler<CreateCommand<Data.Entities.News>, Guid>
    {
        private readonly RentalPointContext _context;

        public CreateNewsHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateCommand<Data.Entities.News> request, CancellationToken cancellationToken)
        {
            request.Entity.Date = DateTime.Now;
            await _context.News.AddAsync(request.Entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return request.Entity.Id;
        }
    }
}