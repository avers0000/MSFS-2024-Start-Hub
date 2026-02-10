using FS24StartHub.Core.Domain;
using FS24StartHub.Core.Settings;
using FS24StartHub.Core.Storage;

namespace FS24StartHub.Infrastructure.Settings
{
    public sealed class SimulatorDetector : ISimulatorDetector
    {
        private readonly IFileStorage _fileStorage;

        public SimulatorDetector(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        public SimulatorDetectionResult? Detect()
        {
            var steamResult = TryDetectSteam();
            if (steamResult != null)
                return steamResult;

            var storeResult = TryDetectStore();
            if (storeResult != null)
                return storeResult;

            return null;
        }

        private SimulatorDetectionResult? TryDetectSteam()
        {
            var cfgPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Microsoft Flight Simulator 2024",
                "UserCfg.opt");

            if (!_fileStorage.FileExists(cfgPath))
                return null;

            var installedPath = ExtractInstalledPackagesPath(cfgPath);
            if (!IsValidSimRoot(installedPath))
                return null;

            return new SimulatorDetectionResult(
                SimType: SimType.Steam,
                SimPath: installedPath!,
                PackageFamilyName: null,
                SimExePath: null // Steam uses AppId, not direct exe path
            );
        }

        private SimulatorDetectionResult? TryDetectStore()
        {
            var basePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Packages");

            if (!_fileStorage.DirectoryExists(basePath))
                return null;

            var candidateDirs = _fileStorage
                .EnumerateDirectories(basePath)
                .Where(dir =>
                    dir.Contains("Microsoft.Limitless", StringComparison.OrdinalIgnoreCase) || 
                    dir.Contains("2024", StringComparison.OrdinalIgnoreCase));

            foreach (var dir in candidateDirs)
            {
                var cfgPath = Path.Combine(dir, "LocalCache", "UserCfg.opt");
                if (!_fileStorage.FileExists(cfgPath))
                    continue;

                var installedPath = ExtractInstalledPackagesPath(cfgPath);
                if (!IsValidSimRoot(installedPath))
                    continue;

                var packageFamilyName = Path.GetFileName(dir);

                return new SimulatorDetectionResult(
                    SimType: SimType.Store,
                    SimPath: installedPath!,
                    PackageFamilyName: packageFamilyName,
                    SimExePath: null
                );
            }

            return null;
        }

        private string? ExtractInstalledPackagesPath(string cfgPath)
        {
            var lines = _fileStorage.ReadAllLines(cfgPath);
            return lines
                .FirstOrDefault(l => l.TrimStart().StartsWith("InstalledPackagesPath"))?
                .Split('"')[1];
        }

        private bool IsValidSimRoot(string? path)
        {
            if (string.IsNullOrEmpty(path) || !_fileStorage.DirectoryExists(path))
                return false;

            var hasCommunity = _fileStorage.DirectoryExists(Path.Combine(path, "Community"));
            var hasOfficial = _fileStorage.EnumerateDirectories(path, "Official*").Any();

            return hasCommunity && hasOfficial;
        }
    }
}
