using FS24StartHub.Core.Domain;
using FS24StartHub.Infrastructure.Settings;
using FS24StartHub.Infrastructure.Storage;
using FS24StartHub.IntegrationTests.Helpers;

namespace FS24StartHub.IntegrationTests.Settings
{
    [TestClass]
    public class SimulatorDetectorIntegrationTests
    {
        private FileStorage _fileStorage = null!;
        private SimulatorDetector _detector = null!;
        private string _externalRoot = null!;

        public TestContext TestContext { get; set; } = null!;

        [TestInitialize]
        public void Setup()
        {
            _externalRoot = TestPathHelper.GetExternalTestDataRoot();
            _fileStorage = new FileStorage();
            _detector = new SimulatorDetector(_fileStorage);
        }

        [TestMethod]
        public void Detect_ShouldBehaveCorrectly_WhenSteamConfigIsPresentOrMissing()
        {
            var fileStorage = new FileStorage();
            var detector = new SimulatorDetector(fileStorage);

            var roamingPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var steamCfgPath = Path.Combine(roamingPath, "Microsoft Flight Simulator 2024", "UserCfg.opt");

            var cfgExists = fileStorage.FileExists(steamCfgPath);
            var result = detector.Detect();

            if (cfgExists)
            {
                Console.WriteLine($"Steam config found at: {steamCfgPath}");
                Assert.IsNotNull(result, "Simulator should be detected.");
                Assert.AreEqual(SimType.Steam, result!.SimType, "Expected Steam simulator.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(result.SimPath), "SimPath should be set.");
                Assert.IsNull(result.PackageFamilyName, "PackageFamilyName should be null for Steam.");
                Assert.IsNull(result.SimExePath, "SimExePath should be null for Steam.");
            }
            else
            {
                Console.WriteLine("Steam config not found — simulator should not be detected.");
                Assert.IsTrue(result == null || result.SimType != SimType.Steam,
                    "Steam simulator should not be detected when config is missing.");
            }
        }

        [TestMethod]
        public void Detect_ShouldReturnStore_WhenRealStoreConfigExists()
        {
            var packagesRoot = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Packages");

            if (!_fileStorage.DirectoryExists(packagesRoot))
            {
                Console.WriteLine("Packages folder not found — assuming simulator is not installed.");
                var result = _detector.Detect();
                Assert.IsNull(result, "Simulator should not be detected when Packages folder is missing.");
                return;
            }

            var candidates = _fileStorage.EnumerateDirectories(packagesRoot);
            foreach (var candidate in candidates)
            {
                var cfgPath = Path.Combine(candidate, "LocalCache", "UserCfg.opt");
                if (!_fileStorage.FileExists(cfgPath))
                    continue;

                var lines = _fileStorage.ReadAllLines(cfgPath);
                var installedPath = ParseInstalledPackagesPath(lines);

                if (installedPath == null || !_fileStorage.DirectoryExists(installedPath))
                    continue;

                var hasCommunity = _fileStorage.DirectoryExists(Path.Combine(installedPath, "Community"));
                var hasOfficial = _fileStorage.EnumerateDirectories(installedPath, "Official*").Any();

                if (!hasCommunity || !hasOfficial)
                    continue;

                var result = _detector.Detect();

                Assert.IsNotNull(result, "Simulator should be detected.");
                Assert.AreEqual(SimType.Store, result!.SimType);
                Assert.AreEqual(installedPath, result.SimPath);
                Assert.AreEqual(Path.GetFileName(candidate), result.PackageFamilyName);
                return;
            }

            Console.WriteLine("No valid Store config found — simulator should not be detected.");
            var fallbackResult = _detector.Detect();
            Assert.IsNull(fallbackResult, "Simulator should not be detected when no valid Store config is present.");
        }

        private static string? ParseInstalledPackagesPath(string[] lines)
        {
            foreach (var line in lines)
            {
                var trimmed = line.Trim();
                if (trimmed.StartsWith("InstalledPackagesPath"))
                {
                    var start = trimmed.IndexOf('"');
                    var end = trimmed.LastIndexOf('"');
                    if (start >= 0 && end > start)
                        return trimmed.Substring(start + 1, end - start - 1);
                }
            }
            return null;
        }
    }
}