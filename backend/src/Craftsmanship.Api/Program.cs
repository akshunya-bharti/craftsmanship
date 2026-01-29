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

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactApp",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
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

app.UseCors("ReactApp");

// Map Controllers (CRITICAL)
app.MapControllers();

// Optional: simple health check root
app.MapGet("/", () => "Craftsmanship API is running");

app.Run();