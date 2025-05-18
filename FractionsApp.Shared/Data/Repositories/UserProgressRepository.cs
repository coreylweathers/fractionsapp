using FractionsApp.Shared.Data.Context;
using FractionsApp.Shared.Data.Interfaces;
using FractionsApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace FractionsApp.Shared.Data.Repositories;

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

    Task<IEnumerable<UserProgressModel>> IUserProgressRepository.GetAllUserProgressAsync()
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<UserProgressModel>> IUserProgressRepository.GetUserProgressByUserIdAsync(string userId)
    {
        throw new NotImplementedException();
    }

    Task<UserProgressModel> IUserProgressRepository.CreateUserProgressAsync(UserProgressModel userProgress)
    {
        throw new NotImplementedException();
    }

    Task<UserProgressModel> IUserProgressRepository.UpdateUserProgressAsync(UserProgressModel userProgress)
    {
        throw new NotImplementedException();
    }

    Task IUserProgressRepository.DeleteUserProgressAsync(Guid id)
    {
        return DeleteUserProgressAsync(id);
    }
}