using FS24StartHub.Core.Domain;
using FS24StartHub.Core.Logging;
using FS24StartHub.Core.Settings;
using FS24StartHub.Core.Storage;

namespace FS24StartHub.Infrastructure.Settings
{
    public class FirstRunInitializer
    {
        private readonly IFileStorage _fileStorage;
        private readonly IJsonStorage _jsonStorage;
        private readonly ILogger _logger;
        private readonly string _baseFolderPath;
        private readonly string _settingsFilePath;

        public FirstRunInitializer(IFileStorage fileStorage, IJsonStorage jsonStorage, ILogger logger, string baseFolderPath)
        {
            _fileStorage = fileStorage;
            _jsonStorage = jsonStorage;
            _logger = logger;
            _baseFolderPath = baseFolderPath;
            _settingsFilePath = Path.Combine(_baseFolderPath, "fs24sh.json");
        }

        /// <summary>
        /// Performs first run initialization:
        /// - Prepares backup folder and (TODO) Backups original simulator files
        /// - Creates and saves default fs24sh.json
        /// </summary>
        public bool Initialize()
        {
            var settingsFile = Path.Combine(_baseFolderPath, "fs24sh.json");
            if (_fileStorage.FileExists(settingsFile))
            {
                _logger.Info("Initialization skipped: settings file already exists.");
                return true;
            }

            _logger.Info("First run detected. Initializing environment...");

            var detector = new SimulatorDetector(_fileStorage);
            var detection = detector.Detect();

            if (detection == null)
            {
                _logger.Error("Simulator not found. Update configuration manually in fs24sh.json and restart application.");
                return false;
            }

            BackupOriginalFiles(detection.SimPath);

            SaveDefaultSettings(detection);

            _logger.Info("First run initialization completed (modules will handle domain-specific setup).");
            return true;
        }

        /// <summary>
        /// Creates BackupOriginal folder and (later) copies original simulator files into it.
        /// Currently only prepares the folder.
        /// </summary>
        private void BackupOriginalFiles(string simPath)
        {
            var backupPath = Path.Combine(_baseFolderPath, "BackupOriginal");
            _fileStorage.CreateDirectory(backupPath);

            // TODO: define which simulator files should be backed up from simPath
            _logger.Info($"Backup folder prepared at {backupPath} (copy logic not implemented yet).");
        }

        /// <summary>
        /// Creates a default AppSettings object and saves it to fs24sh.json.
        /// Careers and Configs are empty until proper backup/initialization is implemented.
        /// </summary>
        private void SaveDefaultSettings(SimulatorDetectionResult detection)
        {
            var settings = new AppSettings
            {
                Language = "en-US",
                SimType = detection.SimType,
                SimPath = detection.SimPath,
                PackageFamilyName = detection.PackageFamilyName,
                SimExePath = detection.SimExePath,
                CurrentCareerId = "",
                CurrentConfigId = "",
                CleanupSettings = new CleanupSettings
                {
                    CareerAutosaveLimit = 5,
                    ConfigAutosaveLimit = 5
                },
                Careers = [],
                Configs = [],
                StartupItems = []
            };

            _jsonStorage.Save(_settingsFilePath, settings);
            _logger.Info($"Default settings saved to: {_settingsFilePath}");
        }
    }
}
