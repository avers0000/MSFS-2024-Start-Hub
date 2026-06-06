using FS24StartHub.Core.Domain;
using FS24StartHub.Core.Logging;
using FS24StartHub.Infrastructure.Configs;
using FS24StartHub.Infrastructure.Logging;
using FS24StartHub.Infrastructure.Settings;
using FS24StartHub.Infrastructure.Storage;
using FS24StartHub.IntegrationTests.Helpers;

namespace FS24StartHub.IntegrationTests.Configs
{
    [TestClass]
    public class ConfigManagerIntegrationTests
    {
        public TestContext TestContext { get; set; } = null!;

        private string _baseFolder = null!;
        private string _simFolder = null!;
        private string _userCfgPath = null!;
        private SettingsManager _settingsManager = null!;
        private FileStorage _fileStorage = null!;
        private ILogManager _logManager = null!;

        [TestInitialize]
        public void Setup()
        {
            var root = TestPathHelper.GetExternalTestDataRoot();
            var runFolderName = $"{DateTime.UtcNow:yyyyMMdd_HHmmssfff}_{TestContext.TestName}";
            _baseFolder = Path.Combine(root, "Integration", "ConfigManager", runFolderName);

            _simFolder = Path.Combine(_baseFolder, "Sim", "Packages");
            _userCfgPath = Path.Combine(_baseFolder, "Sim", "UserCfg.opt");

            Directory.CreateDirectory(_baseFolder);
            Directory.CreateDirectory(_simFolder);

            // Создаём реальный UserCfg.opt рядом с папкой симулятора
            File.WriteAllText(_userCfgPath, "InstalledPackagesPath \"C:\\Sim\"");

            _fileStorage = new FileStorage();
            var jsonStorage = new JsonStorage(_fileStorage);
            _logManager = new LogManager(new[] { new JsonFileLogSink(_fileStorage, _baseFolder) });

            _settingsManager = new SettingsManager(_baseFolder, _fileStorage, jsonStorage, _logManager);
            _settingsManager.Update(new AppSettings
            {
                SimPath = _simFolder,
                Configs = new List<Config>(),
                CurrentConfigId = string.Empty
            });
        }

        private ConfigManager CreateManager() =>
            new ConfigManager(_settingsManager, _fileStorage, _logManager);

        [TestMethod]
        public void SaveCurrentConfig_ShouldCreateSnapshotFile_AndUpdateCurrentConfigId()
        {
            var manager = CreateManager();

            // Конструктор уже создал autosave — меняем файл чтобы следующий save был новым
            File.WriteAllText(_userCfgPath, "InstalledPackagesPath \"D:\\NewSim\"");
            manager.SaveCurrentConfig("MyProfile");

            var configs = manager.GetConfigs().ToList();
            Assert.AreEqual(2, configs.Count);

            var myProfile = configs.FirstOrDefault(c => c.Name == "MyProfile");
            Assert.IsNotNull(myProfile);

            var snapshotPath = manager.GetConfigFilePath(myProfile!.Id);
            Assert.IsTrue(File.Exists(snapshotPath));
            Assert.AreEqual(manager.CurrentConfigId, myProfile.Id);
        }

        [TestMethod]
        public void IsCurrentConfigUpToDate_ReturnsTrue_WhenFilesAreIdentical()
        {
            var manager = CreateManager();
            manager.SaveCurrentConfig("Baseline");

            Assert.IsTrue(manager.IsCurrentConfigUpToDate());
        }

        [TestMethod]
        public void IsCurrentConfigUpToDate_ReturnsFalse_WhenSourceFileChanged()
        {
            var manager = CreateManager();
            manager.SaveCurrentConfig("Baseline");

            // Симулируем изменение UserCfg.opt симулятором
            File.WriteAllText(_userCfgPath, "InstalledPackagesPath \"D:\\NewSim\"");

            Assert.IsFalse(manager.IsCurrentConfigUpToDate());
        }

        [TestMethod]
        public void ApplySelectedConfig_ShouldCopySnapshotToSimulator()
        {
            var manager = CreateManager();
            manager.SaveCurrentConfig("Baseline");
            var baselineId = manager.CurrentConfigId!;

            // Меняем UserCfg.opt
            File.WriteAllText(_userCfgPath, "InstalledPackagesPath \"D:\\NewSim\"");

            // Сохраняем новый профиль
            manager.SaveCurrentConfig("Modified");
            var modifiedId = manager.CurrentConfigId!;

            // Выбираем baseline и применяем
            manager.SelectConfig(baselineId);
            manager.ApplySelectedConfig();

            var content = File.ReadAllText(_userCfgPath);
            Assert.IsTrue(content.Contains("C:\\Sim"));
            Assert.AreEqual(baselineId, manager.CurrentConfigId);
        }

        [TestMethod]
        public void Constructor_CreatesAutosave_WhenCurrentConfigIsMissing()
        {
            // Первый запуск — нет CurrentConfigId
            var manager = CreateManager();

            var configs = manager.GetConfigs().ToList();
            Assert.AreEqual(1, configs.Count);
            StringAssert.StartsWith(configs[0].Name, "autosave-");
        }

        [TestMethod]
        public void Constructor_CreatesAutosave_WhenConfigDriftDetected()
        {
            // Сохраняем baseline
            var manager = CreateManager();
            var firstAutosaveId = manager.CurrentConfigId!;

            // Меняем UserCfg.opt — симулируем drift
            File.WriteAllText(_userCfgPath, "InstalledPackagesPath \"D:\\Changed\"");

            // Пересоздаём менеджер — должен обнаружить drift и создать новый autosave
            var reloadedSettings = new SettingsManager(
                _baseFolder, _fileStorage, new JsonStorage(_fileStorage), _logManager);
            reloadedSettings.Load();

            var manager2 = new ConfigManager(reloadedSettings, _fileStorage, _logManager);

            var configs = manager2.GetConfigs().ToList();
            Assert.IsTrue(configs.Count >= 2);
            Assert.AreNotEqual(firstAutosaveId, manager2.CurrentConfigId);
        }

        [TestMethod]
        public void DeleteConfig_ShouldRemoveSnapshotFolder()
        {
            var manager = CreateManager();
            manager.SaveCurrentConfig("ToDelete");
            var id = manager.CurrentConfigId!;

            // Сохраняем второй профиль чтобы первый не был current
            File.WriteAllText(_userCfgPath, "InstalledPackagesPath \"D:\\Other\"");
            manager.SaveCurrentConfig("Current");

            var folderPath = Path.Combine(_baseFolder, "Configs", id);
            Assert.IsTrue(Directory.Exists(folderPath));

            manager.DeleteConfig(id);

            Assert.IsFalse(Directory.Exists(folderPath));
        }
    }
}