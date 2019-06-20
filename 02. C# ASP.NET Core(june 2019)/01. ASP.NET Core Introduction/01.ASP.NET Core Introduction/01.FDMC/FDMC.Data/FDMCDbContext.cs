using Microsoft.EntityFrameworkCore;
using System;

namespace FDMC.Data
{
    public class FDMCDbContext : DbContext
    {
        public FDMCDbContext(DbContextOptions<FDMCDbContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
