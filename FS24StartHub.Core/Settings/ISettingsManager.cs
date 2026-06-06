using FS24StartHub.Core.Domain;

namespace FS24StartHub.Core.Settings
{
    public interface ISettingsManager
    {
        /// <summary>
        /// Returns the currently loaded application settings, or null if not loaded yet.
        /// </summary>
        AppSettings? CurrentSettings { get; }

        /// <summary>
        /// Loads settings from disk (fs24sh.json).
        /// </summary>
        AppSettings Load();

        /// <summary>
        /// Updates and saves settings to disk.
        /// </summary>
        void Update(AppSettings settings);

        /// <summary>
        /// Updates the list of startup items in CurrentSettings.
        /// </summary>
        void UpdateStartupItems(IEnumerable<StartupItem> items);

        /// <summary>
        /// Updates the list of startup items and saves to disk.
        /// </summary>
        void SaveStartupItems(IEnumerable<StartupItem> items);

        /// <summary>
        /// Validates simulator-related configuration.
        /// </summary>
        bool ValidateSimConfiguration(AppSettings settings);

        /// <summary>
        /// Saves all changes from the provided services to disk.
        /// </summary>
        void Save(IEnumerable<ISaveable> saveableServices);

        /// <summary>
        /// Triggered when settings are reloaded from disk.
        /// </summary>
        event Action? SettingsReloaded;

        /// <summary>
        /// Triggered when settings are changed in memory.
        /// </summary>
        event Action? SettingsChanged;

        /// <summary>
        /// Updates configs list in CurrentSettings.
        /// </summary>
        void UpdateConfigs(IEnumerable<Config> configs);

        /// <summary>
        /// Updates current config id in CurrentSettings.
        /// </summary>
        void UpdateCurrentConfigId(string? currentConfigId);

        /// <summary>
        /// Updates configs list and saves to disk.
        /// </summary>
        void SaveConfigs(IEnumerable<Config> configs);

        /// <summary>
        /// Updates current config id and saves to disk.
        /// </summary>
        void SaveCurrentConfigId(string? currentConfigId);

        /// <summary>
        /// Returns the base folder path for settings.
        /// </summary>
        string BaseFolderPath { get; }
    }
}
