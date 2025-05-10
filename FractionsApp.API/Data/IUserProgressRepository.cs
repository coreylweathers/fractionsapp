using FractionsApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FractionsApp.API.Data
{
    public interface IUserProgressRepository
    {
        Task<List<UserProgressModel>> GetAllUserProgressAsync(string userId);
        Task<UserProgressModel?> GetUserProgressByIdAsync(Guid id);
        Task<Guid> SaveUserProgressAsync(UserProgressModel userProgress);
        Task<bool> DeleteUserProgressAsync(Guid id);
    }
}