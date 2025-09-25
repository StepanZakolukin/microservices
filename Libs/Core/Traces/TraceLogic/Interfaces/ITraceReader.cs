namespace Core.Traces.TraceLogic.Interfaces;

public interface ITraceReader
{
    string Name { get; }

    void WriteValue(string value);
}