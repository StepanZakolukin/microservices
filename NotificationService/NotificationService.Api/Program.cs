using Core;
using Core.Traces.Middleware;
using NotificationService.Api.Hubs;
using NotificationService.Api.Services;
using NotificationService.BusinessLogic;
using NotificationService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Подключаем SignalR
builder.Services.AddSignalR();

// Подключаем Core сервисы, уведомления, бизнесс-логику, инфроструктуру
builder.Services
    .AddCore(builder.Host)
    .AddNotifications()
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

app.MapHub<NotificationHub>("/hubs/notifications");
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseTraceReaderMiddleware();
app.MapControllers();
app.Run();