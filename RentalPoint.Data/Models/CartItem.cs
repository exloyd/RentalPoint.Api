using RentalPoint.Data.Entities;

namespace RentalPoint.Data.Models
{
    public class CartItem
    {
        public Inventory Inventory { get; set; }
        public int Count { get; set; }
    }
}