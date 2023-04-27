using System;
using System.Collections.Generic;

namespace RentalPoint.Data.Entities
{
    public class CreateOrder : BaseEntity<Guid>
    {
        public Guid? PromoId { get; set; }
        public Guid UserId { get; set; }
        public IEnumerable<CreateInventoryOrder> Inventory { get; set; }
    }

    public class CreateInventoryOrder
    {
        public Guid Id { get; set; }
        public int Count { get; set; }
    }
}