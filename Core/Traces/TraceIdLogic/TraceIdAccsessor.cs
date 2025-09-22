using Core.Trace.TraceLogic.Interfaces;
using Serilog.Context;

namespace Core.Traces.TraceIdLogic;

internal class TraceIdAccessor : ITraceReader, ITraceWriter, ITraceIdAccessor
{
    public string Name => "TraceId";

    private string _value;
    
    public string GetValue()
    {
        return _value;
    }

    public void WriteValue(string value)
    {
        _value = value;
        LogContext.PushProperty("TraceId", value);
    }
}