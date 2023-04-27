using System;

namespace RentalPoint.Data.Entities
{
    public class Category : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Image { get; set; }
    }
}