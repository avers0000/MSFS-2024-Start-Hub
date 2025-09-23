using FS24StartHub.Core.Domain;
using FS24StartHub.Core.Logging;
using FS24StartHub.Infrastructure.Logging;
using FS24StartHub.Infrastructure.Settings;
using FS24StartHub.Infrastructure.Storage;
using FS24StartHub.IntegrationTests.Helpers;
using System.Text.Json;

namespace FS24StartHub.IntegrationTests.Settings
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
        public void Initialize_CreatesSettingsFile_AndLogs_AndBackupFolder()
        {
            var fileStorage = new FileStorage();
            var jsonStorage = new JsonStorage(fileStorage);
            var logSink = new JsonFileLogSink(fileStorage, _baseFolder);
            var logManager = new LogManager(new[] { logSink });

            var init = new FirstRunInitializer(fileStorage, jsonStorage, logManager, _baseFolder);

            init.Initialize();

            // Verify that settings file is created
            Assert.IsTrue(File.Exists(_settingsPath), "Settings file should be created");

            // Verify that settings file can be loaded and has default values
            var success = jsonStorage.TryLoad<AppSettings>(_settingsPath, out var settings);
            Assert.IsTrue(success, "Settings should be loadable");
            Assert.IsNotNull(settings);
            Assert.AreEqual(5, settings!.CleanupSettings.CareerAutosaveLimit);
            Assert.AreEqual(5, settings.CleanupSettings.ConfigAutosaveLimit);

            // Verify that backup folder is created (file inside is not expected yet)
            var backupDir = Path.Combine(_baseFolder, "BackupOriginal");
            Assert.IsTrue(Directory.Exists(backupDir), "Backup folder should be created");

            // Verify that log file contains expected messages
            var logFile = Directory.GetFiles(_logsFolder, "log_*.jsonl").Single();
            var lines = File.ReadAllLines(logFile);
            var events = lines.Select(line => JsonSerializer.Deserialize<LogEvent>(line, LogJsonDefaults.Options)).ToArray();

            Assert.IsTrue(events.Any(e => e!.Message.Contains("First run detected")), "Log should contain 'First run detected'");
            Assert.IsTrue(events.Any(e => e!.Message.Contains("completed")), "Log should contain 'completed'");
            Assert.IsTrue(events.Any(e => e!.Message.Contains("Backup folder prepared")), "Log should contain 'Backup folder prepared'");
        }

        [TestMethod]
        public void Initialize_ShouldNotOverwriteExistingSettingsFile()
        {
            var fileStorage = new FileStorage();
            var jsonStorage = new JsonStorage(fileStorage);
            var logSink = new JsonFileLogSink(fileStorage, _baseFolder);
            var logManager = new LogManager(new[] { logSink });

            var init = new FirstRunInitializer(fileStorage, jsonStorage, logManager, _baseFolder);

            // First run should create the settings file
            init.Initialize();
            var originalContent = File.ReadAllText(_settingsPath);

            // Second run should not overwrite the settings file
            init.Initialize();
            var secondContent = File.ReadAllText(_settingsPath);

            Assert.AreEqual(originalContent, secondContent, "Settings file should not be overwritten");

            // Verify that log file contains both 'First run detected' and 'Initialization skipped'
            var logFile = Directory.GetFiles(_logsFolder, "log_*.jsonl").Single();
            var lines = File.ReadAllLines(logFile);
            var events = lines.Select(line => JsonSerializer.Deserialize<LogEvent>(line, LogJsonDefaults.Options)).ToArray();

            var firstRunCount = events.Count(e => e!.Message.Contains("First run detected"));
            var skippedCount = events.Count(e => e!.Message.Contains("Initialization skipped"));

            Assert.AreEqual(1, firstRunCount, "First run message should appear once");
            Assert.AreEqual(1, skippedCount, "Initialization skipped message should appear once");
        }
    }
}