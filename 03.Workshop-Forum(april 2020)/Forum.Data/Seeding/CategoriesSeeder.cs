namespace Forum.Data.Seeding
{
    using Forum.Data.Seeding.Interfaces;
    using Forum.DataModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ForumDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categories = new List<string>() { "Sport", "Coronavirus", "News", "Programming", "Cats", "Dogs", "Arts" };
            foreach (var category in categories)
            {
                await dbContext.Categories.AddAsync(new Category()
                {
                    Name = category,
                    CreatedOn = DateTime.UtcNow,
                    Description = category,
                    Title = category,
                });
            }
        }
    }
}
