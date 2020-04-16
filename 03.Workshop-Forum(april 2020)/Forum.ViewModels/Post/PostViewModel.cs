namespace Forum.ViewModels.Post
{
    using System;
    using Forum.DataModels;
    using Forum.Mapper.Interfaces;
    using Ganss.XSS;

    public class PostViewModel : IMapFrom<Post>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string UserUserName { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
