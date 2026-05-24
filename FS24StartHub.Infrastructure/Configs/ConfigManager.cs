using FS24StartHub.Core.Configs;
using FS24StartHub.Core.Domain;
using FS24StartHub.Core.Launcher.Tasks;
using FS24StartHub.Core.Logging;
using FS24StartHub.Core.Settings;

namespace FS24StartHub.Infrastructure.Configs
{
    public class ConfigManager : IConfigManager
    {
        private readonly ISettingsManager _settingsManager;
        private readonly ILogManager _logManager;
        private List<Config> _configs = [];
        private string? _currentConfigId;

        public bool IsDirty { get; private set; }
        public string? CurrentConfigId => _currentConfigId;

        public event Action? DataChanged;

        public ConfigManager(ISettingsManager settingsManager, ILogManager logManager)
        {
            _settingsManager = settingsManager;
            _logManager = logManager;

            _settingsManager.SettingsReloaded += LoadData;

            LoadData();
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
            // TODO: implement SaveConfigManagerTask
            throw new NotImplementedException();
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
    }
}