using SULS.Data;
using SULS.Models;
using SULS.Services.Contracts;
using System;
using System.Linq;

namespace SULS.Services
{
    public class SubmissionService : ISubmissionService
    {
        private readonly SULSContext context;
        private Random random = new Random();


        public SubmissionService(SULSContext context)
        {
            this.context = context;
        }

        public void CreateSubmissionAndAddToProblem(string code, string problemId, string userId)
        {
            var problemFromDb = this.context
                .Problems
                .Where(x => x.Id == problemId)
                .FirstOrDefault();

            var userFromDb = this.context
                .Users
                .Where(x => x.Id == userId)
                .FirstOrDefault();


            if (problemFromDb == null || userFromDb == null)
            {
                return;
            }

            var submission = new Submission()
            {
                ProblemId = problemFromDb.Id,
                Code = code,
                CreatedOn = DateTime.UtcNow,
                Problem = problemFromDb,
                UserId = userId,
                AchievedResult = this.random.Next(0, problemFromDb.Points),
                User = userFromDb
            };

            this.context.Submissions.Add(submission);
            this.context.SaveChanges();

            problemFromDb.Submissions.Add(submission);
            this.context.Update(problemFromDb);
            this.context.SaveChanges();
        }

        public Submission GetSubmission(string id)
        {
            return this.context
                .Submissions
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public void Delete(Submission submission)
        {
            this.context
                .Submissions
                .Remove(submission);

            this.context.SaveChanges();
        }
    }
}
