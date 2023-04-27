using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Services.Commands;

namespace RentalPoint.Services.Handlers.Order
{
    public class UpdateOrderHandler : IRequestHandler<UpdateCommand<Data.Entities.Order>>
    {
        private readonly RentalPointContext _context;

        public UpdateOrderHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateCommand<Data.Entities.Order> request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == request.Entity.Id, cancellationToken);
            if (order != null)
            {
                order.Status = request.Entity.Status;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}