using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using migration_from_ERD.Data;
using migration_from_ERD.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

EnvFile.Load(Path.Combine(builder.Environment.ContentRootPath, ".env"));

var databaseSettings = builder.Configuration.GetSection(DatabaseSettings.SectionName)
    .Get<DatabaseSettings>()
    ?? throw new InvalidOperationException("Database configuration is missing.");

var databaseUsername = Environment.GetEnvironmentVariable("DB_USERNAME")
    ?? throw new InvalidOperationException("DB_USERNAME is missing.");
var databasePassword = Environment.GetEnvironmentVariable("DB_PASSWORD")
    ?? throw new InvalidOperationException("DB_PASSWORD is missing.");

var connectionString = new SqlConnectionStringBuilder
{
    DataSource = $"{databaseSettings.Host},{databaseSettings.Port}",
    InitialCatalog = databaseSettings.Name,
    UserID = databaseUsername,
    Password = databasePassword,
    Encrypt = databaseSettings.Encrypt,
    TrustServerCertificate = databaseSettings.TrustServerCertificate
}.ConnectionString;

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
