using FS24StartHub.Core.Domain;
using FS24StartHub.Core.Logging;
using FS24StartHub.Core.Settings;
using FS24StartHub.Core.Storage;

namespace FS24StartHub.Infrastructure.Settings
{
    /// <summary>
    /// Manages application settings stored in fs24sh.json.
    /// Provides explicit error signaling on load/save operations.
    /// </summary>
    public class SettingsManager : ISettingsManager
    {
        private readonly string _settingsPath;
        private readonly IFileStorage _fileStorage;
        private readonly IJsonStorage _jsonStorage;
        private readonly ILogger _logger;

        private AppSettings _currentSettings;

        public SettingsManager(string baseFolderPath, IFileStorage fileStorage, IJsonStorage jsonStorage, ILogger logger)
        {
            _settingsPath = Path.Combine(baseFolderPath, "fs24sh.json");
            _fileStorage = fileStorage;
            _jsonStorage = jsonStorage;
            _logger = logger;
        }

        /// <summary>
        /// Gets the currently loaded settings.
        /// </summary>
        public AppSettings CurrentSettings => _currentSettings;

        /// <summary>
        /// Loads settings from fs24sh.json.
        /// Throws if the file is missing or invalid.
        /// </summary>
        public AppSettings Load()
        {
            if (!_fileStorage.FileExists(_settingsPath))
            {
                _logger.Error($"Settings file not found: {_settingsPath}");
                throw new FileNotFoundException("Settings file not found", _settingsPath);
            }

            if (!_jsonStorage.TryLoad<AppSettings>(_settingsPath, out var settings) || settings == null)
            {
                _logger.Error($"Failed to load settings from {_settingsPath}");
                throw new InvalidDataException("Settings file is invalid or corrupted");
            }

            _logger.Info("Settings loaded successfully.");
            _currentSettings = settings;
            return settings;
        }

        /// <summary>
        /// Replaces current settings with the provided instance and persists them.
        /// </summary>
        public void Update(AppSettings updatedSettings)
        {
            if (updatedSettings == null)
                throw new ArgumentNullException(nameof(updatedSettings));

            _currentSettings = updatedSettings;
            Save();
        }

        public bool ValidateSimConfiguration(AppSettings settings)
        {
            if (string.IsNullOrWhiteSpace(settings.SimPath) || !_fileStorage.DirectoryExists(settings.SimPath))
            {
                _logger.Error($"Invalid SimPath: {settings.SimPath}");
                return false;
            }

            if (settings.SimType == null)
            {
                _logger.Error("SimType is not set.");
                return false;
            }

            if (settings.SimType == SimType.Store && string.IsNullOrWhiteSpace(settings.PackageFamilyName))
            {
                _logger.Error("PackageFamilyName is required for Store version.");
                return false;
            }

            if (settings.SimType == SimType.Custom && (string.IsNullOrWhiteSpace(settings.SimExePath) || !_fileStorage.FileExists(settings.SimExePath)))
            {
                _logger.Error($"SimExePath is invalid or missing: {settings.SimExePath}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Saves settings to fs24sh.json.
        /// Uses atomic write provided by FileStorage.
        /// </summary>
        private void Save()
        {
            try
            {
                _jsonStorage.Save(_settingsPath, _currentSettings);
                _logger.Info("Settings saved successfully.");
            }
            catch (Exception ex)
            {
                _logger.Error($"Failed to save settings: {ex.Message}");
                throw;
            }
        }

    }
}