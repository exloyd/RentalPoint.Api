using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Services.Commands;

namespace RentalPoint.Services.Handlers.Promo
{
    public class UpdatePromoHandler : IRequestHandler<UpdateCommand<Data.Entities.Promo>>
    {
        private readonly RentalPointContext _context;

        public UpdatePromoHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateCommand<Data.Entities.Promo> request, CancellationToken cancellationToken)
        {
            var category = await _context.Promo.FirstOrDefaultAsync(x => x.Id == request.Entity.Id, cancellationToken);
            if (category != null)
            {
                category.Code = request.Entity.Code;
                category.Discount = request.Entity.Discount;
                category.Count = request.Entity.Count;
                category.BestBefore = request.Entity.BestBefore;
                
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}