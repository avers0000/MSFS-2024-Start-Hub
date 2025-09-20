using FS24StartHub.Core.Domain;
using FS24StartHub.Core.Logging;
using FS24StartHub.Infrastructure.Logging;
using FS24StartHub.Infrastructure.Settings;
using FS24StartHub.Infrastructure.Storage;
using FS24StartHub.IntegrationTests.Helpers;

namespace FS24StartHub.IntegrationTests.Initialization
{
    [TestClass]
    public class FirstRunInitializerFullIntegrationTests
    {
        private string _baseFolder = null!;
        private string _settingsPath = null!;
        private string _logsFolder = null!;

        public TestContext TestContext { get; set; } = null!;

        [TestInitialize]
        public void Setup()
        {
            var externalRoot = TestPathHelper.GetExternalTestDataRoot();
            _baseFolder = Path.Combine(externalRoot, "FirstRunInitializerFull", TestContext.TestName!);

            if (Directory.Exists(_baseFolder))
                Directory.Delete(_baseFolder, recursive: true);

            Directory.CreateDirectory(_baseFolder);

            _settingsPath = Path.Combine(_baseFolder, "fs24sh.json");
            _logsFolder = Path.Combine(_baseFolder, "Logs");
        }

        [TestMethod]
        public void Initialize_CreatesBackup_SettingsFile_AndLogs()
        {
            // Arrange
            var fileStorage = new FileStorage();
            var jsonStorage = new JsonStorage(fileStorage);
            var logger = new Logger(new ILogSink[]
            {
                new FileLogSink(_baseFolder) // real file sink
            });

            var init = new FirstRunInitializer(fileStorage, jsonStorage, logger, _baseFolder);

            // Act
            init.Initialize();

            // Assert: backup folder and settings file exist
            Assert.IsTrue(Directory.Exists(Path.Combine(_baseFolder, "BackupOriginal")),
                "BackupOriginal folder should be created");
            Assert.IsTrue(File.Exists(_settingsPath),
                "Settings file should be created");

            // Assert: settings file contains valid defaults
            var success = jsonStorage.TryLoad<AppSettings>(_settingsPath, out var settings);
            Assert.IsTrue(success, "Settings file should be loadable");
            Assert.IsNotNull(settings, "Settings should not be null");
            Assert.AreEqual(0, settings!.Careers.Count);
            Assert.AreEqual(0, settings.Configs.Count);
            Assert.AreEqual(0, settings.StartupItems.Count);
            Assert.AreEqual(5, settings.CleanupSettings.CareerAutosaveLimit);
            Assert.AreEqual(5, settings.CleanupSettings.ConfigAutosaveLimit);

            // Assert: log file contains expected entries
            var logFile = Directory.GetFiles(_logsFolder, "log-*.txt").Single();
            var logContent = File.ReadAllText(logFile);
            StringAssert.Contains(logContent, "First run detected");
            StringAssert.Contains(logContent, "completed");
        }

        [TestMethod]
        public void Initialize_ShouldNotOverwriteExistingSettingsFile()
        {
            // Arrange
            var fileStorage = new FileStorage();
            var jsonStorage = new JsonStorage(fileStorage);
            var logger = new Logger(new ILogSink[]
            {
                new FileLogSink(_baseFolder)
            });

            var init = new FirstRunInitializer(fileStorage, jsonStorage, logger, _baseFolder);

            // First run
            init.Initialize();
            var originalContent = File.ReadAllText(_settingsPath);

            // Act: call Initialize() again
            init.Initialize();
            var secondContent = File.ReadAllText(_settingsPath);

            // Assert: settings file content remains unchanged
            Assert.AreEqual(originalContent, secondContent,
                "Settings file should not be overwritten on second initialization");

            // Assert: log file should contain both messages
            var logFile = Directory.GetFiles(_logsFolder, "log-*.txt").Single();
            var logContent = File.ReadAllText(logFile);

            // "First run detected" should appear once
            var firstRunOccurrences = logContent.Split("First run detected").Length - 1;
            Assert.AreEqual(1, firstRunOccurrences, "First run message should appear only once");

            // "Initialization skipped" should appear once
            StringAssert.Contains(logContent, "Initialization skipped: settings file already exists.");
        }
    }
}