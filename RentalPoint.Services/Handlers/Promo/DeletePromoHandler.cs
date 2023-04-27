using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Services.Commands;

namespace RentalPoint.Services.Handlers.Promo
{
    public class DeletePromoHandler : IRequestHandler<DeleteCommand<Data.Entities.Promo>, Guid>
    {
        private readonly RentalPointContext _context;

        public DeletePromoHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(DeleteCommand<Data.Entities.Promo> request, CancellationToken cancellationToken)
        {
            var category = await _context.Promo.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (category != null)
            {
                _context.Remove(category);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return category?.Id ?? Guid.Empty;
        }
    }
}