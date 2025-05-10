using FractionsApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FractionsApp.API.Data
{
    public class UserProgressRepository : IUserProgressRepository
    {
        private readonly FractionsDbContext _context;

        public UserProgressRepository(FractionsDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserProgressModel>> GetAllUserProgressAsync(string userId)
        {
            return await _context.UserProgress
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CompletedDate)
                .ToListAsync();
        }

        public async Task<UserProgressModel?> GetUserProgressByIdAsync(Guid id)
        {
            return await _context.UserProgress.FindAsync(id);
        }

        public async Task<Guid> SaveUserProgressAsync(UserProgressModel userProgress)
        {
            if (userProgress.Id == Guid.Empty)
            {
                // It's a new record
                userProgress.Id = Guid.NewGuid();
                _context.UserProgress.Add(userProgress);
            }
            else
            {
                // It's an update
                _context.Entry(userProgress).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
            return userProgress.Id;
        }

        public async Task<bool> DeleteUserProgressAsync(Guid id)
        {
            var progress = await _context.UserProgress.FindAsync(id);
            if (progress == null)
                return false;

            _context.UserProgress.Remove(progress);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}