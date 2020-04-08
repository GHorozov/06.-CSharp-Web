using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.DataModels
{
    public class Comment : BaseDeletableModel<string>
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Content { get; set; }

        public string PostId { get; set; }
        public virtual Post Post { get; set; }

        public string UserId { get; set; }
        public virtual ForumUser User { get; set; }
    }
}
