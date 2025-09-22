namespace Core.Trace.TraceLogic.Interfaces;

public interface ITraceWriter
{
    string Name { get; }
    
    string GetValue();
}