using System;

namespace RentalPoint.Data.Entities
{
    public class Promo : BaseEntity<Guid>
    {
        public string Code { get; set; }
        public decimal Discount { get; set; }
        public int Count { get; set; }
        public DateTime BestBefore { get; set; }
        public DateTime Date { get; set; }
    }
}