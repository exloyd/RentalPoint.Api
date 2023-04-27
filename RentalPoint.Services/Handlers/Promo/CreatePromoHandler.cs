using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Services.Commands;

namespace RentalPoint.Services.Handlers.Promo
{
    public class CreatePromoHandler : IRequestHandler<CreateCommand<Data.Entities.Promo>, Guid>
    {
        private readonly RentalPointContext _context;

        public CreatePromoHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateCommand<Data.Entities.Promo> request, CancellationToken cancellationToken)
        {
            var hasPromo = await _context.Promo.AnyAsync(x => x.Code == request.Entity.Code, cancellationToken);
            if (hasPromo)
                throw new Exception("Данный промокод уже существует в системе");
            
            request.Entity.Date = DateTime.Now;
            await _context.Promo.AddAsync(request.Entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return request.Entity.Id;
        }
    }
}