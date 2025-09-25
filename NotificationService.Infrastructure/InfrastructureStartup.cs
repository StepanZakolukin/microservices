using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Infrastructure.Interfaces;
using NotificationService.Infrastructure.Repositories;

namespace NotificationService.Infrastructure;

public static class InfrastructureStartup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationRoot configuration)
    {
        return services
            .AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")))
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<INotificationRepository, NotificationRepository>();
    }
}