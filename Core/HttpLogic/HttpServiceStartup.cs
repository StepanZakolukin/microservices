using Core.HttpLogic.HttpBodyLogic;
using Core.HttpLogic.HttpBodyLogic.Interfaces;
using Core.HttpLogic.Services;
using Core.HttpLogic.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Core.HttpLogic;

public static class HttpServiceStartup
{
    public static IServiceCollection AddHttpRequestService(this IServiceCollection services)
    {
        services
            .AddHttpContextAccessor()
            .AddHttpClient();
        
        services.TryAddTransient<IHttpRequestService, HttpRequestService>();
        services.AddTransient<IHttpConnectionService, HttpConnectionService>();
        services.AddTransient<IHttpContentPacker, HttpContentPackerToJsonFormat>();
        services.AddTransient<IUnpackerHttpResponse, JsonUnpackerHttpResponse>();
        
        return services;
    }
}