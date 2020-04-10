namespace Forum.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Forum.ViewModels;
    using Forum.ViewModels.Home;
    using Forum.Services.Interfaces;

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
                Categories = this.categoryService.All<IndexCategoryViewModel>(count)
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
