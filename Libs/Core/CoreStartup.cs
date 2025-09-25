﻿using Core.HttpLogic;
using Core.Logs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Core;

public static class CoreStartup
{
    public static IServiceCollection AddCore(this IServiceCollection services, IHostBuilder hostBuilder)
    {
        services
            .AddLoggerServices()
            .AddHttpRequestService();
        
        hostBuilder.UseSerilog(
            (builderContext, logConfiguration) => logConfiguration.GetConfiguration(),
            preserveStaticLogger: true);
        
        return services;
    } 
}