namespace Forum.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Forum.Services.Interfaces;
    using Forum.ViewModels.Category;
    using Microsoft.AspNetCore.Mvc;

    public class CategoryController : Controller
    {
        private const int ItemsPerPage = 5;
        private readonly ICategoryService categoryService;
        private readonly IPostService postService;

        public CategoryController(ICategoryService categoryService, IPostService postService)
        {
            this.categoryService = categoryService;
            this.postService = postService;
        }

        public IActionResult GetByName(string name, int page = 1)
        {
            if (page <= 0)
            {
                page = 1;
            }

            var viewModel = this.categoryService.GetByName<CategoryViewModel>(name);
            viewModel.CurrentPage = page;
            viewModel.ForumPosts = this.postService.GetByCategoryId<PostInCategoryViewModel>(viewModel.Id, ItemsPerPage, (page - 1) * ItemsPerPage);
            var countPosts = this.postService.GetCountByCategoryId(viewModel.Id);
            viewModel.PagesCount = (int)Math.Ceiling((double)countPosts / ItemsPerPage);

            return this.View(viewModel);
        }
    }
}
