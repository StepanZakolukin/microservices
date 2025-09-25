using Microsoft.Extensions.DependencyInjection;
using NotificationService.ConnectionLib;
using TaskService.BusinessLogic.Interfaces;

namespace TaskService.BusinessLogic;

public static class BusinessLogicStartup
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
    {
        return services
            .AddScoped<ITaskManager, TaskManager>()
            .AddNotificationServiceConnectionLib();
    } 
}