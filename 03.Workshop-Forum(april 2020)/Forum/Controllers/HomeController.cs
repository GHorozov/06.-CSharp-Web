namespace Forum.Controllers
{
    using System.Diagnostics;

    using Forum.Services.Interfaces;
    using Forum.ViewModels;
    using Forum.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class HomeController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly ILogger<HomeController> logger;

        public HomeController(ICategoryService categoryService, ILogger<HomeController> logger)
        {
            this.categoryService = categoryService;
            this.logger = logger;
        }

        public IActionResult Index(int? count)
        {
            var viewModel = new IndexViewModel()
            {
                Categories = this.categoryService.All<IndexCategoryViewModel>(count),
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
