namespace Forum.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Forum.Data;
    using Forum.DataModels;
    using Forum.DataModels.Enums;
    using Forum.Services.Interfaces;

    public class VoteService : IVoteService
    {
        private readonly ForumDbContext context;

        public VoteService(ForumDbContext context)
        {
            this.context = context;
        }

        public int GetVotesCount(string postId)
        {
            var result = this.context
                .Votes
                .Where(x => x.PostId == postId)
                .Sum(x => (int)x.Type);

            return result;
        }

        public async Task VoteAsync(string postId, string userId, bool isUpVote)
        {
            var vote = this.context
                .Votes
                .FirstOrDefault(x => x.PostId == postId && x.UserId == userId);

            if (vote != null)
            {
                vote.Type = isUpVote ? VoteType.Up : VoteType.Down;
            }
            else
            {
                vote = new Vote()
                {
                    PostId = postId,
                    UserId = userId,
                    CreatedOn = DateTime.UtcNow,
                    Type = isUpVote ? VoteType.Up : VoteType.Down,
                };

                await this.context.Votes.AddAsync(vote);
            }

            await this.context.SaveChangesAsync();
        }
    }
}
