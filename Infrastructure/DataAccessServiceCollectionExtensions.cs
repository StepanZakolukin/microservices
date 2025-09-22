using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Infrastructure;

public static class DataAccessServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services
            .AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("AppDatabase")));
        
        services.TryAddScoped<ITaskRepository, TaskRepository>();
        services.TryAddScoped<IChangeRepository, ChangeRepository>();
        services.TryAddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}