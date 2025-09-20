using FS24StartHub.Core.Domain;
using FS24StartHub.Core.Logging;
using FS24StartHub.Core.Storage;
using FS24StartHub.Infrastructure.Logging;
using FS24StartHub.Infrastructure.Settings;
using FS24StartHub.Infrastructure.Storage;
using FS24StartHub.IntegrationTests.Helpers;

namespace FS24StartHub.IntegrationTests.Settings
{
    [TestClass]
    public class SettingsManagerTests
    {
        private string _baseFolder = null!;
        private IFileStorage _fileStorage = null!;
        private IJsonStorage _jsonStorage = null!;
        private ILogger _logger = null!;
        public TestContext TestContext { get; set; } = null!;

        [TestInitialize]
        public void Setup()
        {
            // Create a unique folder for each test run
            var root = TestPathHelper.GetExternalTestDataRoot();
            var runFolderName = $"{DateTime.UtcNow:yyyyMMdd_HHmmssfff}_{Sanitize(TestContext.TestName!)}";
            _baseFolder = Path.Combine(root, "Integration", "Settings", runFolderName);
            Directory.CreateDirectory(_baseFolder);

            _fileStorage = new FileStorage();
            _jsonStorage = new JsonStorage(_fileStorage);

            // Use the real logger with FileLogSink and ConsoleLogSink
            ILogSink fileSink = new FileLogSink(_baseFolder);
            ILogSink consoleSink = new ConsoleLogSink();
            _logger = new Logger(new[] { fileSink, consoleSink });

            _logger.Info($"[SETUP] BaseFolder = {_baseFolder}");
        }

        [TestMethod]
        public void SaveAndLoad_ShouldPersistCareersAndConfigs()
        {
            var manager = new SettingsManager(_baseFolder, _fileStorage, _jsonStorage, _logger);

            var career = new Career
            {
                Id = "career-001",
                Slug = "first-career",
                Name = "Test Career",
                LastUsed = new DateTime(2025, 9, 17, 12, 0, 0, DateTimeKind.Utc),
                CurrentDump = "dump-123"
                // Dumps is ignored by JSON, so we don't check it here
            };

            var config = new Config
            {
                Id = "config-001",
                FolderName = "DefaultConfig",
                Description = "Test configuration",
                CreatedDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                LastUsed = new DateTime(2025, 9, 17, 12, 0, 0, DateTimeKind.Utc),
                Rating = 5
            };

            var settings = new AppSettings
            {
                Language = "en-US",
                SimPath = @"C:\MSFS",
                CurrentCareerId = career.Id,
                CurrentConfigId = config.Id,
                Careers = new List<Career> { career },
                Configs = new List<Config> { config }
            };

            manager.Save(settings);
            var loaded = manager.Load();

            Assert.AreEqual(1, loaded.Careers.Count);
            Assert.AreEqual("career-001", loaded.Careers[0].Id);
            Assert.AreEqual("first-career", loaded.Careers[0].Slug);
            Assert.AreEqual("Test Career", loaded.Careers[0].Name);
            Assert.AreEqual("dump-123", loaded.Careers[0].CurrentDump);
            Assert.AreEqual(new DateTime(2025, 9, 17, 12, 0, 0, DateTimeKind.Utc), loaded.Careers[0].LastUsed);

            Assert.AreEqual(1, loaded.Configs.Count);
            Assert.AreEqual("config-001", loaded.Configs[0].Id);
            Assert.AreEqual("DefaultConfig", loaded.Configs[0].FolderName);
            Assert.AreEqual("Test configuration", loaded.Configs[0].Description);
            Assert.AreEqual(5, loaded.Configs[0].Rating);
            Assert.AreEqual(new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), loaded.Configs[0].CreatedDate);
            Assert.AreEqual(new DateTime(2025, 9, 17, 12, 0, 0, DateTimeKind.Utc), loaded.Configs[0].LastUsed);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Load_WhenFileMissing_ShouldThrow()
        {
            var manager = new SettingsManager(_baseFolder, _fileStorage, _jsonStorage, _logger);
            manager.Load();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void Load_WhenFileCorrupted_ShouldThrow()
        {
            var manager = new SettingsManager(_baseFolder, _fileStorage, _jsonStorage, _logger);

            var settingsPath = Path.Combine(_baseFolder, "fs24sh.json");
            Directory.CreateDirectory(_baseFolder);
            File.WriteAllText(settingsPath, "{ invalid json }");

            manager.Load();
        }

        [TestMethod]
        public void Save_MultipleTimes_ShouldAlwaysLeaveValidFile()
        {
            var manager = new SettingsManager(_baseFolder, _fileStorage, _jsonStorage, _logger);
            var settings = new AppSettings
            {
                Language = "en-US",
                SimPath = @"D:\MSFS",
                CurrentCareerId = "career-xyz",
                CurrentConfigId = "config-abc"
            };

            for (int i = 0; i < 3; i++)
            {
                manager.Save(settings);
                var loaded = manager.Load();

                Assert.AreEqual("en-US", loaded.Language);
                Assert.AreEqual(@"D:\MSFS", loaded.SimPath);
                Assert.AreEqual("career-xyz", loaded.CurrentCareerId);
                Assert.AreEqual("config-abc", loaded.CurrentConfigId);
            }
        }

        [TestMethod]
        public void Logger_WritesToFileSink()
        {
            _logger.Info("[MARK] Logger_WritesToFileSink");

            var logsDir = Path.Combine(_baseFolder, "Logs");
            Assert.IsTrue(Directory.Exists(logsDir), "Logs directory should exist.");

            var todayFile = Path.Combine(logsDir, $"log-{DateTime.UtcNow:yyyyMMdd}.txt");
            Assert.IsTrue(File.Exists(todayFile), $"Expected log file: {todayFile}");

            var content = File.ReadAllText(todayFile);
            Assert.IsTrue(content.Length > 0, "Log file should not be empty.");
            StringAssert.Contains(content, "[MARK] Logger_WritesToFileSink");
        }

        private static string Sanitize(string name)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
                name = name.Replace(c, '_');
            return name;
        }
    }
}