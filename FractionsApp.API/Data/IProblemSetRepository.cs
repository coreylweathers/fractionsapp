using FractionsApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FractionsApp.API.Data
{
    public interface IProblemSetRepository
    {
        Task<List<ProblemSetModel>> GetAllProblemSetsAsync();
        Task<ProblemSetModel?> GetProblemSetByIdAsync(Guid id);
        Task<List<ProblemSetModel>> GetProblemSetsByCategoryAsync(string category);
        Task<List<ProblemSetModel>> GetProblemSetsByDifficultyAsync(string difficulty);
        Task<Guid> SaveProblemSetAsync(ProblemSetModel problemSet);
        Task<bool> DeleteProblemSetAsync(Guid id);
    }
}