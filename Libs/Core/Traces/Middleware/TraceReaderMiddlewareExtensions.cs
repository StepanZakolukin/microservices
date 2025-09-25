using Microsoft.AspNetCore.Builder;

namespace Core.Traces.Middleware;

public static class TraceReaderMiddlewareExtensions
{
    public static IApplicationBuilder UseTraceReaderMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TraceReaderMiddleware>();
    }
}