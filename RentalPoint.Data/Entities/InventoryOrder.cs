using System;

namespace RentalPoint.Data.Entities
{
    public class InventoryOrder : BaseEntity<Guid>
    {
        public Guid InventoryId { get; set; }
        public Guid OrderId { get; set; }
        
        public Inventory Inventory { get; set; }
    }
}