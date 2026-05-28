using FS24StartHub.Core.Storage;

namespace FS24StartHub.Infrastructure.Storage
{
    public class FileStorage: IFileStorage
    {
        public bool FileExists(string path) => File.Exists(path);
        public bool DirectoryExists(string path) => Directory.Exists(path);

        public string ReadAllText(string path) => File.ReadAllText(path);

        public string[] ReadAllLines(string path) => File.ReadAllLines(path);

        public void WriteAllText(string path, string contents)
        {
            var dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var tempPath = path + ".tmp";
            File.WriteAllText(tempPath, contents);

            if (File.Exists(path))
            {
                // Atomic replacement without backup
                File.Replace(tempPath, path, null, ignoreMetadataErrors: true);
            }
            else
            {
                File.Move(tempPath, path);
            }
        }

        public void AppendAllText(string path, string contents)
        {
            var dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            File.AppendAllText(path, contents);
        }

        public void CreateDirectory(string path) => Directory.CreateDirectory(path);

        public IEnumerable<string> EnumerateFiles(string path, string searchPattern = "*.*")
            => Directory.EnumerateFiles(path, searchPattern, SearchOption.TopDirectoryOnly);

        public IEnumerable<string> EnumerateDirectories(string path) => Directory.GetDirectories(path);

        public IEnumerable<string> EnumerateDirectories(string path, string searchPattern) => Directory.GetDirectories(path, searchPattern);

        public void Delete(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }

        public void Move(string sourcePath, string destinationPath)
        {
            var destDir = Path.GetDirectoryName(destinationPath);
            if (!string.IsNullOrEmpty(destDir) && !Directory.Exists(destDir))
                Directory.CreateDirectory(destDir);

            if (File.Exists(destinationPath))
                File.Delete(destinationPath);

            File.Move(sourcePath, destinationPath);
        }

        public void DeleteDirectory(string path, bool recursive = true)
        {
            if (Directory.Exists(path))
                Directory.Delete(path, recursive);
        }

        public void CopyFile(string sourcePath, string destPath, bool overwrite = false)
        {
            var destDir = Path.GetDirectoryName(destPath);
            if (!string.IsNullOrEmpty(destDir) && !Directory.Exists(destDir))
                Directory.CreateDirectory(destDir);

            File.Copy(sourcePath, destPath, overwrite);
        }

        public async Task<string> ComputeFileHashAsync(string path)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            await using var stream = File.OpenRead(path);
            var hash = await sha.ComputeHashAsync(stream);
            return Convert.ToHexString(hash);
        }
    }
}
