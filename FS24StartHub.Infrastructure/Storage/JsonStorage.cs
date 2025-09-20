using FS24StartHub.Core.Storage;
using System.Text.Json;

namespace FS24StartHub.Infrastructure.Storage
{
    public class JsonStorage : IJsonStorage
    {
        private readonly IFileStorage _fileStorage;

        public JsonStorage(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        public T Load<T>(string path)
        {
            var json = _fileStorage.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(json)!;
        }

        public bool TryLoad<T>(string path, out T? result)
        {
            try
            {
                result = Load<T>(path);
                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }

        public void Save<T>(string path, T data)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            _fileStorage.WriteAllText(path, json);
        }
    }
}
