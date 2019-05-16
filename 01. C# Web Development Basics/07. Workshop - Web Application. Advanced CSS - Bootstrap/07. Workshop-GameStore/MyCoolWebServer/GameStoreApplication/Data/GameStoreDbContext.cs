namespace MyCoolWebServer.GameStoreApplication.Data
{
    using Microsoft.EntityFrameworkCore;
    using MyCoolWebServer.GameStoreApplication.Data.Models;

    public class GameStoreDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        public DbSet<Game> Games { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<UserGame> UserGames { get; set; }

        public DbSet<OrderGame> OrderGames { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Server=.; Database=GameStore;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //store only unique email in database
            modelBuilder
                .Entity<User>()
                .HasIndex(x => x.Email)
                .IsUnique();

            modelBuilder
                .Entity<UserGame>()
                .HasKey(x => new { x.UserId, x.GameId });

            modelBuilder
                .Entity<User>()
                .HasMany(x => x.Games)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            modelBuilder
                .Entity<Game>()
                .HasMany(x => x.Users)
                .WithOne(x => x.Game)
                .HasForeignKey(x => x.GameId);

            modelBuilder
                .Entity<Order>()
                .HasOne(x => x.User)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.UserId);

            modelBuilder
                .Entity<OrderGame>()
                .HasKey(x => new { x.OrderId, x.GameId });

            modelBuilder
                .Entity<Order>()
                .HasMany(x => x.Games)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);

            modelBuilder
                .Entity<Game>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.Game)
                .HasForeignKey(x => x.GameId);
        }
    }
}
