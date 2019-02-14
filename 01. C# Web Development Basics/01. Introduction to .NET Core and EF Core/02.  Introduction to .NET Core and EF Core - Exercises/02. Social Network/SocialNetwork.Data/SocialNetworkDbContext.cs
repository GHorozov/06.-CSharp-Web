namespace SocialNetwork.Data
{
    using Microsoft.EntityFrameworkCore;
    using SocialNetwork.Models;

    public class SocialNetworkDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<UserFriend> UserFriend { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<AlbumTag> AlbumTag { get; set; }

        public DbSet<UserAlbum> UserAlbum { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(@"Server=.;Database=SocialNetwork;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<UserFriend>()
                .HasKey(x => new { x.UserId, x.FriendId });

            modelBuilder
                .Entity<UserFriend>()
                .HasOne(x => x.Friend)
                .WithMany()
                .HasForeignKey(x => x.FriendId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<UserFriend>()
                .HasOne(x => x.User)
                .WithMany(x => x.Friends)
                .HasForeignKey(x => x.UserId);

            modelBuilder
                .Entity<Picture>()
                .HasOne(x => x.Album)
                .WithMany(x => x.Pictures)
                .HasForeignKey(x => x.AlbumId);

            modelBuilder
                .Entity<User>()
                .HasMany(x => x.Albums)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            modelBuilder
                .Entity<UserAlbum>()
                .HasKey(x => new { x.UserId, x.AlbumId });

            modelBuilder
                .Entity<User>()
                .HasMany(x => x.UserAlbum)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
                
            modelBuilder
                .Entity<Album>()
                .HasMany(x => x.UserAlbum)
                .WithOne(x => x.Album)
                .HasForeignKey(x => x.AlbumId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<AlbumTag>()
                .HasKey(x => new { x.AlbumId, x.TagId });

            modelBuilder
                .Entity<AlbumTag>()
                .HasOne(x => x.Album)
                .WithMany(x => x.Tags)
                .HasForeignKey(x => x.AlbumId);

            modelBuilder
                .Entity<AlbumTag>()
                .HasOne(x => x.Tag)
                .WithMany(x => x.Albums)
                .HasForeignKey(x => x.TagId);
        }
    }
}
