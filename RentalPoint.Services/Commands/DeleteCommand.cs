using System;
using MediatR;
using RentalPoint.Data.Entities;

namespace RentalPoint.Services.Commands
{
    public class DeleteCommand<TEntity> : IRequest<Guid> where TEntity : BaseEntity<Guid>
    {
        public DeleteCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}