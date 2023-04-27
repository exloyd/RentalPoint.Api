using System;
using RentalPoint.Data.Enums;

namespace RentalPoint.Data.Entities
{
    public class Faq : BaseEntity<Guid>
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public DateTime Date { get; set; }
        public FaqType Type { get; set; }
    }
}