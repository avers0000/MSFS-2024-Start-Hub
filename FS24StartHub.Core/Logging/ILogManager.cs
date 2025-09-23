namespace FS24StartHub.Core.Logging
{
    public interface ILogManager
    {
        void Log(LogEvent evt);

        void Debug(string message, string module, string eventType = "Debug");
        void Info(string message, string module, string eventType = "Info");
        void Warn(string message, string module, string eventType = "Warning");
        void Error(string message, string module, Exception? ex = null);
        void Fatal(string message, string module, Exception? ex = null);
    }
}