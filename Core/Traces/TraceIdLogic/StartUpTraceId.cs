using Core.Trace.TraceLogic.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Core.Traces.TraceIdLogic;

public static class StartUpTraceId 
{
    public static IServiceCollection TryAddTraceId(this IServiceCollection services)
    {
        services.AddScoped<TraceIdAccessor>();
        services
            .TryAddScoped<ITraceWriter>(provider => ServiceProviderServiceExtensions.GetRequiredService<TraceIdAccessor>(provider));
        services
            .TryAddScoped<ITraceReader>(provider => provider.GetRequiredService<TraceIdAccessor>());
        services
            .TryAddScoped<ITraceIdAccessor>(provider => provider.GetRequiredService<TraceIdAccessor>());

        return services;
    }
}