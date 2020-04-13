namespace Forum.ViewModels.Category
{
    using System;
    using Forum.DataModels;
    using Forum.Mapper.Interfaces;

    public class PostInCategoryViewModel : IMapFrom<Post>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ShortContent => this.Content.Length > 150 ? this.Content?.Substring(0, 50) + "..." : this.Content;

        public string UserUserName { get; set; }

        public int CommentsCount { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
