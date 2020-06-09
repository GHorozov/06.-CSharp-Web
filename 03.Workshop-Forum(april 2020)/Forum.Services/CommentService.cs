namespace Forum.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Forum.Data;
    using Forum.DataModels;
    using Forum.Services.Interfaces;

    public class CommentService : ICommentService
    {
        private readonly ForumDbContext context;

        public CommentService(ForumDbContext context)
        {
            this.context = context;
        }

        public async Task Create(string userId, string postId, string content, string parentId = null)
        {
            var comment = new Comment()
            {
                PostId = postId,
                CreatedOn = DateTime.UtcNow,
                Content = content,
                ParentCommentId = parentId,
                UserId = userId,
            };

            await this.context.Comments.AddAsync(comment);
            await this.context.SaveChangesAsync();
        }
    }
}
