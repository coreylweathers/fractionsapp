using FractionsApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FractionsApp.Maui.Services
{
    public interface ISQLiteService
    {
        Task InitializeDatabaseAsync();
        Task<List<UserProgressModel>> GetAllUserProgressAsync(string userId);
        Task<UserProgressModel> GetUserProgressAsync(Guid id);
        Task<int> SaveUserProgressAsync(UserProgressModel userProgress);
        Task<int> DeleteUserProgressAsync(UserProgressModel userProgress);
        Task<List<UserProgressModel>> GetUnsyncedUserProgressAsync(string userId);
        Task MarkAsSyncedAsync(Guid id);
    }
}