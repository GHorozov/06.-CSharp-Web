using SIS.MvcFramework.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.App.BindingModels.Problems
{
    public class ProblemDetailsBindingModel
    {
        [RequiredSis]
        public string Id { get; set; }

        [RequiredSis]
        [StringLengthSis(5, 20, "Name must be between 5 and 20 symbols.")]
        public string Name { get; set; }
    }
}
