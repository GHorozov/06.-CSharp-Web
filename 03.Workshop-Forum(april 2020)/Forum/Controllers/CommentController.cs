namespace Forum.Controllers
{
    using System.Threading.Tasks;

    using Forum.DataModels;
    using Forum.InputModels.Comment;
    using Forum.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class CommentController : Controller
    {
        private readonly UserManager<ForumUser> userManager;
        private readonly ICommentService commentService;

        public CommentController(UserManager<ForumUser> userManager, ICommentService commentService)
        {
            this.userManager = userManager;
            this.commentService = commentService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateCommentInputModel inputModel)
        {
            if (inputModel.ParentId != string.Empty)
            {
                if (!this.commentService.IsInPostId(inputModel.ParentId, inputModel.PostId))
                {
                    return this.BadRequest();
                }
            }

            var user = await this.userManager.GetUserAsync(this.User);
            await this.commentService.Create(user.Id, inputModel.PostId, inputModel.Content, inputModel.ParentId);

            return this.RedirectToAction("GetPostById", "Post", new { id = inputModel.PostId });
        }
    }
}
