using FS24StartHub.Core.Domain;
using FS24StartHub.Core.Storage;
using FS24StartHub.Core.Logging;
using FS24StartHub.Infrastructure.Settings;
using Moq;

namespace FS24StartHub.Tests.Settings
{
    [TestClass]
    public class SettingsManagerTests
    {
        private Mock<IFileStorage> _fileStorage = null!;
        private Mock<IJsonStorage> _jsonStorage = null!;
        private Mock<ILogManager> _logManager = null!;
        private SettingsManager _settingsManager = null!;

        [TestInitialize]
        public void Setup()
        {
            _fileStorage = new Mock<IFileStorage>();
            _jsonStorage = new Mock<IJsonStorage>();
            _logManager = new Mock<ILogManager>();
            _settingsManager = new SettingsManager(
                @"C:\TestBase", _fileStorage.Object, _jsonStorage.Object, _logManager.Object);
        }

        [TestMethod]
        public void ValidateSimConfiguration_ReturnsTrue_WhenStoreIsValid()
        {
            _fileStorage.Setup(fs => fs.DirectoryExists(@"C:\SimPath")).Returns(true);

            var settings = new AppSettings
            {
                SimType = SimType.Store,
                SimPath = @"C:\SimPath",
                PackageFamilyName = "Microsoft.Limitless_8wekyb3d8bbwe"
            };

            Assert.IsTrue(_settingsManager.ValidateSimConfiguration(settings));
        }

        [TestMethod]
        public void ValidateSimConfiguration_ReturnsTrue_WhenSteamIsValid()
        {
            _fileStorage.Setup(fs => fs.DirectoryExists(@"C:\SimPath")).Returns(true);

            var settings = new AppSettings
            {
                SimType = SimType.Steam,
                SimPath = @"C:\SimPath"
            };

            Assert.IsTrue(_settingsManager.ValidateSimConfiguration(settings));
        }

        [TestMethod]
        public void ValidateSimConfiguration_ReturnsTrue_WhenCustomIsValid()
        {
            _fileStorage.Setup(fs => fs.DirectoryExists(@"C:\SimPath")).Returns(true);
            _fileStorage.Setup(fs => fs.FileExists(@"C:\Sim\FlightSimulator2024.exe")).Returns(true);

            var settings = new AppSettings
            {
                SimType = SimType.Custom,
                SimPath = @"C:\SimPath",
                SimExePath = @"C:\Sim\FlightSimulator2024.exe"
            };

            Assert.IsTrue(_settingsManager.ValidateSimConfiguration(settings));
        }

        [TestMethod]
        public void ValidateSimConfiguration_ReturnsFalse_WhenSimPathIsEmpty()
        {
            var settings = new AppSettings
            {
                SimType = SimType.Steam,
                SimPath = string.Empty
            };

            Assert.IsFalse(_settingsManager.ValidateSimConfiguration(settings));
        }

        [TestMethod]
        public void ValidateSimConfiguration_ReturnsFalse_WhenSimPathDoesNotExist()
        {
            _fileStorage.Setup(fs => fs.DirectoryExists(It.IsAny<string>())).Returns(false);

            var settings = new AppSettings
            {
                SimType = SimType.Steam,
                SimPath = @"C:\NonExistent"
            };

            Assert.IsFalse(_settingsManager.ValidateSimConfiguration(settings));
        }

        [TestMethod]
        public void ValidateSimConfiguration_ReturnsFalse_WhenSimTypeIsNull()
        {
            _fileStorage.Setup(fs => fs.DirectoryExists(@"C:\SimPath")).Returns(true);

            var settings = new AppSettings
            {
                SimType = null,
                SimPath = @"C:\SimPath"
            };

            Assert.IsFalse(_settingsManager.ValidateSimConfiguration(settings));
        }

        [TestMethod]
        public void ValidateSimConfiguration_ReturnsFalse_WhenStoreHasNoPackageFamilyName()
        {
            _fileStorage.Setup(fs => fs.DirectoryExists(@"C:\SimPath")).Returns(true);

            var settings = new AppSettings
            {
                SimType = SimType.Store,
                SimPath = @"C:\SimPath",
                PackageFamilyName = null
            };

            Assert.IsFalse(_settingsManager.ValidateSimConfiguration(settings));
        }

        [TestMethod]
        public void ValidateSimConfiguration_ReturnsFalse_WhenCustomHasNoExePath()
        {
            _fileStorage.Setup(fs => fs.DirectoryExists(@"C:\SimPath")).Returns(true);

            var settings = new AppSettings
            {
                SimType = SimType.Custom,
                SimPath = @"C:\SimPath",
                SimExePath = null
            };

            Assert.IsFalse(_settingsManager.ValidateSimConfiguration(settings));
        }
    }
}