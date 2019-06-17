using Microsoft.EntityFrameworkCore;
using SULS.Data;
using SULS.Models;
using SULS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SULS.Services
{
    public class ProblemService : IProblemService
    {
        private readonly SULSContext context;

        public ProblemService(SULSContext context)
        {
            this.context = context;
        }

        public void CreateProblem(string name, int points, string username)
        {
            var userIdFromDb = this.context.Users.Where(x => x.Username == username).Select(x => x.Id).FirstOrDefault();
            if (userIdFromDb == null)
            {
                return;
            }

            var problem = new Problem()
            {
                Name = name,
                Points = points,
                UserId = userIdFromDb
            };

            this.context.Problems.Add(problem);
            this.context.SaveChanges();
        }

        public List<Problem> GetAllProblems()
        {
            return this.context
                .Problems
                .Include(x => x.Submissions)
                .ToList();
        }

        public Problem GetById(string problemId)
        {
            var problemFromDb = this.context
                .Problems
                .Where(x => x.Id == problemId)
                .Include(x => x.Submissions)
                .ThenInclude(x => x.User)
                .FirstOrDefault();

            if(problemFromDb == null)
            {
                return null;
            }

            return problemFromDb;
        }
    }
}
