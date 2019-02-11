namespace _09.QueryLoadedData
{
    using Microsoft.EntityFrameworkCore;

    public class ShopDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Salesman> Salesmans { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<ItemsOrders> ItemsOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(@"Server=.;Database=ShopLoadedData;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .HasOne(x => x.Salesman)
                .WithMany(x => x.Customers)
                .HasForeignKey(x => x.SalesmanId);

            modelBuilder.Entity<Order>()
                .HasOne(x => x.Customer)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CustomerId);

            modelBuilder.Entity<Review>()
                .HasOne(x => x.Customer)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.CustomerId);

            modelBuilder.Entity<ItemsOrders>()
                .HasKey(x => new { x.ItemId, x.OrderId });

            modelBuilder.Entity<Item>()
                .HasMany(x => x.ItemsOrders)
                .WithOne(x => x.Item)
                .HasForeignKey(x => x.ItemId);

            modelBuilder.Entity<Order>()
                .HasMany(x => x.ItemsOrders)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);

            modelBuilder.Entity<Review>()
                .HasOne(x => x.Item)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.ItemId);
        }
    }
}
