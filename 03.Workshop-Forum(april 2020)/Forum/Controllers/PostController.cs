namespace Forum.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Forum.DataModels;
    using Forum.InputModels.Post;
    using Forum.Services.Interfaces;
    using Forum.ViewModels.Category;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostService postService;
        private readonly ICategoryService categoryService;
        private readonly UserManager<ForumUser> userManager;

        public PostController(IPostService postService, ICategoryService categoryService, UserManager<ForumUser> userManager)
        {
            this.postService = postService;
            this.categoryService = categoryService;
            this.userManager = userManager;
        }

        public IActionResult GetPostById(string id)
        {
            return this.View();
        }

        public IActionResult Create()
        {
            var categories = this.categoryService.All<CategoryDropDownViewModel>(null);
            var viewModel = new PostCreateInputModel()
            {
                Categories = categories,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var postId = await this.postService.Create(inputModel.Title, inputModel.Content, inputModel.CategoryId, user.Id);

            return this.RedirectToAction(nameof(this.GetPostById), new { id = postId });
        }
    }
}
