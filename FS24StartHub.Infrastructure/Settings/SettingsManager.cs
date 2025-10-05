using FS24StartHub.Core.Domain;
using FS24StartHub.Core.Logging;
using FS24StartHub.Core.Settings;
using FS24StartHub.Core.Storage;

namespace FS24StartHub.Infrastructure.Settings
{
    public class SettingsManager : ISettingsManager
    {
        private readonly string _settingsPath;
        private readonly IFileStorage _fileStorage;
        private readonly IJsonStorage _jsonStorage;
        private readonly ILogManager _logManager;

        private AppSettings? _currentSettings;

        public event Action? SettingsChanged; // Triggered when settings are updated
        public event Action? SettingsReloaded; // Triggered when settings are reloaded from file

        public SettingsManager(string baseFolderPath, IFileStorage fileStorage, IJsonStorage jsonStorage, ILogManager logManager)
        {
            _settingsPath = Path.Combine(baseFolderPath, "fs24sh.json");
            _fileStorage = fileStorage;
            _jsonStorage = jsonStorage;
            _logManager = logManager;
        }

        // Return a copy of the current settings to prevent external modifications
        public AppSettings? CurrentSettings => _currentSettings?.Clone();

        public AppSettings Load()
        {
            if (!_fileStorage.FileExists(_settingsPath))
            {
                _logManager.Error($"Settings file not found: {_settingsPath}", "SettingsManager");
                throw new FileNotFoundException("Settings file not found", _settingsPath);
            }

            if (!_jsonStorage.TryLoad<AppSettings>(_settingsPath, out var settings) || settings == null)
            {
                _logManager.Error($"Failed to load settings from {_settingsPath}", "SettingsManager");
                throw new InvalidDataException("Settings file is invalid or corrupted");
            }

            _logManager.Info("Settings loaded successfully.", "SettingsManager", "SettingsLoaded");
            _currentSettings = settings;

            // Trigger the SettingsReloaded event
            SettingsReloaded?.Invoke();

            return settings;
        }

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
                _logManager.Error($"Invalid SimPath: {settings.SimPath}", "SettingsManager");
                return false;
            }

            if (settings.SimType == null)
            {
                _logManager.Error("SimType is not set.", "SettingsManager");
                return false;
            }

            if (settings.SimType == SimType.Store && string.IsNullOrWhiteSpace(settings.PackageFamilyName))
            {
                _logManager.Error("PackageFamilyName is required for Store version.", "SettingsManager");
                return false;
            }

            if (settings.SimType == SimType.Custom &&
                (string.IsNullOrWhiteSpace(settings.SimExePath) || !_fileStorage.FileExists(settings.SimExePath)))
            {
                _logManager.Error($"SimExePath is invalid or missing: {settings.SimExePath}", "SettingsManager");
                return false;
            }

            return true;
        }

        public void UpdateStartupItems(IEnumerable<StartupItem> items)
        {
            _currentSettings!.StartupItems = [..items];
            Save();
        }

        private void Save()
        {
            try
            {
                _jsonStorage.Save(_settingsPath, _currentSettings);
                _logManager.Info("Settings saved successfully.", "SettingsManager", "SettingsSaved");

                // Trigger the SettingsChanged event
                SettingsChanged?.Invoke();
            }
            catch (Exception ex)
            {
                _logManager.Error($"Failed to save settings: {ex.Message}", "SettingsManager", ex);
                throw;
            }
        }
    }
}