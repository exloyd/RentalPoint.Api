using System.Collections;
using MediatR;

namespace RentalPoint.Services.Queries
{
    public class GetListQuery<TCollection> : IRequest<TCollection> where TCollection : IEnumerable
    {
    }
}