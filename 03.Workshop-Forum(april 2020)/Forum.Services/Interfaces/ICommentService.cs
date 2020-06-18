namespace Forum.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICommentService
    {
        Task Create(string userId, string postId, string content, string parentId = null);

        bool IsInPostId(string commentId, string postId);
    }
}
