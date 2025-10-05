using FS24StartHub.Core.Apps;
using FS24StartHub.Core.Domain;
using FS24StartHub.Core.Logging;
using FS24StartHub.Core.Settings;
using FS24StartHub.Infrastructure.Apps;
using FS24StartHub.Infrastructure.Logging;
using FS24StartHub.Infrastructure.Settings;
using FS24StartHub.Infrastructure.Storage;
using FS24StartHub.IntegrationTests.Helpers;

namespace FS24StartHub.IntegrationTests.Apps
{
    [TestClass]
    public class AppsManagerIntegrationTests
    {
        public TestContext TestContext { get; set; } = null!;

        private string _baseFolder = null!;
        private ISettingsManager _settingsManager = null!;
        private ILogManager _logManager = null!;
        private AppsManager _appsManager = null!;

        [TestInitialize]
        public void Setup()
        {
            var root = TestPathHelper.GetExternalTestDataRoot();
            var runFolderName = $"{DateTime.UtcNow:yyyyMMdd_HHmmssfff}_{this.TestContext.TestName}";
            _baseFolder = Path.Combine(root, "Integration", "AppsManager", runFolderName);

            Directory.CreateDirectory(_baseFolder);
            var fileStorage = new FileStorage();
            var jsonStorage = new JsonStorage(fileStorage);
            _logManager = new LogManager(new[] { new JsonFileLogSink(fileStorage, _baseFolder) });

            _settingsManager = new SettingsManager(_baseFolder, fileStorage, jsonStorage, _logManager);
            _settingsManager.Update(new AppSettings { StartupItems = new List<StartupItem>() });

            _appsManager = new AppsManager(_settingsManager, _logManager);
        }

        [TestMethod]
        public void SaveChanges_ShouldPersistStartupItemsToFs24shJson()
        {
            // Arrange
            var newItem = new StartupItem
            {
                Path = "C:\\example.exe",
                RunOption = RunOption.BeforeSimStarts,
                Enabled = true
            };
            _appsManager.AddStartupItem(newItem);

            // Act
            _appsManager.SaveChanges();

            // Assert
            var settings = _settingsManager.Load();
            Assert.AreEqual(1, settings.StartupItems.Count);
            Assert.AreEqual("C:\\example.exe", settings.StartupItems[0].Path);
            Assert.IsTrue(settings.StartupItems[0].Enabled);
        }

        [TestMethod]
        public void AddStartupItem_ShouldLogAction()
        {
            // Arrange
            var newItem = new StartupItem
            {
                Path = "C:\\example.exe",
                RunOption = RunOption.BeforeSimStarts,
                Enabled = true
            };

            // Act
            _appsManager.AddStartupItem(newItem);

            // Assert
            var logPath = Path.Combine(_baseFolder, "Logs", $"log_{DateTime.UtcNow:yyyy-MM-dd}.jsonl");

            // Проверяем, существует ли лог-файл
            Assert.IsTrue(File.Exists(logPath), $"Log file not found: {logPath}");

            // Читаем строки из лог-файла
            var logLines = File.ReadAllLines(logPath);

            // Настраиваем параметры десериализации
            var options = new System.Text.Json.JsonSerializerOptions
            {
                Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
            };

            // Десериализуем строки в объекты LogEvent
            var logEvents = logLines
                .Select(line => System.Text.Json.JsonSerializer.Deserialize<LogEvent>(line, options))
                .ToList();

            // Проверяем, что лог содержит событие "ItemAdded" с ожидаемым сообщением
            var itemAddedLog = logEvents.FirstOrDefault(log =>
                log != null &&
                log.EventType == "ItemAdded" &&
                log.Message == $"Startup item added: {newItem.Path}");

            Assert.IsNotNull(itemAddedLog, $"Log does not contain the expected 'ItemAdded' event for path: {newItem.Path}");
        }
    }
}