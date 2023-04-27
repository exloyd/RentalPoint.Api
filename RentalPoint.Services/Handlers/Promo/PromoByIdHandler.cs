using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Services.Queries;

namespace RentalPoint.Services.Handlers.Promo
{
    public class PromoByIdHandler : IRequestHandler<GetByIdQuery<Data.Entities.Promo>, Data.Entities.Promo>
    {
        private readonly RentalPointContext _context;

        public PromoByIdHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task<Data.Entities.Promo> Handle(GetByIdQuery<Data.Entities.Promo> request, CancellationToken cancellationToken)
        {
            return await _context.Promo.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}