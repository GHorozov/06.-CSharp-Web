using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Action;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;
using SULS.App.BindingModels.Problems;
using SULS.App.ViewModels.Problems;
using SULS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SULS.App.Controllers
{
    public class ProblemsController : Controller
    {
        private readonly IProblemService problemService;

        public ProblemsController(IProblemService problemService)
        {
            this.problemService = problemService;
        }


        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(CreateBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Problems/Create");
            }

            this.problemService.CreateProblem(model.Name, model.Points, this.User.Username);

            return this.Redirect("/");
        }

        [Authorize]
        public IActionResult Details(ProblemDetailsBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/");
            }

            var problem = this.problemService
                .GetById(model.Id);

            if(problem == null)
            {
                return this.Redirect("/");
            }

            var details = new ProblemDetailsViewModel()
            {
                Name = problem.Name,
                Submissions = problem
                .Submissions
                .Select(x => new ProblemDetailsViewModel.SubmissionDetails
                {
                    AchievedResult = GetResult(x.AchievedResult, problem.Points),
                    CreatedOn = x.CreatedOn.ToString("dd/MM/yyyy"),
                    Username = x.User.Username,
                    SubmissionId = x.Id
                })
                .ToList()
            };

            return this.View(details);
        }

        [NonAction]
        private int GetResult(int achievedResult, int points)
        {
            var firstParam = (decimal)achievedResult;
            var secondParam = (decimal)points;
            var result = (int)decimal.Round(firstParam / secondParam * 100M);

            return result;
        }
    }
}
