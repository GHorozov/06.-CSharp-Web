using SIS.MvcFramework.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.App.BindingModels.Problems
{
    public class CreateBindingModel
    {
        [RequiredSis]
        [StringLengthSis(5, 20, "Name must be between 5 and 20 symbols.")]
        public string Name { get; set; }

        [RequiredSis]
        [RangeSis(50, 300, "Points must ne between 50 and 300." )]
        public int Points { get; set; }
    }
}
