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
        private readonly ILogManager _logManager;
        private readonly string _baseFolderPath;
        private readonly string _settingsFilePath;

        public FirstRunInitializer(IFileStorage fileStorage, IJsonStorage jsonStorage, ILogManager logManager, string baseFolderPath)
        {
            _fileStorage = fileStorage;
            _jsonStorage = jsonStorage;
            _logManager = logManager;
            _baseFolderPath = baseFolderPath;
            _settingsFilePath = Path.Combine(_baseFolderPath, "fs24sh.json");
        }

        public bool Initialize()
        {
            if (_fileStorage.FileExists(_settingsFilePath))
            {
                _logManager.Info("Initialization skipped: settings file already exists.", "FirstRun", "InitializationSkipped");
                return true;
            }

            _logManager.Info("First run detected. Initializing environment...", "FirstRun", "FirstRunDetected");

            var detector = new SimulatorDetector(_fileStorage);
            var detection = detector.Detect();

            if (detection == null)
            {
                _logManager.Error("Simulator not found. Update configuration manually in fs24sh.json and restart application.", "FirstRun");
                return false;
            }

            BackupOriginalFiles(detection.SimPath);
            SaveDefaultSettings(detection);

            _logManager.Info("First run initialization completed (modules will handle domain-specific setup).", "FirstRun", "InitializationComplete");
            return true;
        }

        private void BackupOriginalFiles(string simPath)
        {
            var backupPath = Path.Combine(_baseFolderPath, "BackupOriginal");
            _fileStorage.CreateDirectory(backupPath);

            _logManager.Info($"Backup folder prepared at {backupPath} (copy logic not implemented yet).", "FirstRun", "BackupPrepared");
        }

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

            _logManager.Info($"Default settings saved to: {_settingsFilePath}", "FirstRun", "DefaultSettingsSaved");
        }
    }
}