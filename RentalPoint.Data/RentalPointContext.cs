using Microsoft.EntityFrameworkCore;
using RentalPoint.Data.Entities;

namespace RentalPoint.Data
{
    public class RentalPointContext : DbContext
    {
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<InventoryCategory> InventoryCategory { get; set; }
        
        public DbSet<Faq> Faq { get; set; }
        
        public DbSet<News> News { get; set; }
        
        public DbSet<Promo> Promo { get; set; }
        
        public DbSet<Penalty> Penalty { get; set; }
        
        public DbSet<Order> Orders { get; set; }
        public DbSet<InventoryOrder> InventoryOrder { get; set; }

        public DbSet<User> User { get; set; }

        public RentalPointContext(DbContextOptions<RentalPointContext> options) : base(options)
        {
        }
    }
}