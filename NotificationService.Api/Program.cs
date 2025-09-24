using System.Net.Mime;
using Core;
using Core.Traces.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Подключаем Core сервисы
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