using FS24StartHub.Core.Domain;

namespace FS24StartHub.Core.Apps
{
    public interface IAppsManager
    {
        /// <summary>
        /// Event triggered when startup items are changed.
        /// </summary>
        event Action? StartupItemsChanged;

        /// <summary>
        /// Retrieves a copy of all startup items for the specified RunOption group.
        /// </summary>
        /// <param name="runOption">The group of startup items to retrieve.</param>
        /// <returns>A copy of the startup items in the specified group.</returns>
        IEnumerable<StartupItem> GetStartupItems(RunOption runOption);

        /// <summary>
        /// Adds a new startup item to the specified group.
        /// </summary>
        /// <param name="item">The startup item to add.</param>
        /// <param name="index">Optional index to insert the item at. If null, the item is added to the end.</param>
        void AddStartupItem(StartupItem item, int? index = null);

        /// <summary>
        /// Updates an existing startup item. If the RunOption changes, the item is moved to the new group.
        /// </summary>
        /// <param name="updatedItem">The updated startup item.</param>
        void UpdateStartupItem(StartupItem updatedItem);

        /// <summary>
        /// Removes a startup item by its ID.
        /// </summary>
        /// <param name="id">The ID of the startup item to remove.</param>
        /// <returns>The removed startup item, or null if not found.</returns>
        StartupItem? RemoveStartupItem(string id);

        /// <summary>
        /// Moves a startup item within its group to a new index.
        /// </summary>
        /// <param name="id">The ID of the startup item to move.</param>
        /// <param name="newIndex">The new index to move the item to.</param>
        void MoveStartupItem(string id, bool moveDown);

        /// <summary>
        /// Saves all changes to the startup items.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Updates the Enabled state of a startup item by its ID.
        /// </summary>
        /// <param name="id">The ID of the startup item to update.</param>
        /// <param name="isEnabled">The new Enabled state.</param>
        void SetStartupItemEnabled(string id, bool isEnabled);
    }
}