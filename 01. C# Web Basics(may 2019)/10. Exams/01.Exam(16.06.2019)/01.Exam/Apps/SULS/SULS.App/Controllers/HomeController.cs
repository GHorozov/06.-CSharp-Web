using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Result;
using SIS.MvcFramework;
using SULS.Services.Contracts;
using System.Linq;
using SULS.App.ViewModels.Home;

namespace SULS.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProblemService problemService;

        public HomeController(IProblemService problemService)
        {
            this.problemService = problemService;
        }

        [HttpGet(Url = "/")]
        public IActionResult IndexSlash()
        {
            return this.Index();
        }

        public IActionResult Index()
        {
            if (this.IsLoggedIn())
            {
                var problems = this.problemService
                    .GetAllProblems()
                    .Select(x => new HomeProblemViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Count = x.Submissions.Count()
                    })
                    .ToList();
                
                return this.View(problems, "IndexLoggedIn");
            }

            return this.View();
        }
    }
}