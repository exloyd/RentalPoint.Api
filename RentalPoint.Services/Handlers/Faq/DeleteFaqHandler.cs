using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Services.Commands;

namespace RentalPoint.Services.Handlers.Faq
{
    public class DeleteFaqHandler : IRequestHandler<DeleteCommand<Data.Entities.Faq>, Guid>
    {
        private readonly RentalPointContext _context;

        public DeleteFaqHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(DeleteCommand<Data.Entities.Faq> request, CancellationToken cancellationToken)
        {
            var faq = await _context.Faq.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (faq != null)
            {
                _context.Remove(faq);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return faq?.Id ?? Guid.Empty;
        }
    }
}