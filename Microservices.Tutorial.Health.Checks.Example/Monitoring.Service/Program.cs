using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecksUI(settings =>
{
    // burayı yorum satırına alıp gerekli konfigrsyonu appsettings.json'da da yapabilirsin
    settings.AddHealthCheckEndpoint("Service A", "http://localhost:5038/health");
    settings.AddHealthCheckEndpoint("Service B", "http://localhost:5042/health");
    settings.SetEvaluationTimeInSeconds(3);
    settings.SetApiMaxActiveRequests(3);
    settings.ConfigureApiEndpointHttpclient((serviceProvider, httpClient) =>
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "....");
    })
    .ConfigureWebhooksEndpointHttpclient((serviceProvider, httpClient) =>
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "....");
    });
}
).AddSqlServerStorage("Server=localhost, 1433;Database=HealthCheckUIDB;User ID=SA;Password=Passw0rd;TrustServerCertificate=True");

var app = builder.Build();

app.UseHealthChecksUI(options =>
{
    options.UIPath = "/health-ui";
    // options.AddCustomStylesheet("health-check-ui.css");
});

app.Run();
