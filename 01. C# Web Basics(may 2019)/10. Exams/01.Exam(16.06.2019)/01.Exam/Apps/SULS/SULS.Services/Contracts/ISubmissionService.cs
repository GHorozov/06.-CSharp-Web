using SULS.Models;

namespace SULS.Services.Contracts
{
    public interface ISubmissionService
    {
        void CreateSubmissionAndAddToProblem(string code, string problemId, string userId);

        Submission GetSubmission(string id);

        void Delete(Submission submission);
    }
}
