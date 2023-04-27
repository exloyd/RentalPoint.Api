using System;
using RentalPoint.Data.Enums;

namespace RentalPoint.Data.Entities
{
    public class Penalty : BaseEntity<Guid>
    {
        public PenaltyType Type { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public decimal Paid { get; set; }
    }
}