using FS24StartHub.Core.Storage;
using FS24StartHub.Infrastructure.Storage;

namespace FS24StartHub.Tests.Storage
{
    [TestClass]
    public class FileStorageTests
    {
        private string _tempDir = null!;
        private FileStorage _fileStorage = null!;

        [TestInitialize]
        public void Setup()
        {
            _tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_tempDir);
            _fileStorage = new FileStorage();
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (Directory.Exists(_tempDir))
                Directory.Delete(_tempDir, true);
        }

        [TestMethod]
        public void FileExists_ShouldReturnFalse_WhenFileDoesNotExist()
        {
            var path = Path.Combine(_tempDir, "missing.txt");
            Assert.IsFalse(_fileStorage.FileExists(path));
        }

        [TestMethod]
        public void FileExists_ShouldReturnTrue_WhenFileExists()
        {
            var path = Path.Combine(_tempDir, "exists.txt");
            File.WriteAllText(path, "hello");
            Assert.IsTrue(_fileStorage.FileExists(path));
        }

        [TestMethod]
        public void WriteAllText_ThenReadAllText_ShouldReturnSameContent()
        {
            var path = Path.Combine(_tempDir, "data.txt");
            var content = "test content";

            _fileStorage.WriteAllText(path, content);
            var read = _fileStorage.ReadAllText(path);

            Assert.AreEqual(content, read);
        }

        [TestMethod]
        public void CreateDirectory_ShouldCreateFolder()
        {
            var newDir = Path.Combine(_tempDir, "subfolder");
            _fileStorage.CreateDirectory(newDir);

            Assert.IsTrue(Directory.Exists(newDir));
        }

        [TestMethod]
        public void ReadAllLines_ShouldReturnLines_WhenFileExists()
        {
            var path = Path.Combine(_tempDir, "lines.txt");
            var content = "line1\nline2\nline3";
            File.WriteAllText(path, content);

            var lines = _fileStorage.ReadAllLines(path);

            CollectionAssert.AreEqual(new[] { "line1", "line2", "line3" }, lines);
        }

        [TestMethod]
        public void EnumerateDirectories_ShouldReturnSubfolders()
        {
            var d1 = Path.Combine(_tempDir, "Alpha");
            var d2 = Path.Combine(_tempDir, "Beta");
            Directory.CreateDirectory(d1);
            Directory.CreateDirectory(d2);

            var result = _fileStorage.EnumerateDirectories(_tempDir).Select(Path.GetFileName).ToArray();

            CollectionAssert.AreEquivalent(new[] { "Alpha", "Beta" }, result);
        }

        [TestMethod]
        public void EnumerateDirectories_WithPattern_ShouldFilterCorrectly()
        {
            Directory.CreateDirectory(Path.Combine(_tempDir, "OfficialContent"));
            Directory.CreateDirectory(Path.Combine(_tempDir, "Community"));
            Directory.CreateDirectory(Path.Combine(_tempDir, "Other"));

            var result = _fileStorage.EnumerateDirectories(_tempDir, "Official*").Select(Path.GetFileName).ToArray();

            CollectionAssert.AreEqual(new[] { "OfficialContent" }, result);
        }

    }
}