using FS24StartHub.Infrastructure.Storage;
using FS24StartHub.IntegrationTests.Helpers;

namespace FS24StartHub.IntegrationTests.Storage
{
    [TestClass]
    public class FileStorageIntegrationTests
    {
        private string _baseFolder = null!;
        private string _filePath = null!;

        public TestContext TestContext { get; set; } = null!;

        [TestInitialize]
        public void Setup()
        {
            var externalRoot = TestPathHelper.GetExternalTestDataRoot();
            var testDataRoot = Path.Combine(externalRoot, "FileStorage");

            _baseFolder = Path.Combine(testDataRoot, TestContext.TestName!);

            if (Directory.Exists(_baseFolder))
                Directory.Delete(_baseFolder, recursive: true);

            Directory.CreateDirectory(_baseFolder);

            _filePath = Path.Combine(_baseFolder, "test.txt");
        }

        [TestMethod]
        public void WriteAllText_and_ReadAllText_roundtrip_works()
        {
            var storage = new FileStorage();
            var content = "Hello, FileStorage!";

            storage.WriteAllText(_filePath, content);
            var readBack = storage.ReadAllText(_filePath);

            Assert.AreEqual(content, readBack);
            Assert.IsTrue(storage.FileExists(_filePath), "File should exist after writing");
        }

        [TestMethod]
        public void WriteAllText_overwrites_existing_file()
        {
            var storage = new FileStorage();

            storage.WriteAllText(_filePath, "First");
            storage.WriteAllText(_filePath, "Second");
            var readBack = storage.ReadAllText(_filePath);

            Assert.AreEqual("Second", readBack);
        }

        [TestMethod]
        public void ReadAllText_on_missing_file_throws()
        {
            var storage = new FileStorage();

            Assert.IsFalse(storage.FileExists(_filePath), "File should not exist initially");
            Assert.ThrowsException<FileNotFoundException>(() => storage.ReadAllText(_filePath));
        }

        [TestMethod]
        public void WriteAllText_creates_missing_directory()
        {
            var storage = new FileStorage();
            var nestedDir = Path.Combine(_baseFolder, "nested", "deep");
            var nestedFile = Path.Combine(nestedDir, "file.txt");

            Assert.IsFalse(storage.DirectoryExists(nestedDir), "Nested directory should not exist initially");

            storage.WriteAllText(nestedFile, "data");

            Assert.IsTrue(storage.DirectoryExists(nestedDir), "WriteAllText should create missing directory");
            Assert.IsTrue(storage.FileExists(nestedFile), "File should exist after writing");
            Assert.AreEqual("data", storage.ReadAllText(nestedFile));
        }

        [TestMethod]
        public void CreateDirectory_is_idempotent_and_creates_folder()
        {
            var storage = new FileStorage();
            var dir = Path.Combine(_baseFolder, "folder");

            storage.CreateDirectory(dir);
            storage.CreateDirectory(dir); // idempotent

            Assert.IsTrue(storage.DirectoryExists(dir), "Directory should exist after CreateDirectory");
        }

        [TestMethod]
        public void EnumerateFiles_returns_expected_results()
        {
            var storage = new FileStorage();
            var f1 = Path.Combine(_baseFolder, "a.txt");
            var f2 = Path.Combine(_baseFolder, "b.log");
            var f3 = Path.Combine(_baseFolder, "c.txt");

            storage.WriteAllText(f1, "1");
            storage.WriteAllText(f2, "2");
            storage.WriteAllText(f3, "3");

            var all = storage.EnumerateFiles(_baseFolder).Select(Path.GetFileName).ToArray();
            var txtOnly = storage.EnumerateFiles(_baseFolder, "*.txt").Select(Path.GetFileName).ToArray();

            CollectionAssert.AreEquivalent(new[] { "a.txt", "b.log", "c.txt" }, all);
            CollectionAssert.AreEquivalent(new[] { "a.txt", "c.txt" }, txtOnly);
        }

        [TestMethod]
        public void Delete_removes_file_and_is_idempotent()
        {
            var storage = new FileStorage();

            storage.WriteAllText(_filePath, "x");
            Assert.IsTrue(storage.FileExists(_filePath), "File should exist before delete");

            storage.Delete(_filePath);
            Assert.IsFalse(storage.FileExists(_filePath), "File should be removed");

            storage.Delete(_filePath); // idempotent
            Assert.IsFalse(storage.FileExists(_filePath), "File should still be absent");
        }

        [TestMethod]
        public void ReadAllLines_ReturnsCorrectLines_FromRealFile()
        {
            var storage = new FileStorage();
            var path = Path.Combine(_baseFolder, "multi.txt");
            var lines = new[] { "first", "second", "third" };
            File.WriteAllLines(path, lines);

            var read = storage.ReadAllLines(path);

            CollectionAssert.AreEqual(lines, read);
        }

        [TestMethod]
        public void EnumerateDirectories_ReturnsSubfolders()
        {
            var storage = new FileStorage();
            var d1 = Path.Combine(_baseFolder, "One");
            var d2 = Path.Combine(_baseFolder, "Two");
            Directory.CreateDirectory(d1);
            Directory.CreateDirectory(d2);

            var result = storage.EnumerateDirectories(_baseFolder).Select(Path.GetFileName).ToArray();

            CollectionAssert.AreEquivalent(new[] { "One", "Two" }, result);
        }

        [TestMethod]
        public void EnumerateDirectories_WithPattern_FiltersCorrectly()
        {
            var storage = new FileStorage();
            Directory.CreateDirectory(Path.Combine(_baseFolder, "OfficialPackages"));
            Directory.CreateDirectory(Path.Combine(_baseFolder, "Community"));
            Directory.CreateDirectory(Path.Combine(_baseFolder, "Temp"));

            var result = storage.EnumerateDirectories(_baseFolder, "Official*").Select(Path.GetFileName).ToArray();

            CollectionAssert.AreEqual(new[] { "OfficialPackages" }, result);
        }

        // 🔹 Дополнения

        [TestMethod]
        public void AppendAllText_AppendsContent()
        {
            var storage = new FileStorage();
            storage.WriteAllText(_filePath, "line1\n");
            storage.AppendAllText(_filePath, "line2\n");

            var lines = storage.ReadAllLines(_filePath);
            CollectionAssert.AreEqual(new[] { "line1", "line2" }, lines);
        }

        [TestMethod]
        public void Move_MovesFileAndReplacesIfExists()
        {
            var storage = new FileStorage();
            storage.WriteAllText(_filePath, "original");

            var destDir = Path.Combine(_baseFolder, "dest");
            var destFile = Path.Combine(destDir, "file.txt");
            Directory.CreateDirectory(destDir);
            File.WriteAllText(destFile, "old");

            storage.Move(_filePath, destFile);

            Assert.IsFalse(storage.FileExists(_filePath));
            Assert.AreEqual("original", storage.ReadAllText(destFile));
        }

        [TestMethod]
        public void DirectoryExists_ReturnsTrue_WhenDirectoryCreated()
        {
            var storage = new FileStorage();
            var dir = Path.Combine(_baseFolder, "check");
            storage.CreateDirectory(dir);

            Assert.IsTrue(storage.DirectoryExists(dir));
        }

        [TestMethod]
        public void WriteAllText_DoesNotLeaveTempFile()
        {
            var storage = new FileStorage();
            storage.WriteAllText(_filePath, "data");

            Assert.IsTrue(storage.FileExists(_filePath));
            Assert.IsFalse(storage.FileExists(_filePath + ".tmp"));
        }
    }
}