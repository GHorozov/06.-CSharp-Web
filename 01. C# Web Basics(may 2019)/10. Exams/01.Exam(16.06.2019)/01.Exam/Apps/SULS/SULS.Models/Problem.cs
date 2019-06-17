using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.Models
{
    public class Problem
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Points { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public List<Submission> Submissions { get; set; } = new List<Submission>();
    }
}
