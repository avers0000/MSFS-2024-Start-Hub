namespace FS24StartHub.Core.Storage
{
    public interface IFileStorage
    {
        bool FileExists(string path);
        bool DirectoryExists(string path);

        string ReadAllText(string path);
        string[] ReadAllLines(string path);

        void WriteAllText(string path, string contents);

        void CreateDirectory(string path);

        IEnumerable<string> EnumerateFiles(string path, string searchPattern = "*.*");

        IEnumerable<string> EnumerateDirectories(string path);
        IEnumerable<string> EnumerateDirectories(string path, string searchPattern);

        void Delete(string path);
        void Move(string sourcePath, string destinationPath);
    }
}
