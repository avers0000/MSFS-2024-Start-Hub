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

        [TestMethod]
        public void WriteAllText_ShouldReplaceExistingFile()
        {
            var path = Path.Combine(_tempDir, "replace.txt");
            File.WriteAllText(path, "old");

            _fileStorage.WriteAllText(path, "new");
            var read = File.ReadAllText(path);

            Assert.AreEqual("new", read);
            Assert.IsFalse(File.Exists(path + ".tmp"), "Temp file should not remain");
        }

        [TestMethod]
        public void AppendAllText_ShouldAppendContent()
        {
            var path = Path.Combine(_tempDir, "append.txt");
            _fileStorage.WriteAllText(path, "line1\n");
            _fileStorage.AppendAllText(path, "line2\n");

            var lines = File.ReadAllLines(path);
            CollectionAssert.AreEqual(new[] { "line1", "line2" }, lines);
        }

        [TestMethod]
        public void DirectoryExists_ShouldReturnTrue_WhenDirectoryExists()
        {
            var dir = Path.Combine(_tempDir, "subdir");
            Directory.CreateDirectory(dir);

            Assert.IsTrue(_fileStorage.DirectoryExists(dir));
        }

        [TestMethod]
        public void EnumerateFiles_ShouldReturnFiles()
        {
            var f1 = Path.Combine(_tempDir, "a.txt");
            var f2 = Path.Combine(_tempDir, "b.log");
            File.WriteAllText(f1, "x");
            File.WriteAllText(f2, "y");

            var all = _fileStorage.EnumerateFiles(_tempDir).Select(Path.GetFileName).ToArray();
            CollectionAssert.AreEquivalent(new[] { "a.txt", "b.log" }, all);

            var txtOnly = _fileStorage.EnumerateFiles(_tempDir, "*.txt").Select(Path.GetFileName).ToArray();
            CollectionAssert.AreEqual(new[] { "a.txt" }, txtOnly);
        }

        [TestMethod]
        public void Delete_ShouldRemoveFile()
        {
            var path = Path.Combine(_tempDir, "delete.txt");
            File.WriteAllText(path, "to delete");

            _fileStorage.Delete(path);

            Assert.IsFalse(File.Exists(path));
        }

        [TestMethod]
        public void Move_ShouldMoveFileAndReplaceIfExists()
        {
            var src = Path.Combine(_tempDir, "src.txt");
            var destDir = Path.Combine(_tempDir, "dest");
            var dest = Path.Combine(destDir, "dest.txt");

            File.WriteAllText(src, "original");
            Directory.CreateDirectory(destDir);
            File.WriteAllText(dest, "old");

            _fileStorage.Move(src, dest);

            Assert.IsFalse(File.Exists(src));
            Assert.AreEqual("original", File.ReadAllText(dest));
        }
    }
}