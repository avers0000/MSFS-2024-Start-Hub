using FS24StartHub.Core.Domain;
using FS24StartHub.Core.Storage;
using FS24StartHub.Infrastructure.Settings;
using Moq;

namespace FS24StartHub.Tests.Settings
{
    [TestClass]
    public class SimulatorDetectorTests
    {
        private Mock<IFileStorage> _fileStorage = null!;
        private SimulatorDetector _detector = null!;

        [TestInitialize]
        public void Setup()
        {
            _fileStorage = new Mock<IFileStorage>();
            _detector = new SimulatorDetector(_fileStorage.Object);
        }

        [TestMethod]
        public void Detect_ReturnsSteam_WhenSteamConfigIsValid()
        {
            var cfgPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Microsoft Flight Simulator 2024",
                "UserCfg.opt");

            _fileStorage.Setup(fs => fs.FileExists(cfgPath)).Returns(true);
            _fileStorage.Setup(fs => fs.ReadAllLines(cfgPath)).Returns(new[]
            {
                @"InstalledPackagesPath ""C:\SimRoot"""
            });

            _fileStorage.Setup(fs => fs.DirectoryExists("C:\\SimRoot")).Returns(true);
            _fileStorage.Setup(fs => fs.DirectoryExists(@"C:\SimRoot\Community")).Returns(true);
            _fileStorage.Setup(fs => fs.EnumerateDirectories("C:\\SimRoot", "Official*"))
                        .Returns(new[] { @"C:\SimRoot\OfficialContent" });

            var result = _detector.Detect();

            Assert.IsNotNull(result);
            Assert.AreEqual(SimType.Steam, result!.SimType);
            Assert.AreEqual(@"C:\SimRoot", result.SimPath);
            Assert.AreEqual(@"C:\SimRoot\FlightSimulator.exe", result.SimExePath);
            Assert.IsNull(result.PackageFamilyName);
        }

        [TestMethod]
        public void Detect_ReturnsStore_WhenStoreConfigIsValid()
        {
            var basePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Packages");

            var packageDir = Path.Combine(basePath, "SomePackage");
            var cfgPath = Path.Combine(packageDir, "LocalCache", "UserCfg.opt");

            _fileStorage.Setup(fs => fs.DirectoryExists(basePath)).Returns(true);
            _fileStorage.Setup(fs => fs.EnumerateDirectories(basePath)).Returns(new[] { packageDir });
            _fileStorage.Setup(fs => fs.FileExists(cfgPath)).Returns(true);
            _fileStorage.Setup(fs => fs.ReadAllLines(cfgPath)).Returns(new[]
            {
                @"InstalledPackagesPath ""D:\SimStore"""
            });

            _fileStorage.Setup(fs => fs.DirectoryExists("D:\\SimStore")).Returns(true);
            _fileStorage.Setup(fs => fs.DirectoryExists(@"D:\SimStore\Community")).Returns(true);
            _fileStorage.Setup(fs => fs.EnumerateDirectories("D:\\SimStore", "Official*"))
                        .Returns(new[] { @"D:\SimStore\OfficialPackages" });

            var result = _detector.Detect();

            Assert.IsNotNull(result);
            Assert.AreEqual(SimType.Store, result!.SimType);
            Assert.AreEqual(@"D:\SimStore", result.SimPath);
            Assert.IsNull(result.SimExePath);
            Assert.AreEqual("SomePackage", result.PackageFamilyName);
        }

        [TestMethod]
        public void Detect_ReturnsNull_WhenNoValidConfigFound()
        {
            _fileStorage.Setup(fs => fs.FileExists(It.IsAny<string>())).Returns(false);
            _fileStorage.Setup(fs => fs.DirectoryExists(It.IsAny<string>())).Returns(false);

            var result = _detector.Detect();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Detect_PrefersSteam_WhenBothSteamAndStoreAreValid()
        {
            var steamCfg = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Microsoft Flight Simulator 2024",
                "UserCfg.opt");

            var storeBase = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Packages");

            var storeDir = Path.Combine(storeBase, "StorePackage");
            var storeCfg = Path.Combine(storeDir, "LocalCache", "UserCfg.opt");

            // Steam setup
            _fileStorage.Setup(fs => fs.FileExists(steamCfg)).Returns(true);
            _fileStorage.Setup(fs => fs.ReadAllLines(steamCfg)).Returns(new[]
            {
                @"InstalledPackagesPath ""C:\SteamSim"""
            });
            _fileStorage.Setup(fs => fs.DirectoryExists("C:\\SteamSim")).Returns(true);
            _fileStorage.Setup(fs => fs.DirectoryExists(@"C:\SteamSim\Community")).Returns(true);
            _fileStorage.Setup(fs => fs.EnumerateDirectories("C:\\SteamSim", "Official*"))
                        .Returns(new[] { @"C:\SteamSim\OfficialContent" });

            // Store setup
            _fileStorage.Setup(fs => fs.DirectoryExists(storeBase)).Returns(true);
            _fileStorage.Setup(fs => fs.EnumerateDirectories(storeBase)).Returns(new[] { storeDir });
            _fileStorage.Setup(fs => fs.FileExists(storeCfg)).Returns(true);
            _fileStorage.Setup(fs => fs.ReadAllLines(storeCfg)).Returns(new[]
            {
                @"InstalledPackagesPath ""D:\StoreSim"""
            });
            _fileStorage.Setup(fs => fs.DirectoryExists("D:\\StoreSim")).Returns(true);
            _fileStorage.Setup(fs => fs.DirectoryExists(@"D:\StoreSim\Community")).Returns(true);
            _fileStorage.Setup(fs => fs.EnumerateDirectories("D:\\StoreSim", "Official*"))
                        .Returns(new[] { @"D:\StoreSim\OfficialPackages" });

            var result = _detector.Detect();

            Assert.IsNotNull(result);
            Assert.AreEqual(SimType.Steam, result!.SimType);
            Assert.AreEqual(@"C:\SteamSim", result.SimPath);
        }
    }
}