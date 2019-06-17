using SULS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.Services.Contracts
{
    public interface IProblemService
    {
        void CreateProblem(string name, int points, string username);

        List<Problem> GetAllProblems();

        Problem GetById(string problemId);
    }
}
