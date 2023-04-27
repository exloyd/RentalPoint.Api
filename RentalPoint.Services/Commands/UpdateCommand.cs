using System;
using MediatR;
using RentalPoint.Data.Entities;

namespace RentalPoint.Services.Commands
{
    public class UpdateCommand<TEntity> : IRequest where TEntity : BaseEntity<Guid>
    {
        public UpdateCommand(Guid id, TEntity entity)
        {
            entity.Id = id;
            Entity = entity;
        }

        public TEntity Entity { get; }
    }
}