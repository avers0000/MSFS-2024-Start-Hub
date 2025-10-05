using FS24StartHub.Core.Storage;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FS24StartHub.Infrastructure.Storage
{
    public class JsonStorage : IJsonStorage
    {
        private readonly IFileStorage _fileStorage;
        private readonly JsonSerializerOptions _jsonOptions;

        public JsonStorage(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() }
            };
        }

        public T Load<T>(string path)
        {
            var json = _fileStorage.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(json, _jsonOptions)!;
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
            var json = JsonSerializer.Serialize(data, _jsonOptions);
            _fileStorage.WriteAllText(path, json);
        }
    }
}
