using NotificationService.Api.Services.Interfaces;

namespace NotificationService.Api.Services;

public static class NotificationServiceStartup
{
    public static IServiceCollection AddNotifications(this IServiceCollection services)
    {
        services.AddSignalR();
        services.AddScoped<INotificationService, NotificationService>();
        
        return services;
    }
}