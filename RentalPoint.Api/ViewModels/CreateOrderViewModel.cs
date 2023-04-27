using System;
using System.Collections.Generic;

namespace RentalPoint.ViewModels
{
    public class CreateOrderViewModel
    {
        public Guid? PromoId { get; set; }
        public Guid UserId { get; set; }
        public IEnumerable<CreateInventoryOrderViewModel> Inventory { get; set; }
    }

    public class CreateInventoryOrderViewModel
    {
        public Guid Id { get; set; }
        public int Count { get; set; }
    }
}