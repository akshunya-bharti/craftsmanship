using Craftsmanship.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// Services
// --------------------

// Enable Controllers (REQUIRED for IngestionController)
builder.Services.AddControllers()
.AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter()
        );
    });

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database (EF Core + SQLite)
var connectionString = builder.Configuration.GetConnectionString("CraftsmanshipDb");

builder.Services.AddDbContext<CraftsmanshipDbContext>(options =>
{
    options.UseSqlite(connectionString);
});

var app = builder.Build();

// --------------------
// Middleware pipeline
// --------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Map Controllers (CRITICAL)
app.MapControllers();

// Optional: simple health check root
app.MapGet("/", () => "Craftsmanship API is running");

app.Run();