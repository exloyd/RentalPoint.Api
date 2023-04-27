using System;
using RentalPoint.Data.Entities;

namespace RentalPoint.Data.Models
{
    public class MonthlyReport<TEntity> where TEntity : BaseEntity<Guid>
    {
        public string Month { get; set; }
        public int MonthNumber { get; set; }
        public int Count { get; set; }
    }
}