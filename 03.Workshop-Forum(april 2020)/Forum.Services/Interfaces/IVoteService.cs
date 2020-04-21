namespace Forum.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IVoteService
    {
        Task VoteAsync(string postId, string userId, bool isUpVote);

        int GetVotesCount(string postId);
    }
}
