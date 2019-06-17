using SIS.MvcFramework.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.App.BindingModels.Submissions
{
    public class SubmissionBindingModel
    {
        public string Id { get; set; }

        [StringLengthSis(30, 800, "Name must be between 30 and 800 symbols.")]
        public string Code { get; set; }
    }
}
