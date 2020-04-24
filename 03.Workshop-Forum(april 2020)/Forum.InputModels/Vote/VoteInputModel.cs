namespace Forum.InputModels.Vote
{
    using System.ComponentModel.DataAnnotations;

    public class VoteInputModel
    {
        [Required]
        public string PostId { get; set; }

        [Required]
        public bool IsUpVote { get; set; }
    }
}
