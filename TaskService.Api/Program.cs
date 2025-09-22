using Serilog;
using Core.Logs;
using Core.Traces.Middleware;
using TaskService.BusinessLogic;
using TaskService.Infrastructure;
using TaskService.BusinessLogic.Interfaces;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDataAccess(builder.Configuration);
builder.Services.TryAddScoped<ITaskManager, TaskManager>();

// Настраиваем логирование, на основе Serilog
builder.Services.AddLoggerServices();
builder.Host.UseSerilog(
    (builderContext, logConfiguration) => logConfiguration.GetConfiguration(),
    preserveStaticLogger: true);

builder.Services.AddControllers();

// Регистрация сервисов Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseTraceReaderMiddleware();
app.MapControllers();
app.Run();