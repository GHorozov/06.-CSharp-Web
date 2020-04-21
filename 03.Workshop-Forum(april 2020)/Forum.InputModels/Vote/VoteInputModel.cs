namespace Forum.InputModels.Vote
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class VoteInputModel
    {
        [Required]
        public string PostId { get; set; }

        [Required]
        public bool IsUpVote { get; set; }
    }
}
