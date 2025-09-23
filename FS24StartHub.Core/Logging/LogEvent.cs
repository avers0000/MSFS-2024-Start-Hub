namespace FS24StartHub.Core.Logging
{
    public class LogEvent
    {
        public DateTime Timestamp { get; init; } = DateTime.UtcNow;
        public LogLevel Level { get; init; } = LogLevel.Info;
        public string Module { get; init; } = "";    // e.g. "Simulator", "Career", "Config"
        public string EventType { get; init; } = ""; // e.g. "CareerSwitched"
        public string Message { get; init; } = "";
        public Dictionary<string, string>? Attributes { get; init; } = null;
    }
}
