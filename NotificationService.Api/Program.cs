using Core;
using Core.Traces.Middleware;
using NotificationService.BusinessLogic;
using NotificationService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Подключаем Core сервисы, бизнесс-логику, инфроструктуру
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