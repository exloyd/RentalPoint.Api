using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Services.Commands;

namespace RentalPoint.Services.Handlers.News
{
    public class UpdateNewsHandler : IRequestHandler<UpdateCommand<Data.Entities.News>>
    {
        private readonly RentalPointContext _context;

        public UpdateNewsHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateCommand<Data.Entities.News> request, CancellationToken cancellationToken)
        {
            var news = await _context.News.FirstOrDefaultAsync(x => x.Id == request.Entity.Id, cancellationToken);
            if (news != null)
            {
                news.Title = request.Entity.Title;
                news.Description = request.Entity.Description;
                news.Image = request.Entity.Image;
                news.UserId = request.Entity.UserId;
                news.Date = DateTime.Now;

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}