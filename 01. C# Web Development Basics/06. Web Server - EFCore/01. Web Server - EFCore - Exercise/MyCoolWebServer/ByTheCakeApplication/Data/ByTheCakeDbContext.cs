namespace MyCoolWebServer.ByTheCakeApplication.Data
{
    using Microsoft.EntityFrameworkCore;
    using MyCoolWebServer.ByTheCakeApplication.Data.Models;

    public class ByTheCakeDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderProduct> OrdersProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {          
            optionsBuilder.UseSqlServer(@"Server=.; Database=ByTheCakeDb;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Order>()
                .HasOne(x => x.User)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.UserId);

            builder
                 .Entity<OrderProduct>()
                 .HasKey(x => new { x.OrderId, x.ProductId });

            builder
                .Entity<Order>()
                .HasMany(x => x.Products)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Product>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
