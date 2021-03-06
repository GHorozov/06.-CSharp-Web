﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.Models
{
    public class Submission
    {
        public string Id { get; set; }

        public string Code { get; set; }

        public int AchievedResult  { get; set; }

        public DateTime CreatedOn  { get; set; }

        public string ProblemId { get; set; }
        public Problem Problem { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
