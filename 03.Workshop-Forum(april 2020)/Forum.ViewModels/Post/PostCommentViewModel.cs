namespace Forum.ViewModels.Post
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using Forum.DataModels;
    using Forum.Mapper.Interfaces;
    using Ganss.XSS;

    public class PostCommentViewModel : IMapFrom<Comment>
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public string CleanContent
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Content, @"<[^>]+>", string.Empty));
                return content;
            }
        }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string ParentCommentId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserUserName { get; set; }
    }
}
