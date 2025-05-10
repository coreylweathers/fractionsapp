using FractionsApp.Shared.Models;

namespace FractionsApp.Data.Interfaces
{
    public interface IProblemSetRepository
    {
        Task<IEnumerable<ProblemSetModel>> GetAllProblemSetsAsync();
        Task<ProblemSetModel> GetProblemSetByIdAsync(Guid id);
        Task<ProblemSetModel> CreateProblemSetAsync(ProblemSetModel problemSet);
        Task<ProblemSetModel> UpdateProblemSetAsync(ProblemSetModel problemSet);
        Task DeleteProblemSetAsync(Guid id);
    }
}