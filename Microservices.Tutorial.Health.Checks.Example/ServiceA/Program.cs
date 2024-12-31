using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks()
    .AddRedis(
        redisConnectionString: "localhost:6379",
        name: "Redis Check",
        failureStatus: HealthStatus.Degraded | HealthStatus.Unhealthy,
        tags: ["redis"]
    )
    .AddMongoDb(
        clientFactory: sp => new MongoClient("mongodb://admin:Passw0rd@localhost:27017"),
        name: "MongoDB Check",
        failureStatus: HealthStatus.Degraded | HealthStatus.Unhealthy,
        tags: ["mongodb"]
    )
    .AddNpgSql(
        connectionString: "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=Passw0rd;",
        name: "PostgreSQL",
        healthQuery: "SELECT 1",
        failureStatus: HealthStatus.Degraded | HealthStatus.Unhealthy,
        tags: ["postgresql", "sql", "db"]
    );

var app = builder.Build();
app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
