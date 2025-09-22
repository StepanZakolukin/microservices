using Core.Trace.TraceLogic.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Core.Traces.Middleware;

/// <summary>
/// Middleware для чтения троссировочных значений запроса
/// </summary>
internal class TraceReaderMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IEnumerable<ITraceReader> _traceReaderList;

    public TraceReaderMiddleware(RequestDelegate next, IEnumerable<ITraceReader> traceReaderList)
    {
        _next = next;
        _traceReaderList = traceReaderList;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        foreach (var traceReader in _traceReaderList)
        {
            var traceId = context.Request.Headers[traceReader.Name].FirstOrDefault() ?? Guid.NewGuid().ToString();
            traceReader.WriteValue(traceId);
        }
        
        await _next(context);
    }
}