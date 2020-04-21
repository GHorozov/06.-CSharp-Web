namespace Forum.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Forum.DataModels;
    using Forum.InputModels.Vote;
    using Forum.Services.Interfaces;
    using Forum.ViewModels.Vote;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class VoteController : ControllerBase
    {
        private readonly IVoteService voteService;
        private readonly UserManager<ForumUser> userManager;
        private readonly IPostService postService;

        public VoteController(IVoteService voteService, UserManager<ForumUser> userManager, IPostService postService)
        {
            this.voteService = voteService;
            this.userManager = userManager;
            this.postService = postService;
        }

        [HttpPost]
        public async Task<ActionResult<VoteViewModel>> VoteAsync(VoteInputModel inputModel)
        {
            var isPostIdExist = this.postService.IsExist(inputModel.PostId);
            if (!isPostIdExist)
            {
                return this.NotFound(inputModel.PostId);
            }

            var user = this.userManager.GetUserAsync(this.User);
            await this.voteService.VoteAsync(inputModel.PostId, user.Id.ToString(), inputModel.IsUpVote);
            var votes = this.voteService.GetVotesCount(inputModel.PostId);

            return new VoteViewModel() { VotesCount = votes };
        }
    }
}
