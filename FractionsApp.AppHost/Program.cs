using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

// Add PostgresSQL database

var postgresDb = builder.AddPostgres("postgres")
    .WithDataVolume()
    .WithPgWeb()
    .AddDatabase("fractions");

// Add migrations service for database initialization
var migrationsService = builder.AddProject<Projects.FractionsApp_Data_MigrationsService>("migrations")
    .WithReference(postgresDb)
    .WaitFor(postgresDb);

// Add API with database reference
var api = builder.AddProject<Projects.FractionsApp_API>("api")
    .WithReference(postgresDb)
    .WaitFor(migrationsService);

// Add web frontend with reference to API
var webApp = builder.AddProject<Projects.FractionsApp_Web>("webfrontend")
    .WithReference(api)
    .WaitFor(migrationsService);

// Build and run the application
await builder.Build().RunAsync();
