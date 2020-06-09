namespace Forum.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Forum.Data;
    using Forum.DataModels;
    using Forum.Mapper;
    using Forum.Services.Interfaces;

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

        public T GetByName<T>(string name, int? take = null, int skip = 0)
        {
            var result = this.context
                .Categories
                .Where(x => !x.IsDeleted && x.Name == name)
                .To<T>()
                .FirstOrDefault();

            return result;
        }
    }
}
