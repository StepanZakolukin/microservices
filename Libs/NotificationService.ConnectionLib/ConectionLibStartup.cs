using Microsoft.Extensions.DependencyInjection;
using NotificationService.ConnectionLib.Interfaces;

namespace NotificationService.ConnectionLib;

public static class ConectionLibStartup
{
    public static IServiceCollection AddNotificationServiceConnectionLib(this IServiceCollection services)
    {
        return services
            .AddScoped<INotificationServiceConnection, NotificationServiceConnection>();
    }
}