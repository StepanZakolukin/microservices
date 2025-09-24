using Core.Logs;
using Core.Traces.Middleware;
using BusinessLogic;
using Infrastructure;
using BusinessLogic.Interfaces;
using Core;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDataAccess(builder.Configuration);
builder.Services.TryAddScoped<ITaskManager, TaskManager>();

//Подключаем Core сервисы
builder.Services.AddCore(builder.Host);

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