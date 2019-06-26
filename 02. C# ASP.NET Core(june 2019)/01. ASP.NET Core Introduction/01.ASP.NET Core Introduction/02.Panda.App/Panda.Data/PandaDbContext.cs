using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Panda.Domain;
using System;

namespace Panda.Data
{
    public class PandaDbContext : IdentityDbContext<PandaUser, PandaUserRole, string>
    {
        public DbSet<Package> Packages { get; set; }

        public DbSet<Receipt> Receipts { get; set; }

        public DbSet<PackageStatus> PackageStatus { get; set; }

        public PandaDbContext()
        {
        }

        public PandaDbContext(DbContextOptions<PandaDbContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server =.; Database = PandaDB; Trusted_Connection = true;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<PandaUser>()
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<PandaUser>()
                .HasMany(x => x.Packages)
                .WithOne(x => x.Recipient)
                .HasForeignKey(x => x.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
               .Entity<PandaUser>()
               .HasMany(x => x.Receipt)
               .WithOne(x => x.Recipient)
               .HasForeignKey(x => x.RecipientId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Receipt>()
                .HasOne(x => x.Package)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder
            //    .Entity<Package>()
            //    .HasOne(x => x.Status)
            //    .WithOne()
            //    .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
