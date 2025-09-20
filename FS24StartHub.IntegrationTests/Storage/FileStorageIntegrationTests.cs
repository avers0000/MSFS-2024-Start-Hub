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
            // Arrange
            var storage = new FileStorage();
            var content = "Hello, FileStorage!";

            // Act
            storage.WriteAllText(_filePath, content);
            var readBack = storage.ReadAllText(_filePath);

            // Assert
            Assert.AreEqual(content, readBack);
            Assert.IsTrue(storage.FileExists(_filePath), "File should exist after writing");
        }

        [TestMethod]
        public void WriteAllText_overwrites_existing_file()
        {
            // Arrange
            var storage = new FileStorage();

            // Act
            storage.WriteAllText(_filePath, "First");
            storage.WriteAllText(_filePath, "Second");
            var readBack = storage.ReadAllText(_filePath);

            // Assert
            Assert.AreEqual("Second", readBack);
        }

        [TestMethod]
        public void ReadAllText_on_missing_file_throws()
        {
            // Arrange
            var storage = new FileStorage();

            // Act + Assert
            Assert.IsFalse(storage.FileExists(_filePath), "File should not exist initially");
            Assert.ThrowsException<FileNotFoundException>(() => storage.ReadAllText(_filePath));
        }

        [TestMethod]
        public void WriteAllText_creates_missing_directory()
        {
            // Arrange
            var storage = new FileStorage();
            var nestedDir = Path.Combine(_baseFolder, "nested", "deep");
            var nestedFile = Path.Combine(nestedDir, "file.txt");

            // Pre-assert
            Assert.IsFalse(storage.DirectoryExists(nestedDir), "Nested directory should not exist initially");

            // Act
            storage.WriteAllText(nestedFile, "data");

            // Assert
            Assert.IsTrue(storage.DirectoryExists(nestedDir), "WriteAllText should create missing directory");
            Assert.IsTrue(storage.FileExists(nestedFile), "File should exist after writing");
            Assert.AreEqual("data", storage.ReadAllText(nestedFile));
        }

        [TestMethod]
        public void CreateDirectory_is_idempotent_and_creates_folder()
        {
            // Arrange
            var storage = new FileStorage();
            var dir = Path.Combine(_baseFolder, "folder");

            // Act
            storage.CreateDirectory(dir);
            storage.CreateDirectory(dir); // idempotent

            // Assert
            Assert.IsTrue(storage.DirectoryExists(dir), "Directory should exist after CreateDirectory");
        }

        [TestMethod]
        public void EnumerateFiles_returns_expected_results()
        {
            // Arrange
            var storage = new FileStorage();
            var f1 = Path.Combine(_baseFolder, "a.txt");
            var f2 = Path.Combine(_baseFolder, "b.log");
            var f3 = Path.Combine(_baseFolder, "c.txt");

            storage.WriteAllText(f1, "1");
            storage.WriteAllText(f2, "2");
            storage.WriteAllText(f3, "3");

            // Act
            var all = storage.EnumerateFiles(_baseFolder).Select(Path.GetFileName).ToArray();
            var txtOnly = storage.EnumerateFiles(_baseFolder, "*.txt").Select(Path.GetFileName).ToArray();

            // Assert
            CollectionAssert.AreEquivalent(new[] { "a.txt", "b.log", "c.txt" }, all);
            CollectionAssert.AreEquivalent(new[] { "a.txt", "c.txt" }, txtOnly);
        }

        [TestMethod]
        public void Delete_removes_file_and_is_idempotent()
        {
            // Arrange
            var storage = new FileStorage();

            // Act
            storage.WriteAllText(_filePath, "x");
            Assert.IsTrue(storage.FileExists(_filePath), "File should exist before delete");

            storage.Delete(_filePath);
            Assert.IsFalse(storage.FileExists(_filePath), "File should be removed");

            // Idempotency: deleting again should not throw
            storage.Delete(_filePath);
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

    }
}