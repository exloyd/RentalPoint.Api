using System;
using MediatR;
using RentalPoint.Data.Entities;

namespace RentalPoint.Services.Commands
{
    public class CreateCommand<TEntity> : IRequest<Guid> where TEntity : BaseEntity<Guid>
    {
        public CreateCommand(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            Entity = entity;
        }

        public TEntity Entity { get; }
    }
}