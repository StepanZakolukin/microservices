using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TaskService.Domain.Interfaces;
using TaskService.Infrastructure.Repositories;

namespace TaskService.Infrastructure;

public static class DataAccessServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services
            .AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("AppDatabase")));
        
        services.TryAddScoped<ITaskRepository, TaskRepository>();
        
        return services;
    }
}