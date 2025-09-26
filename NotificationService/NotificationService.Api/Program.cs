using Core;
using Core.Traces.Middleware;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using NotificationService.Api.Services;
using NotificationService.BusinessLogic;
using NotificationService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Подключаем Core сервисы, уведомления, бизнесс-логику, инфроструктуру, OpenApi
builder.Services
    .AddCore(builder.Host)
    .AddNotifications()
    .AddBusinessLogic()
    .AddInfrastructure(builder.Configuration)
    .AddOpenApi();

builder.Services.AddHealthChecks()
    .AddCheck(name: "self", () => HealthCheckResult.Healthy())
    .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection"), name: "postgres");

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
}

app.MapHealthChecks("/notification-service/health");
app.UseNotifications();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseTraceReaderMiddleware();
app.MapControllers();
app.Run();