using System;

namespace RentalPoint.Data.Entities
{
    public class News : BaseEntity<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}