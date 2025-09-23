using FS24StartHub.Core.Logging;
using System.Text.Json;

namespace FS24StartHub.Infrastructure.Logging
{
    public class ConsoleLogSink : ILogSink
    {
        public void Write(string line)
        {
            LogEvent? evt = null;
            try
            {
                evt = JsonSerializer.Deserialize<LogEvent>(line, LogJsonDefaults.Options);
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"[LOG ERROR] Failed to parse log line: {line}");
                Console.ResetColor();
                return;
            }

            if (evt == null)
                return;

            var timestamp = evt.Timestamp.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.fff");
            var level = evt.Level.ToString().ToUpper();
            var message = evt.Message;

            switch (level)
            {
                case "ERROR":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "WARN":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "INFO":
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                default:
                    Console.ResetColor();
                    break;
            }

            Console.WriteLine($"{timestamp} [{level}] {message}");
            Console.ResetColor();
        }
    }
}