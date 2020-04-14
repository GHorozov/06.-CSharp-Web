namespace Forum.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Forum.Services.Interfaces;
    using Forum.ViewModels.Category;
    using Microsoft.AspNetCore.Mvc;

    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IActionResult GetByName(string name)
        {
            var viewModel = this.categoryService.GetByName<CategoryViewModel>(name);

            return this.View(viewModel);
        }
    }
}
