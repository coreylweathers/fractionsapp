using FractionsApp.Data.Context;
using FractionsApp.Data.Interfaces;
using FractionsApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace FractionsApp.Data.Migrations.Repositories
{
    public class ProblemSetRepository : IProblemSetRepository
    {
        private readonly FractionsDbContext _context;

        public ProblemSetRepository(FractionsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProblemSetModel>> GetAllProblemSetsAsync()
        {
            return await _context.ProblemSets
                .Include(p => p.Problems)
                    .ThenInclude(p => p.Operand1)
                .Include(p => p.Problems)
                    .ThenInclude(p => p.Operand2)
                .ToListAsync();
        }

        public async Task<ProblemSetModel> GetProblemSetByIdAsync(Guid id)
        {
            return await _context.ProblemSets
                .Include(p => p.Problems)
                    .ThenInclude(p => p.Operand1)
                .Include(p => p.Problems)
                    .ThenInclude(p => p.Operand2)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ProblemSetModel> CreateProblemSetAsync(ProblemSetModel problemSet)
        {
            await _context.ProblemSets.AddAsync(problemSet);
            await _context.SaveChangesAsync();
            return problemSet;
        }

        public async Task<ProblemSetModel> UpdateProblemSetAsync(ProblemSetModel problemSet)
        {
            _context.Entry(problemSet).State = EntityState.Modified;
            
            // For each problem in the problemSet, update its state accordingly
            foreach (var problem in problemSet.Problems)
            {
                // If the problem has a new ID (or default), add it as a new entity
                if (problem.Id == Guid.Empty)
                {
                    _context.Entry(problem).State = EntityState.Added;
                }
                else
                {
                    _context.Entry(problem).State = EntityState.Modified;
                }
                
                // Handle operands
                if (problem.Operand1 != null)
                {
                    if (problem.Operand1.Id == Guid.Empty)
                    {
                        _context.Entry(problem.Operand1).State = EntityState.Added;
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
                        _context.Entry(problem.Operand2).State = EntityState.Added;
                    }
                    else
                    {
                        _context.Entry(problem.Operand2).State = EntityState.Modified;
                    }
                }
            }
            
            await _context.SaveChangesAsync();
            return problemSet;
        }

        public async Task DeleteProblemSetAsync(Guid id)
        {
            var problemSet = await _context.ProblemSets.FindAsync(id);
            if (problemSet != null)
            {
                _context.ProblemSets.Remove(problemSet);
                await _context.SaveChangesAsync();
            }
        }
    }
}