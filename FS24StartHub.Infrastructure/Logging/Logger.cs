using FS24StartHub.Core.Logging;

namespace FS24StartHub.Infrastructure.Logging
{
    public class Logger: ILogger
    {
        private readonly IEnumerable<ILogSink> _sinks;

        public Logger(IEnumerable<ILogSink> sinks)
        {
            _sinks = sinks;
        }

        public void Info(string message) => Write("INFO", message);
        public void Warn(string message) => Write("WARN", message);
        public void Error(string message) => Write("ERROR", message);

        private void Write(string level, string message)
        {
            foreach (var sink in _sinks)
            {
                sink.Write(level, message);
            }
        }
    }
}
