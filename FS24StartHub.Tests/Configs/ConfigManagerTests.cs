using FS24StartHub.Core.Configs;
using FS24StartHub.Core.Domain;
using FS24StartHub.Core.Logging;
using FS24StartHub.Core.Settings;
using FS24StartHub.Core.Storage;
using FS24StartHub.Infrastructure.Configs;
using Moq;

namespace FS24StartHub.Tests.Configs
{
    [TestClass]
    public class ConfigManagerTests
    {
        private Mock<ISettingsManager> _settingsManagerMock = null!;
        private Mock<IFileStorage> _fileStorageMock = null!;
        private Mock<ILogManager> _logManagerMock = null!;
        private AppSettings _appSettings = null!;

        [TestInitialize]
        public void Setup()
        {
            _settingsManagerMock = new Mock<ISettingsManager>();
            _fileStorageMock = new Mock<IFileStorage>();
            _logManagerMock = new Mock<ILogManager>();

            _appSettings = new AppSettings
            {
                SimPath = @"C:\Sim",
                Configs = new List<Config>(),
                CurrentConfigId = string.Empty
            };

            _settingsManagerMock.Setup(s => s.CurrentSettings).Returns(_appSettings);
            _settingsManagerMock.Setup(s => s.BaseFolderPath).Returns(@"C:\Base");

            // По умолчанию UserCfg.opt не существует — чтобы SyncCurrentConfig не срабатывал
            _fileStorageMock.Setup(f => f.FileExists(It.IsAny<string>())).Returns(false);
        }

        private ConfigManager CreateManager() =>
            new ConfigManager(_settingsManagerMock.Object, _fileStorageMock.Object, _logManagerMock.Object);

        // ---------- IsCurrentConfigUpToDate ----------

        [TestMethod]
        public void IsCurrentConfigUpToDate_ReturnsFalse_WhenCurrentConfigIdIsEmpty()
        {
            _appSettings.CurrentConfigId = string.Empty;
            var manager = CreateManager();

            Assert.IsFalse(manager.IsCurrentConfigUpToDate());
        }

        [TestMethod]
        public void IsCurrentConfigUpToDate_ReturnsFalse_WhenSnapshotFileNotFound()
        {
            _appSettings.CurrentConfigId = "cfg-001";

            var sourcePath = @"C:\Sim\..\UserCfg.opt";
            var snapshotPath = @"C:\Base\Configs\cfg-001\UserCfg.opt";

            _fileStorageMock.Setup(f => f.FileExists(It.Is<string>(p => p.Contains("Sim")))).Returns(true);
            _fileStorageMock.Setup(f => f.FileExists(It.Is<string>(p => p.Contains("cfg-001")))).Returns(false);

            var manager = CreateManager();

            Assert.IsFalse(manager.IsCurrentConfigUpToDate());
        }

        [TestMethod]
        public void IsCurrentConfigUpToDate_ReturnsTrue_WhenHashesMatch()
        {
            _appSettings.CurrentConfigId = "cfg-001";

            _fileStorageMock.Setup(f => f.FileExists(It.IsAny<string>())).Returns(true);
            _fileStorageMock.Setup(f => f.ComputeFileHash(It.IsAny<string>())).Returns("AABBCC");

            var manager = CreateManager();

            Assert.IsTrue(manager.IsCurrentConfigUpToDate());
        }

        [TestMethod]
        public void IsCurrentConfigUpToDate_ReturnsFalse_WhenHashesDiffer()
        {
            _appSettings.CurrentConfigId = "cfg-001";

            _fileStorageMock.Setup(f => f.FileExists(It.IsAny<string>())).Returns(true);
            _fileStorageMock.Setup(f => f.ComputeFileHash(It.Is<string>(p => p.Contains("Sim")))).Returns("AABBCC");
            _fileStorageMock.Setup(f => f.ComputeFileHash(It.Is<string>(p => p.Contains("cfg-001")))).Returns("DDEEFF");

            var manager = CreateManager();

            Assert.IsFalse(manager.IsCurrentConfigUpToDate());
        }

        // ---------- SaveCurrentConfig ----------

        [TestMethod]
        public void SaveCurrentConfig_ThrowsFileNotFoundException_WhenSourceNotFound()
        {
            _fileStorageMock.Setup(f => f.FileExists(It.IsAny<string>())).Returns(false);

            var manager = CreateManager();

            Assert.ThrowsException<FileNotFoundException>(() => manager.SaveCurrentConfig());
        }

        [TestMethod]
        public void SaveCurrentConfig_AddsConfigAndUpdatesCurrent_WhenSourceExists()
        {
            // Снапшот текущего конфига не существует — SyncCurrentConfig пропустит
            _fileStorageMock.Setup(f => f.FileExists(It.IsAny<string>())).Returns(false);

            var manager = CreateManager();

            // Теперь источник существует
            _fileStorageMock.Setup(f => f.FileExists(It.IsAny<string>())).Returns(true);
            manager.SaveCurrentConfig("MyProfile");

            var configs = manager.GetConfigs().ToList();
            Assert.AreEqual(1, configs.Count);
            Assert.AreEqual("MyProfile", configs[0].Name);
            Assert.IsNotNull(manager.CurrentConfigId);
        }

        [TestMethod]
        public void SaveCurrentConfig_UsesDefaultName_WhenNameIsNull()
        {
            _fileStorageMock.Setup(f => f.FileExists(It.IsAny<string>())).Returns(false);

            var manager = CreateManager();

            _fileStorageMock.Setup(f => f.FileExists(It.IsAny<string>())).Returns(true);
            manager.SaveCurrentConfig();

            var config = manager.GetConfigs().First();
            StringAssert.StartsWith(config.Name, "Config ");
        }

        // ---------- ApplySelectedConfig ----------

        [TestMethod]
        public void ApplySelectedConfig_DoesNothing_WhenSelectedConfigIdIsEmpty()
        {
            var manager = CreateManager();
            manager.SelectConfig(null);

            manager.ApplySelectedConfig(); // should not throw

            _fileStorageMock.Verify(f => f.CopyFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Never);
        }

        [TestMethod]
        public void ApplySelectedConfig_ThrowsFileNotFoundException_WhenSnapshotMissing()
        {
            _appSettings.Configs = new List<Config> { new Config { Id = "cfg-001", Name = "Test" } };
            _appSettings.CurrentConfigId = "cfg-001";

            _fileStorageMock.Setup(f => f.FileExists(It.Is<string>(p => p.Contains("cfg-001")))).Returns(false);

            var manager = CreateManager();
            manager.SelectConfig("cfg-001");

            Assert.ThrowsException<FileNotFoundException>(() => manager.ApplySelectedConfig());
        }

        [TestMethod]
        public void ApplySelectedConfig_CopiesFileAndUpdatesCurrentConfigId()
        {
            _appSettings.Configs = new List<Config> { new Config { Id = "cfg-001", Name = "Test" } };

            _fileStorageMock.Setup(f => f.FileExists(It.IsAny<string>())).Returns(true);

            var manager = CreateManager();
            manager.SelectConfig("cfg-001");
            manager.ApplySelectedConfig();

            _fileStorageMock.Verify(f => f.CopyFile(
                It.Is<string>(p => p.Contains("cfg-001")),
                It.Is<string>(p => p.Contains("Sim")),
                true), Times.Once);

            Assert.AreEqual("cfg-001", manager.CurrentConfigId);
        }

        // ---------- SyncCurrentConfig (via constructor) ----------

        [TestMethod]
        public void Constructor_CallsSaveCurrentConfig_WhenConfigIsOutOfDate()
        {
            _appSettings.CurrentConfigId = "cfg-001";

            // Хэши разные — drift
            _fileStorageMock.Setup(f => f.FileExists(It.IsAny<string>())).Returns(true);
            _fileStorageMock.Setup(f => f.ComputeFileHash(It.Is<string>(p => p.Contains("Sim")))).Returns("AABBCC");
            _fileStorageMock.Setup(f => f.ComputeFileHash(It.Is<string>(p => p.Contains("cfg-001")))).Returns("DDEEFF");

            var manager = CreateManager();

            // После конструктора должен появиться новый autosave
            var configs = manager.GetConfigs().ToList();
            Assert.IsTrue(configs.Any(c => c.Name.StartsWith("autosave-")));
        }

        [TestMethod]
        public void Constructor_DoesNotSave_WhenConfigIsUpToDate()
        {
            _appSettings.CurrentConfigId = "cfg-001";
            _appSettings.Configs = new List<Config> { new Config { Id = "cfg-001", Name = "Existing" } };

            _fileStorageMock.Setup(f => f.FileExists(It.IsAny<string>())).Returns(true);
            _fileStorageMock.Setup(f => f.ComputeFileHash(It.IsAny<string>())).Returns("AABBCC");

            var manager = CreateManager();

            _fileStorageMock.Verify(f => f.CopyFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Never);
        }
    }
}