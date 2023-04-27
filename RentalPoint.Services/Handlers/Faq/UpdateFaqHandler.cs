using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Services.Commands;

namespace RentalPoint.Services.Handlers.Faq
{
    public class UpdateFaqHandler : IRequestHandler<UpdateCommand<Data.Entities.Faq>>
    {
        private readonly RentalPointContext _context;

        public UpdateFaqHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateCommand<Data.Entities.Faq> request, CancellationToken cancellationToken)
        {
            var faq = await _context.Faq.FirstOrDefaultAsync(x => x.Id == request.Entity.Id, cancellationToken);
            if (faq != null)
            {
                faq.Answer = request.Entity.Answer;
                faq.Date = DateTime.Now;

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}