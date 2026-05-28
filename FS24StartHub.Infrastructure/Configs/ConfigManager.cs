using FS24StartHub.Core.Configs;
using FS24StartHub.Core.Domain;
using FS24StartHub.Core.Launcher.Tasks;
using FS24StartHub.Core.Logging;
using FS24StartHub.Core.Settings;
using FS24StartHub.Core.Storage;

namespace FS24StartHub.Infrastructure.Configs
{
    public class ConfigManager : IConfigManager
    {
        private readonly ISettingsManager _settingsManager;
        private readonly IFileStorage _fileStorage;
        private readonly ILogManager _logManager;
        private List<Config> _configs = [];
        private string? _currentConfigId;

        public bool IsDirty { get; private set; }
        public string? CurrentConfigId => _currentConfigId;

        public event Action? DataChanged;

        public ConfigManager(ISettingsManager settingsManager, IFileStorage fileStorage, ILogManager logManager)
        {
            _settingsManager = settingsManager;
            _fileStorage = fileStorage;
            _logManager = logManager;

            _settingsManager.SettingsReloaded += LoadData;

            LoadData();

            try
            {
                if (!IsCurrentConfigUpToDate())
                    SaveCurrentConfig($"autosave-{DateTime.Now:yyyy-MM-dd HH:mm}");
            }
            catch (IOException ex)
            {
                _logManager.Error($"Failed to sync current config: {ex.Message}", "ConfigManager");
            }

            _logManager.Info("ConfigManager initialized.", "ConfigManager");
            IsDirty = false;
        }

        public IEnumerable<Config> GetConfigs()
        {
            foreach (var cfg in _configs)
                cfg.IsCurrent = cfg.Id == _currentConfigId;
            return _configs.ToList();
        }

        public bool HasChanges() => IsDirty;

        public void UpdateChanges()
        {
            if (!IsDirty) return;
            _settingsManager.UpdateConfigs(_configs);
            _settingsManager.UpdateCurrentConfigId(_currentConfigId);
            IsDirty = false;
        }

        public void SaveChanges()
        {
            if (!IsDirty) return;
            _settingsManager.SaveConfigs(_configs);
            _settingsManager.SaveCurrentConfigId(_currentConfigId);
            IsDirty = false;
        }

        public ILaunchTask GetSaveTask()
        {
            return new ApplyConfigTask(this, _logManager);
        }

        private void LoadData()
        {
            _configs = [.. _settingsManager.CurrentSettings?.Configs ?? []];
            _currentConfigId = _settingsManager.CurrentSettings?.CurrentConfigId;
            IsDirty = false;
        }

        public string? SelectedConfigId { get; private set; }

        public void SelectConfig(string? id)
        {
            SelectedConfigId = id;
        }

        public void UpdateConfig(Config config)
        {
            var existing = _configs.FirstOrDefault(c => c.Id == config.Id);
            if (existing is null)
            {
                _logManager.Error($"Config '{config.Id}' not found.", "ConfigManager");
                throw new InvalidOperationException($"Config with ID '{config.Id}' not found.");
            }

            existing.Name = config.Name;
            existing.Description = config.Description;

            IsDirty = true;
            SaveChanges();
            DataChanged?.Invoke();
        }

        public string GetConfigFilePath(string configId)
        {
            return Path.Combine(
                _settingsManager.BaseFolderPath,
                "Configs",
                configId,
                "UserCfg.opt");
        }

        public void DeleteConfig(string configId)
        {
            if (configId == _currentConfigId)
            {
                _logManager.Error($"Cannot delete current config '{configId}'.", "ConfigManager");
                throw new InvalidOperationException("Cannot delete the currently applied config.");
            }

            var existing = _configs.FirstOrDefault(c => c.Id == configId);
            if (existing is null)
            {
                _logManager.Error($"Config '{configId}' not found.", "ConfigManager");
                throw new InvalidOperationException($"Config with ID '{configId}' not found.");
            }

            _configs.Remove(existing);

            var configFolder = Path.Combine(
                _settingsManager.BaseFolderPath,
                "Configs",
                configId);

            _fileStorage.DeleteDirectory(configFolder);
            _logManager.Info($"Config folder deleted: {configFolder}", "ConfigManager");

            if (SelectedConfigId == configId)
                SelectedConfigId = null;

            IsDirty = true;
            SaveChanges();
            DataChanged?.Invoke();
        }

        public bool IsCurrentConfigUpToDate()
        {
            if (string.IsNullOrEmpty(_currentConfigId))
                return false;

            var sourcePath = GetSourceUserCfgPath();
            if (!_fileStorage.FileExists(sourcePath))
                throw new FileNotFoundException("Simulator UserCfg.opt not found.", sourcePath);

            var snapshotPath = GetConfigFilePath(_currentConfigId);
            if (!_fileStorage.FileExists(snapshotPath))
                return false;

            var sourceHash = _fileStorage.ComputeFileHash(sourcePath);
            var snapshotHash = _fileStorage.ComputeFileHash(snapshotPath);

            return sourceHash == snapshotHash;
        }

        public void SaveCurrentConfig(string? name = null)
        {
            var sourcePath = GetSourceUserCfgPath();
            if (!_fileStorage.FileExists(sourcePath))
                throw new FileNotFoundException("Simulator UserCfg.opt not found.", sourcePath);

            var id = Guid.NewGuid().ToString();
            var configName = string.IsNullOrWhiteSpace(name)
                ? $"Config {DateTime.Now:yyyy-MM-dd HH:mm}"
                : name;

            var config = new Config
            {
                Id = id,
                Name = configName,
                CreatedDate = DateTime.Now,
                LastUsed = DateTime.Now
            };

            var destPath = GetConfigFilePath(id);
            _fileStorage.CopyFile(sourcePath, destPath);
            _logManager.Info($"Config snapshot saved: {destPath}", "ConfigManager");

            _configs.Add(config);
            _currentConfigId = id;

            IsDirty = true;
            SaveChanges();
            DataChanged?.Invoke();
        }

        private string GetSourceUserCfgPath()
        {
            var simPath = _settingsManager.CurrentSettings?.SimPath
                ?? throw new InvalidOperationException("SimPath is not configured.");

            return Path.Combine(simPath, "..", "UserCfg.opt");
        }

        public void ApplySelectedConfig()
        {
            if (string.IsNullOrWhiteSpace(SelectedConfigId))
                return;

            var sourcePath = GetConfigFilePath(SelectedConfigId);
            if (!_fileStorage.FileExists(sourcePath))
                throw new FileNotFoundException("Config file not found.", sourcePath);

            var destPath = GetSourceUserCfgPath();
            _fileStorage.CopyFile(sourcePath, destPath, overwrite: true);

            _currentConfigId = SelectedConfigId;
            IsDirty = true;

            _logManager.Info($"Config '{SelectedConfigId}' applied to simulator.", "ConfigManager");
        }
    }
}