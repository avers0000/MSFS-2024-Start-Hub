using FS24StartHub.Core.Logging;

namespace FS24StartHub.Infrastructure.Logging
{
    public class FileLogSink: ILogSink
    {
        private readonly string _logDirectory;

        public FileLogSink(string baseFolderPath)
        {
            _logDirectory = Path.Combine(baseFolderPath, "Logs");
            Directory.CreateDirectory(_logDirectory);
        }

        public void Write(string level, string message)
        {
            var logFilePath = Path.Combine(_logDirectory, $"log-{DateTime.UtcNow:yyyyMMdd}.txt");

            var line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [{level}] {message}";
            File.AppendAllText(logFilePath, line + Environment.NewLine);
        }

    }
}
