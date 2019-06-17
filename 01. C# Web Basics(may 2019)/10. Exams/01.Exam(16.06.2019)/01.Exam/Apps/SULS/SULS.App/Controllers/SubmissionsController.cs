using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;
using SULS.App.BindingModels.Submissions;
using SULS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.App.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly ISubmissionService submissionService;

        public SubmissionsController(ISubmissionService submissionService)
        {
            this.submissionService = submissionService;
        }

        [Authorize]
        public IActionResult Create(SubmmissionCreateViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/");
            }

            return this.View(model);
        }


        [Authorize]
        [HttpPost]
        public IActionResult Create(SubmissionBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Submissions/Create");
            }

            this.submissionService.CreateSubmissionAndAddToProblem(model.Code, model.Id, this.User.Id);

            return this.Redirect("/");
        }

        [Authorize]
        public IActionResult Delete(SubmissionDeleteBindingModel model)
        {
            var submission = this.submissionService.GetSubmission(model.Id);
            if(submission.Id == null)
            {
                return this.Redirect("/");
            }

            this.submissionService.Delete(submission);

            return this.Redirect("/");
        }
    }
}
