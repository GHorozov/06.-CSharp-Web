namespace Forum.Data.Seeding.Interfaces
{
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(ForumDbContext dbContext, IServiceProvider serviceProvider);
    }
}
