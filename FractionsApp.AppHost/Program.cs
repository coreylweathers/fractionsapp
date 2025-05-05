using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

// Add PostgreSQL database
var postgres = builder.AddPostgres("postgres")
    .AddDatabase("fractions");

// Add API with database reference
var api = builder.AddProject<Projects.FractionsApp_API>("api")
    .WithReference(postgres);

// Add web frontend with reference to API
var webApp = builder.AddProject<Projects.FractionsApp_Web>("webfrontend")
    .WithReference(api);

// Add mobile frontend with reference to API
// Note: MAUI projects can be a bit tricky with Aspire, as they're not traditional web services
builder.AddProject<Projects.FractionsApp_Maui>("mobilefrontend")
    .WithReference(api);

// Build and run the application
builder.Build().Run();
