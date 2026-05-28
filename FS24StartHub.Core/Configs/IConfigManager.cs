using FS24StartHub.Core.Domain;
using FS24StartHub.Core.Settings;

namespace FS24StartHub.Core.Configs
{
    public interface IConfigManager : ISaveable
    {
        /// <summary>
        /// Returns the id of the currently applied config.
        /// </summary>
        string? CurrentConfigId { get; }

        /// <summary>
        /// Returns all saved config snapshots.
        /// </summary>
        IEnumerable<Config> GetConfigs();

        string? SelectedConfigId { get; }

        void SelectConfig(string? id);

        void UpdateConfig(Config config);

        string GetConfigFilePath(string configId);

        void DeleteConfig(string configId);
        bool IsCurrentConfigUpToDate();
        void SaveCurrentConfig(string? name = null);
    }
}