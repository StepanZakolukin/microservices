using Core;
using Core.Traces.Middleware;
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

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseTraceReaderMiddleware();
app.MapControllers();
app.Run();