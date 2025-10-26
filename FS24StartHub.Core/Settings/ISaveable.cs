using FS24StartHub.Core.Launcher.Tasks;

namespace FS24StartHub.Core.Settings
{
    public interface ISaveable
    {
        /// <summary>
        /// Updates the corresponding section of the settings in memory (e.g., CurrentSettings).
        /// </summary>
        void UpdateChanges();

        /// <summary>
        /// Saves the corresponding section of the settings to disk.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Indicates whether there are unsaved changes in the service.
        /// </summary>
        /// <returns>True if there are unsaved changes, otherwise false.</returns>
        bool HasChanges();

        /// <summary>
        /// Provides a task for saving changes.
        /// </summary>
        /// <returns>A task for saving changes.</returns>
        ILaunchTask GetSaveTask();

        /// <summary>
        /// Triggered when the service detects changes in its data.
        /// </summary>
        event Action? DataChanged;
    }
}