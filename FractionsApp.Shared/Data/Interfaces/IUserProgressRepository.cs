using FractionsApp.Shared.Models;

namespace FractionsApp.Shared.Data.Interfaces;

public interface IUserProgressRepository
{
    Task<IEnumerable<UserProgressModel>> GetAllUserProgressAsync();
    Task<IEnumerable<UserProgressModel>> GetUserProgressByUserIdAsync(string userId);
    Task<UserProgressModel> GetUserProgressByIdAsync(Guid id);
    Task<UserProgressModel> CreateUserProgressAsync(UserProgressModel userProgress);
    Task<UserProgressModel> UpdateUserProgressAsync(UserProgressModel userProgress);
    Task DeleteUserProgressAsync(Guid id);
}