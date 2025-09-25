using Microsoft.Extensions.DependencyInjection;
using NotificationService.BusinessLogic.Interfaces;

namespace NotificationService.BusinessLogic;

public static class BusinessLogicStartup
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
    {
        return services
            .AddScoped<INotificationManager, NotificationManager>();
    }  
}