namespace Forum.Data
{
    using Forum.DataModels;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ForumDbContext : IdentityDbContext<ForumUser, ForumRole, string>
    {
        public ForumDbContext(DbContextOptions<ForumDbContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //user
            builder
                .Entity<ForumUser>()
                .HasMany(x => x.Roles)
                .WithOne()
                .HasForeignKey(x => x.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .Entity<ForumUser>()
               .HasMany(x => x.Claims)
               .WithOne()
               .HasForeignKey(x => x.UserId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

            builder
              .Entity<ForumUser>()
              .HasMany(x => x.Logins)
              .WithOne()
              .HasForeignKey(x => x.UserId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);
           

        }
    }
}
