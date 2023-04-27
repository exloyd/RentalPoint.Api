using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Services.Commands;

namespace RentalPoint.Services.Handlers.Penalty
{
    public class UpdatePenaltyHandler : IRequestHandler<UpdateCommand<Data.Entities.Penalty>>
    {
        private readonly RentalPointContext _context;

        public UpdatePenaltyHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateCommand<Data.Entities.Penalty> request, CancellationToken cancellationToken)
        {
            var penalties = await _context.Penalty.Where(x => x.UserId == request.Entity.Id).ToListAsync(cancellationToken);
            if (penalties != null && penalties.Any())
            {
                var remainPenalties = penalties.Where(x => x.Amount - x.Paid > 0).ToList();
                var paid = request.Entity.Paid;
                foreach (var penalty in remainPenalties)
                {
                    var remained = penalty.Amount - penalty.Paid;
                    if (remained <= paid)
                    {
                        penalty.Paid += remained;
                        paid -= remained;
                    }
                }
                
                _context.UpdateRange(remainPenalties);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}