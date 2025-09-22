using Microsoft.Extensions.DependencyInjection.Extensions;
using TaskService.BusinessLogic;
using TaskService.BusinessLogic.Interfaces;
using TaskService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDataAccess(builder.Configuration);
builder.Services.TryAddScoped<ITaskManager, TaskManager>();

builder.Services.AddControllers();

// Регистрация сервисов Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
//     app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();