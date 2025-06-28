using FractionsApp.Shared.Data.Context;
using FractionsApp.Shared.Data.Interfaces;
using FractionsApp.Shared.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FractionsApp.Data.MigrationsService.Extensions;

// TODO: USE AN EXTENSION BLOCK (PER C# 14) 
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFractionsDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Get the PostgreSQL connection string
        // In Aspire, the standard connection string name is "postgres"
        var connectionString = configuration.GetConnectionString("postgres");
            
        // Fallback to other connection string names if postgres is not found
        if (string.IsNullOrEmpty(connectionString))
        {
            connectionString = configuration.GetConnectionString("fractions");
        }
            
        if (string.IsNullOrEmpty(connectionString))
        {
            // Create a temporary service provider to get a logger
            using var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<FractionsDbContext>>();
            logger?.LogWarning("No database connection string found in configuration. Database operations will fail.");
                
            // For development purposes only, provide a default connection string
            // This should NEVER be used in production and is just a fallback for development
            connectionString = "Host=localhost;Database=fractions;Username=postgres;Password=postgres";
            logger?.LogWarning("Using default development connection string as fallback: {Host}", "localhost");
        }
        else
        {
            // Create a temporary service provider to get a logger
            using var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<FractionsDbContext>>();
                
            // Log the connection string (with password removed) for debugging
            var sanitizedConnectionString = SanitizeConnectionString(connectionString);
            logger?.LogInformation("Using PostgreSQL connection string: {ConnectionString}", sanitizedConnectionString);
        }

        // Configure the DbContext with the connection string
        services.AddDbContext<FractionsDbContext>(options =>
            options.UseNpgsql(connectionString));

        // Register repositories
        services.AddScoped<IProblemSetRepository, ProblemSetRepository>();
        services.AddScoped<IUserProgressRepository, UserProgressRepository>();

        return services;
    }
        
    // Helper method to remove password from connection string for logging
    private static string SanitizeConnectionString(string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
            return string.Empty;
                
        // Simple approach to sanitize the connection string - replace password with asterisks
        if (connectionString.Contains("Password="))
        {
            var parts = connectionString.Split(';');
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i].StartsWith("Password=", StringComparison.OrdinalIgnoreCase))
                {
                    parts[i] = "Password=********";
                }
            }
            return string.Join(";", parts);
        }
            
        return connectionString;
    }
}