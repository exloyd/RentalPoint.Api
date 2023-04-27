using System;
using System.Collections.Generic;

namespace RentalPoint.ViewModels
{
    public class CreateOrUpdateInventoryViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public IEnumerable<Guid> CategoryIds { get; set; }
    }
}