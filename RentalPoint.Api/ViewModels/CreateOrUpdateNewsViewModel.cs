using System;

namespace RentalPoint.ViewModels
{
    public class CreateOrUpdateNewsViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Guid UserId { get; set; }
    }
}