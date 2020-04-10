namespace Forum.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Forum.Services.Interfaces;
    using Forum.ViewModels.Category;

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
