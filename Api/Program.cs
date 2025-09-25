using Core.Traces.Middleware;
using BusinessLogic;
using Infrastructure;
using Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Подключаем Core сервисы, бизнесс-логику, инфраструктуру
builder.Services
    .AddCore(builder.Host)
    .AddBusinessLogic()
    .AddInfrastructure(builder.Configuration);

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