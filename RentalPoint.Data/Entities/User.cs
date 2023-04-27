using System;
using System.Collections.Generic;
using RentalPoint.Data.Enums;

namespace RentalPoint.Data.Entities
{
    public class User : BaseEntity<Guid>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public UserRole Role { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<Penalty> Penalty { get; set; }
    }
}