﻿namespace _05.ShopHierarchy
{
    using Microsoft.EntityFrameworkCore;

    public class ShopDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Salesman> Salesmans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(@"Server=.;Database=Shop;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .HasOne(x => x.Salesman)
                .WithMany(x => x.Customers)
                .HasForeignKey(x => x.SalesmanId);
        }
    }
}
