using FractionsApp.Data.MigrationsService.Initializers;
using FractionsApp.Shared.Data.Context;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<DbInitializer>();

builder.Services.AddPooledDbContextFactory<FractionsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("fractions"),
    opts => opts.MigrationsAssembly("FractionsApp.Data.Migrations")));

var host = builder.Build();

await host.RunAsync();