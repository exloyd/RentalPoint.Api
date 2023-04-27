using System;
using MediatR;
using RentalPoint.Data.Entities;

namespace RentalPoint.Services.Queries
{
    public class GetByIdQuery<TEntity> : IRequest<TEntity> where TEntity : BaseEntity<Guid>
    {
        public GetByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}