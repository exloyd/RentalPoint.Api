using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalPoint.Data;
using RentalPoint.Services.Queries;

namespace RentalPoint.Services.Handlers.News
{
    public class NewsByIdHandler : IRequestHandler<GetByIdQuery<Data.Entities.News>, Data.Entities.News>
    {
        private readonly RentalPointContext _context;

        public NewsByIdHandler(RentalPointContext context)
        {
            _context = context;
        }

        public async Task<Data.Entities.News> Handle(GetByIdQuery<Data.Entities.News> request, CancellationToken cancellationToken)
        {
            return await _context.News.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}