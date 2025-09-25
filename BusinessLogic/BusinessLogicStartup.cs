using BusinessLogic.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic;

public static class BusinessLogicStartup
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
    {
        return services.AddScoped<ITaskManager, TaskManager>();
    } 
}