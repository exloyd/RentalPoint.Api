using System.Collections;
using MediatR;

namespace RentalPoint.Services.Queries
{
    public class GetMonthlyReportQuery<TCollection> : IRequest<TCollection> where TCollection : IEnumerable
    {
        
    }
}