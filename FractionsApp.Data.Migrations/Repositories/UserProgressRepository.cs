using FractionsApp.Data.Context;
using FractionsApp.Data.Interfaces;
using FractionsApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace FractionsApp.Data.Migrations.Repositories
{
    public class UserProgressRepository : IUserProgressRepository
    {
        private readonly FractionsDbContext _context;

        public UserProgressRepository(FractionsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserProgressModel>> GetAllUserProgressAsync()
        {
            return await _context.UserProgress.ToListAsync();
        }

        public async Task<IEnumerable<UserProgressModel>> GetUserProgressByUserIdAsync(string userId)
        {
            return await _context.UserProgress
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public async Task<UserProgressModel> GetUserProgressByIdAsync(Guid id)
        {
            return await _context.UserProgress.FindAsync(id);
        }

        public async Task<UserProgressModel> CreateUserProgressAsync(UserProgressModel userProgress)
        {
            await _context.UserProgress.AddAsync(userProgress);
            await _context.SaveChangesAsync();
            return userProgress;
        }

        public async Task<UserProgressModel> UpdateUserProgressAsync(UserProgressModel userProgress)
        {
            _context.Entry(userProgress).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return userProgress;
        }

        public async Task DeleteUserProgressAsync(Guid id)
        {
            var userProgress = await _context.UserProgress.FindAsync(id);
            if (userProgress != null)
            {
                _context.UserProgress.Remove(userProgress);
                await _context.SaveChangesAsync();
            }
        }
    }
}