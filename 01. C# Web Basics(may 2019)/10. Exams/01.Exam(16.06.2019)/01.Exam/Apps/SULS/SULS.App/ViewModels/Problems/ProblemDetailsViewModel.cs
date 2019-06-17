using SULS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.App.ViewModels.Problems
{
    public class ProblemDetailsViewModel
    {
        public string Name { get; set; }

        public ICollection<SubmissionDetails> Submissions { get; set; }

        public class SubmissionDetails
        {
            public string Username { get; set; }

            public int AchievedResult { get; set; }

            public string CreatedOn { get; set; }

            public string SubmissionId { get; set; }
        }
    }
}
