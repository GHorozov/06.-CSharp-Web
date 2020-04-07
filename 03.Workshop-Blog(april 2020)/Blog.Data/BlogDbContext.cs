namespace Blog.Data
{
    using Blog.DataModels;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class BlogDbContext : IdentityDbContext<BlogUser, BlogRole, string>
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //user
            builder
                .Entity<BlogUser>()
                .HasMany(x => x.Roles)
                .WithOne()
                .HasForeignKey(x => x.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .Entity<BlogUser>()
               .HasMany(x => x.Claims)
               .WithOne()
               .HasForeignKey(x => x.UserId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

            builder
              .Entity<BlogUser>()
              .HasMany(x => x.Logins)
              .WithOne()
              .HasForeignKey(x => x.UserId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);
           

        }
    }
}
