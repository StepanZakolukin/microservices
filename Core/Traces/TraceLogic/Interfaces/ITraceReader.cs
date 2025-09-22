namespace Core.Trace.TraceLogic.Interfaces;

public interface ITraceReader
{
    string Name { get; }

    void WriteValue(string value);
}