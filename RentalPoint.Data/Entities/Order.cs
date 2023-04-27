using System;
using System.Collections.Generic;
using RentalPoint.Data.Enums;

namespace RentalPoint.Data.Entities
{
    public class Order : BaseEntity<Guid>
    {
        public decimal Price { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        
        public IEnumerable<InventoryOrder> InventoryOrders { get; set; }
    }
}