using FDMC.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace _01.FDMC.Controllers
{
    public class HomeController : Controller
    {
        private readonly FDMCDbContext context;

        public HomeController(FDMCDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var cats = context
                .Cats
                .ToList();

            return this.View(cats);
        }
    }
}
