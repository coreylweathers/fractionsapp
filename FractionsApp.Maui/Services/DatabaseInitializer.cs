using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FractionsApp.Maui.Services
{
    public interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }

    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly ISQLiteService _sqliteService;
        private readonly ILogger<DatabaseInitializer> _logger;

        public DatabaseInitializer(ISQLiteService sqliteService, ILogger<DatabaseInitializer> logger)
        {
            _sqliteService = sqliteService;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            _logger.LogInformation("Initializing local SQLite database");
            await _sqliteService.InitializeDatabaseAsync();
        }
    }
}