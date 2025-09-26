using Core;
using Core.Traces.Middleware;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using TaskService.Api.Services;
using TaskService.Infrastructure;
using TaskService.BusinessLogic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Подключаем Core сервисы, бизнесс-логику, инфраструктуру, OpenApi
builder.Services
    .AddCore(builder.Host)
    .AddBusinessLogic()
    .AddInfrastructure(builder.Configuration)
    .AddOpenApi();

builder.Services.AddHealthChecks()
    .AddCheck(name: "self", () => HealthCheckResult.Healthy())
    .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection"), name: "postgres")
    .AddUrlGroup(new Uri("https://localhost:5004/notification-service/health"), name: "notification-service");

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
}

app.MapHealthChecks("/tasks-service/health");
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseTraceReaderMiddleware();
app.MapControllers();
app.Run();