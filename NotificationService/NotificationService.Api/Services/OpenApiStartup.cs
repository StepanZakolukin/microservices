using System.Reflection;

namespace NotificationService.Api.Services;

public static class OpenApiStartup
{
    public static IServiceCollection AddOpenApi(this IServiceCollection services)
    {
        return services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddSwaggerGen(swaggerGenOptions =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swaggerGenOptions.IncludeXmlComments(xmlPath);
            });;
    }
    
    
    public static IApplicationBuilder UseOpenApi(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        
        return app;
    }
}