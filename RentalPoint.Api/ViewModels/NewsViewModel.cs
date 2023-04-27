using System;

namespace RentalPoint.ViewModels
{
    public class NewsViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }
}