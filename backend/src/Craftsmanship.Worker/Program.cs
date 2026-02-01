using Craftsmanship.Infrastructure.Persistence;
using Craftsmanship.Worker;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("CraftsmanshipDb")
    ?? "Data Source=../craftsmanship.db";

builder.Services.AddDbContext<CraftsmanshipDbContext>(options =>
{
    //options.UseSqlite("Data Source=../craftsmanship.db");
    options.UseSqlite(connectionString, sqlite =>
    {
        sqlite.CommandTimeout(30);
    });
});

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
