using System;
using System.Collections.Generic;

namespace RentalPoint.Data.Entities
{
    public class InventoryCategory : BaseEntity<Guid>
    {
        public Guid InventoryId { get; set; }
        public Guid CategoryId { get; set; }
    }
}