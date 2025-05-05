using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.FractionsApp_Web>("webfrontend");

builder.AddProject<Projects.FractionsApp_Maui>("mobilefrontend");

builder.AddProject<Projects.FractionsApp_API>("api");

builder.Build().Run();
