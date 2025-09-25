using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureStartup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationRoot configuration)
    {
        return services
            .AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")))
            .AddScoped<ITaskRepository, TaskRepository>()
            .AddScoped<IChangeRepository, ChangeRepository>()
            .AddScoped<IUnitOfWork, UnitOfWork>();
    }
}