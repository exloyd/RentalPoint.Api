using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Services.Commands;

namespace RentalPoint.Services.Handlers.News
{
    public class DeleteNewsHandler : IRequestHandler<DeleteCommand<Data.Entities.News>, Guid>
    {
        private readonly RentalPointContext _context;

        public DeleteNewsHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(DeleteCommand<Data.Entities.News> request, CancellationToken cancellationToken)
        {
            var news = await _context.News.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (news != null)
            {
                _context.Remove(news);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return news?.Id ?? Guid.Empty;
        }
    }
}