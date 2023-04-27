using System;

namespace RentalPoint.ViewModels
{
    public class CreatePenaltyViewModel
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public int Type { get; set; }
    }
}