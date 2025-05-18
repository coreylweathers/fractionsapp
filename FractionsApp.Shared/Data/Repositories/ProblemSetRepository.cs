using FractionsApp.Shared.Data.Context;
using FractionsApp.Shared.Data.Interfaces;
using FractionsApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace FractionsApp.Shared.Data.Repositories;

public class ProblemSetRepository : IProblemSetRepository
{
    private readonly FractionsDbContext _context;

    public ProblemSetRepository(FractionsDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProblemSetModel>> GetAllProblemSetsAsync()
    {
        return await _context.ProblemSets
            .Include(p => p.Problems)
            .ThenInclude(p => p.Operand1)
            .Include(p => p.Problems)
            .ThenInclude(p => p.Operand2)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<ProblemSetModel?> GetProblemSetByIdAsync(Guid id)
    {
        return await _context.ProblemSets
            .Include(p => p.Problems)
            .ThenInclude(p => p.Operand1)
            .Include(p => p.Problems)
            .ThenInclude(p => p.Operand2)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<ProblemSetModel>> GetProblemSetsByCategoryAsync(string category)
    {
        return await _context.ProblemSets
            .Include(p => p.Problems)
            .ThenInclude(p => p.Operand1)
            .Include(p => p.Problems)
            .ThenInclude(p => p.Operand2)
            .Where(p => p.Category == category)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<List<ProblemSetModel>> GetProblemSetsByDifficultyAsync(string difficulty)
    {
        return await _context.ProblemSets
            .Include(p => p.Problems)
            .ThenInclude(p => p.Operand1)
            .Include(p => p.Problems)
            .ThenInclude(p => p.Operand2)
            .Where(p => p.Difficulty == difficulty)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<Guid> SaveProblemSetAsync(ProblemSetModel problemSet)
    {
        if (problemSet.Id == Guid.Empty)
        {
            // It's a new record
            problemSet.Id = Guid.NewGuid();
            problemSet.CreatedDate = DateTime.Now;
            _context.ProblemSets.Add(problemSet);
        }
        else
        {
            // It's an update
            problemSet.ModifiedDate = DateTime.Now;
            _context.Entry(problemSet).State = EntityState.Modified;

            // Handle the child collection - problems
            var existingProblems = await _context.FractionProblems
                .Where(p => p.ProblemSetId == problemSet.Id)
                .ToListAsync();

            // Remove problems that are not in the incoming model
            foreach (var existingProblem in existingProblems)
            {
                if (!problemSet.Problems.Any(p => p.Id == existingProblem.Id))
                {
                    _context.FractionProblems.Remove(existingProblem);
                }
            }

            // Add or update problems
            foreach (var problem in problemSet.Problems)
            {
                if (problem.Id == Guid.Empty)
                {
                    // New problem
                    problem.Id = Guid.NewGuid();
                    problem.ProblemSetId = problemSet.Id;
                    _context.FractionProblems.Add(problem);
                }
                else
                {
                    // Update existing problem
                    _context.Entry(problem).State = EntityState.Modified;
                }

                // Handle fractions
                if (problem.Operand1 != null)
                {
                    if (problem.Operand1.Id == Guid.Empty)
                    {
                        problem.Operand1.Id = Guid.NewGuid();
                        _context.Fractions.Add(problem.Operand1);
                    }
                    else
                    {
                        _context.Entry(problem.Operand1).State = EntityState.Modified;
                    }
                }

                if (problem.Operand2 != null)
                {
                    if (problem.Operand2.Id == Guid.Empty)
                    {
                        problem.Operand2.Id = Guid.NewGuid();
                        _context.Fractions.Add(problem.Operand2);
                    }
                    else
                    {
                        _context.Entry(problem.Operand2).State = EntityState.Modified;
                    }
                }
            }
        }

        await _context.SaveChangesAsync();
        return problemSet.Id;
    }

    public async Task<bool> DeleteProblemSetAsync(Guid id)
    {
        var problemSet = await _context.ProblemSets.FindAsync(id);
        if (problemSet == null)
            return false;

        _context.ProblemSets.Remove(problemSet);
        await _context.SaveChangesAsync();
        return true;
    }

    Task<IEnumerable<ProblemSetModel>> IProblemSetRepository.GetAllProblemSetsAsync()
    {
        throw new NotImplementedException();
    }

    Task<ProblemSetModel> IProblemSetRepository.CreateProblemSetAsync(ProblemSetModel problemSet)
    {
        throw new NotImplementedException();
    }

    Task<ProblemSetModel> IProblemSetRepository.UpdateProblemSetAsync(ProblemSetModel problemSet)
    {
        throw new NotImplementedException();
    }

    Task IProblemSetRepository.DeleteProblemSetAsync(Guid id)
    {
        return DeleteProblemSetAsync(id);
    }
}