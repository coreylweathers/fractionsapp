using FractionsApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FractionsApp.Maui.Services
{
    public interface ISyncService
    {
        Task<bool> SyncUserProgressAsync(string userId);
    }

    public class SyncService : ISyncService
    {
        private readonly ISQLiteService _sqliteService;
        private readonly HttpClient _httpClient;
        private readonly ILogger<SyncService> _logger;

        public SyncService(ISQLiteService sqliteService, HttpClient httpClient, ILogger<SyncService> logger)
        {
            _sqliteService = sqliteService;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<bool> SyncUserProgressAsync(string userId)
        {
            try
            {
                // Get all unsynced progress items from local SQLite database
                var unsyncedItems = await _sqliteService.GetUnsyncedUserProgressAsync(userId);

                if (unsyncedItems == null || unsyncedItems.Count == 0)
                {
                    _logger.LogInformation("No unsynced items to sync for user {UserId}", userId);
                    return true;
                }

                // Send them to the API server for syncing with PostgreSQL
                var response = await _httpClient.PostAsJsonAsync("api/UserProgress/sync", unsyncedItems);

                if (response.IsSuccessStatusCode)
                {
                    var syncedIds = await response.Content.ReadFromJsonAsync<List<Guid>>();
                    
                    // Mark items as synced in the local database
                    foreach (var id in syncedIds)
                    {
                        await _sqliteService.MarkAsSyncedAsync(id);
                    }
                    
                    _logger.LogInformation("Successfully synced {Count} items for user {UserId}", syncedIds.Count, userId);
                    return true;
                }
                else
                {
                    _logger.LogWarning("Failed to sync user progress. Status: {StatusCode}", response.StatusCode);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error syncing user progress for user {UserId}", userId);
                return false;
            }
        }
    }
}