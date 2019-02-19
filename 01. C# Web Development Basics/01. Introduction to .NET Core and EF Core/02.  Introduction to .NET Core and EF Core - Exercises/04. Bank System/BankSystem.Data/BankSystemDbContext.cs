namespace BankSystem.Data
{
    using BankSystem.Models;
    using Microsoft.EntityFrameworkCore;

    public class BankSystemDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<CheckingAccount> CheckingAccounts { get; set; }

        public DbSet<SavingsAccount> SavingsAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(@"Server=.;Database=BankSystem;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasMany(x => x.CheckingAccounts)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            modelBuilder
                .Entity<User>()
                .HasMany(x => x.SavingsAccounts)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
