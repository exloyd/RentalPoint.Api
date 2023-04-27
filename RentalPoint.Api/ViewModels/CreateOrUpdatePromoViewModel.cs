using System;

namespace RentalPoint.ViewModels
{
    public class CreateOrUpdatePromoViewModel
    {
        public string Code { get; set; }
        public int Discount { get; set; }
        public int Count { get; set; }
        public DateTime BestBefore { get; set; }
    }
}