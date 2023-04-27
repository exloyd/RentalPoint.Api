using System;

namespace RentalPoint.Data.Entities
{
    public class BaseEntity<T> where T : IComparable
    {
        public T Id { get; set; }
    }
}