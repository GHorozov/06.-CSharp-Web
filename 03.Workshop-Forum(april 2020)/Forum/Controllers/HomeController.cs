namespace Forum.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using Forum.ViewModels;
    using Forum.Data;
    using Forum.ViewModels.Home;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ForumDbContext context;

        public HomeController(ILogger<HomeController> logger, ForumDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();
            var categories = this.context.Categories.Select(x => new IndexCategoryViewModel()
            {
                Name = x.Name,
                Title = x.Title,
                Description = x.Description,
                ImageUrl = x.ImageUrl
            })
            .ToList();

            viewModel.Categories = categories;

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
