namespace Forum.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using Forum.DataModels.Enums;

    public class Vote : BaseModel<string>
    {
        public Vote()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string PostId { get; set; }

        public virtual Post Post { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ForumUser User { get; set; }

        public VoteType Type { get; set; }
    }
}
