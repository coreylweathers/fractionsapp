using FractionsApp.Shared.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FractionsApp.Maui.Services
{
    public class SQLiteService : ISQLiteService
    {
        private SQLiteAsyncConnection _database;
        private readonly string _databasePath;

        public SQLiteService()
        {
            _databasePath = Path.Combine(FileSystem.AppDataDirectory, "fractions.db3");
        }

        private async Task SetupDatabaseAsync()
        {
            if (_database != null)
                return;

            _database = new SQLiteAsyncConnection(_databasePath);
            await _database.CreateTableAsync<UserProgressModel>();
        }

        public async Task InitializeDatabaseAsync()
        {
            await SetupDatabaseAsync();
        }

        public async Task<List<UserProgressModel>> GetAllUserProgressAsync(string userId)
        {
            await SetupDatabaseAsync();
            return await _database.Table<UserProgressModel>().Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task<UserProgressModel> GetUserProgressAsync(Guid id)
        {
            await SetupDatabaseAsync();
            return await _database.Table<UserProgressModel>().Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveUserProgressAsync(UserProgressModel userProgress)
        {
            await SetupDatabaseAsync();
            if (userProgress.Id != Guid.Empty)
                return await _database.UpdateAsync(userProgress);
            else
            {
                userProgress.CompletedDate = DateTime.Now;
                userProgress.IsSynced = false;
                return await _database.InsertAsync(userProgress);
            }
        }

        public async Task<int> DeleteUserProgressAsync(UserProgressModel userProgress)
        {
            await SetupDatabaseAsync();
            return await _database.DeleteAsync(userProgress);
        }

        public async Task<List<UserProgressModel>> GetUnsyncedUserProgressAsync(string userId)
        {
            await SetupDatabaseAsync();
            return await _database.Table<UserProgressModel>().Where(p => p.UserId == userId && !p.IsSynced).ToListAsync();
        }

        public async Task MarkAsSyncedAsync(Guid id)
        {
            await SetupDatabaseAsync();
            var progress = await GetUserProgressAsync(id);
            if (progress != null)
            {
                progress.IsSynced = true;
                await _database.UpdateAsync(progress);
            }
        }
    }
}