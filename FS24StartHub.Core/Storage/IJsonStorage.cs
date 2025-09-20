namespace FS24StartHub.Core.Storage
{
    public interface IJsonStorage
    {
        T? Load<T>(string path);
        bool TryLoad<T>(string path, out T? result);
        void Save<T>(string path, T data);
    }
}
