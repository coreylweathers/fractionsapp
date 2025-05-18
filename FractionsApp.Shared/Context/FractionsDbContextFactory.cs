using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FractionsApp.Shared.Data.Context;

public class FractionsDbContextFactory : IDesignTimeDbContextFactory<FractionsDbContext>
{
    public FractionsDbContext CreateDbContext(string[] args)
    {
        // For migrations only - real connection is configured through Aspire
        var connectionString = "Host=localhost;Database=fractions;Username=postgres;Password=postgres";

        var optionsBuilder = new DbContextOptionsBuilder<FractionsDbContext>();
        optionsBuilder.UseNpgsql(connectionString);
        return new FractionsDbContext(optionsBuilder.Options);
    }
}