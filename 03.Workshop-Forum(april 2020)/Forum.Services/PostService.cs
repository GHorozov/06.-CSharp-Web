namespace Forum.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Forum.Data;
    using Forum.DataModels;
    using Forum.Services.Interfaces;

    public class PostService : IPostService
    {
        private readonly ForumDbContext context;

        public PostService(ForumDbContext context)
        {
            this.context = context;
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
