using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Warehouse.Dal.Entities;

namespace Warehouse.Dal.Contexts
{
    public class WarehouseContext : IdentityDbContext
    {
        public WarehouseContext() { }

        public WarehouseContext(DbContextOptions<WarehouseContext> options) : base(options)
        {
        }

        public DbSet<Producer> Producers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Item>().Property(p => p.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Item>().Property(p => p.Discount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>().Property(p => p.TotalSum).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderItem>().Property(p => p.Discount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderItem>().Property(p => p.SumWithDiscount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderItem>().Property(p => p.SumWithoutDiscount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderItem>().Property(p => p.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Payment>().Property(p => p.TotalSum).HasColumnType("decimal(18,2)");
        }
    }
}
