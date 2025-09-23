using FS24StartHub.Core.Logging;
using FS24StartHub.Core.Storage;

namespace FS24StartHub.Infrastructure.Logging
{
    public class JsonFileLogSink : ILogSink
    {
        private readonly IFileStorage _fileStorage;
        private readonly string _baseFolderPath;

        public JsonFileLogSink(IFileStorage fileStorage, string baseFolderPath)
        {
            _fileStorage = fileStorage;
            _baseFolderPath = baseFolderPath;
        }

        public void Write(string line)
        {
            string logFolder = Path.Combine(_baseFolderPath, "Logs");
            string fileName = $"log_{DateTime.UtcNow:yyyy-MM-dd}.jsonl";
            string fullPath = Path.Combine(logFolder, fileName);

            _fileStorage.AppendAllText(fullPath, line);
        }
    }
}