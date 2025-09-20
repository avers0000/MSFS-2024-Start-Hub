using FS24StartHub.Core.Logging;

namespace FS24StartHub.Infrastructure.Logging
{
    public class ConsoleLogSink : ILogSink
    {
        public void Write(string level, string message)
        {
            var line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [{level}] {message}";

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

            Console.WriteLine(line);
            Console.ResetColor();
        }

    }
}
