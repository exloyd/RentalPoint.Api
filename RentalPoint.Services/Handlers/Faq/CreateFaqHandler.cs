using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RentalPoint.Data;
using RentalPoint.Services.Commands;

namespace RentalPoint.Services.Handlers.Faq
{
    public class CreateFaqHandler : IRequestHandler<CreateCommand<Data.Entities.Faq>, Guid>
    {
        private readonly RentalPointContext _context;

        public CreateFaqHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateCommand<Data.Entities.Faq> request, CancellationToken cancellationToken)
        {
            await _context.Faq.AddAsync(request.Entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return request.Entity.Id;
        }
    }
}