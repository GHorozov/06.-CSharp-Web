namespace Forum.Services
{
    using Forum.Data;
    using Forum.DataModels;
    using Forum.Mapper;
    using Forum.Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CategoryService : ICategoryService
    {
        private readonly ForumDbContext context;

        public CategoryService(ForumDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<T> All<T>(int? count = null)
        {
            IQueryable<Category> query = this.context
                .Categories
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.Name);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            var result = query.To<T>().ToList(); 

            return result;
        }
    }
}
