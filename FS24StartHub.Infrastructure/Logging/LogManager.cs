using FS24StartHub.Core.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FS24StartHub.Infrastructure.Logging;

public class LogManager : ILogManager
{
    private readonly IEnumerable<ILogSink> _sinks;

    public LogManager(IEnumerable<ILogSink> sinks)
    {
        _sinks = sinks;
    }

    public void Log(LogEvent evt)
    {
        string json = JsonSerializer.Serialize(evt, LogJsonDefaults.Options);
        foreach (var sink in _sinks)
            sink.Write(json + Environment.NewLine);
    }

    public void Error(string message, string module, Exception? ex = null)
    {
        var evt = new LogEvent
        {
            Level = LogLevel.Error,
            Module = module,
            EventType = "Error",
            Message = $"{message}{(ex != null ? ": " + ex.Message : "")}",
            Attributes = ex != null ? new() { ["exception"] = ex.GetType().Name } : null
        };
        Log(evt);
    }

    public void Info(string message, string module, string eventType = "Info")
        => Log(new LogEvent { Level = LogLevel.Info, Module = module, EventType = eventType, Message = message });

    public void Warn(string message, string module, string eventType = "Warning")
        => Log(new LogEvent { Level = LogLevel.Warn, Module = module, EventType = eventType, Message = message });

    public void Debug(string message, string module, string eventType = "Debug")
        => Log(new LogEvent { Level = LogLevel.Debug, Module = module, EventType = eventType, Message = message });

    public void Fatal(string message, string module, Exception? ex = null)
    {
        var evt = new LogEvent
        {
            Level = LogLevel.Fatal,
            Module = module,
            EventType = "Fatal",
            Message = $"{message}{(ex != null ? ": " + ex.Message : "")}",
            Attributes = ex != null ? new() { ["exception"] = ex.GetType().Name } : null
        };
        Log(evt);
    }
}