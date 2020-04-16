namespace Forum.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Forum.Data;
    using Forum.DataModels;
    using Forum.Mapper;
    using Forum.Services.Interfaces;

    public class PostService : IPostService
    {
        private readonly ForumDbContext context;

        public PostService(ForumDbContext context)
        {
            this.context = context;
        }

        public T ById<T>(string id)
        {
            var result = this.context
                .Posts
                .Where(x => !x.IsDeleted && x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return result;
        }

        public async Task<string> Create(string title, string content, string categoryId, string userId)
        {
            var post = new Post()
            {
                Title = title,
                Content = content,
                UserId = userId,
                CategoryId = categoryId,
                CreatedOn = DateTime.UtcNow,
            };

            await this.context.Posts.AddAsync(post);
            await this.context.SaveChangesAsync();

            return post.Id;
        }
    }
}
