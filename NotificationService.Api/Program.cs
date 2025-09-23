using Core.Logs;
using Core.Traces.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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