using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Data.Entities;
using RentalPoint.Data.Enums;
using RentalPoint.Services.Commands;

namespace RentalPoint.Services.Handlers.Penalty
{
    public class CreatePenaltyHandler : IRequestHandler<CreateCommand<Data.Entities.Penalty>, Guid>
    {
        private readonly RentalPointContext _context;

        public CreatePenaltyHandler(RentalPointContext context)
        {
            _context = context;
        }
        
        public async Task<Guid> Handle(CreateCommand<Data.Entities.Penalty> request, CancellationToken cancellationToken)
        {
            request.Entity.Paid = 0;
            request.Entity.Date = DateTime.Now;
            await _context.Penalty.AddAsync(request.Entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return request.Entity.Id;
        }
    }
}