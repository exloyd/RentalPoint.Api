using System;
using System.Collections.Generic;

namespace RentalPoint.Data.Entities
{
    public class Inventory : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        
        public ICollection<InventoryCategory> Categories { get; set; }
    }
}