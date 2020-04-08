namespace Forum.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Post : BaseDeletableModel<string> 
    {
        public Post()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Comments = new HashSet<Comment>();
        }

        public string Title { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }
        public virtual ForumUser User { get; set; }

        public string CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
