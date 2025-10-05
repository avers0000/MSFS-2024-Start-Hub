using FS24StartHub.Core.Apps;
using FS24StartHub.Core.Domain;
using FS24StartHub.Core.Logging;
using FS24StartHub.Core.Settings;

namespace FS24StartHub.Infrastructure.Apps
{
    public class AppsManager : IAppsManager
    {
        private readonly ISettingsManager _settingsManager;
        private readonly ILogManager _logManager;
        private readonly List<StartupItem> _beforeStartupItems = [];
        private readonly List<StartupItem> _afterStartupItems = [];

        public bool IsDirty { get; private set; }

        public event Action? StartupItemsChanged;

        public AppsManager(ISettingsManager settingsManager, ILogManager logManager)
        {
            _settingsManager = settingsManager;
            _logManager = logManager;

            // Subscribe to SettingsManager events
            _settingsManager.SettingsReloaded += LoadData;

            // Initialize data
            LoadData();
            _logManager.Info("AppsManager initialized with startup items.", "AppsManager");
            IsDirty = false;
        }

        public IEnumerable<StartupItem> GetStartupItems(RunOption runOption)
        {
            // Return a copy of the list to prevent external modifications
            return GetTargetList(runOption).ToList();
        }

        public void AddStartupItem(StartupItem item, int? index = null)
        {
            var targetList = GetTargetList(item.RunOption);
            item.Id = Guid.NewGuid().ToString();

            if (index.HasValue && index.Value >= 0 && index.Value <= targetList.Count)
            {
                targetList.Insert(index.Value, item);
            }
            else
            {
                targetList.Add(item); // Add to the end if no index is specified
            }

            _logManager.Info($"Startup item added: {item.DisplayName ?? item.Path}", "AppsManager", "ItemAdded");
            ReorderStartupItems();
            OnStartupItemsChanged();
        }

        public void UpdateStartupItem(StartupItem updatedItem)
        {
            var existingItem = _beforeStartupItems.FirstOrDefault(i => i.Id == updatedItem.Id)
                               ?? _afterStartupItems.FirstOrDefault(i => i.Id == updatedItem.Id);

            if (existingItem != null)
            {
                // Update all fields except Order and RunOption
                existingItem.Type = updatedItem.Type;
                existingItem.Path = updatedItem.Path;
                existingItem.Enabled = updatedItem.Enabled;
                existingItem.DelayBeforeMs = updatedItem.DelayBeforeMs;
                existingItem.DelayAfterMs = updatedItem.DelayAfterMs;
                existingItem.DisplayName = updatedItem.DisplayName;

                // If RunOption changed, move the item to the new list
                if (existingItem.RunOption != updatedItem.RunOption)
                {
                    var removedItem = RemoveStartupItem(existingItem.Id);
                    if (removedItem != null)
                    {
                        removedItem.RunOption = updatedItem.RunOption;
                        AddStartupItem(removedItem);
                    }
                }

                _logManager.Info($"Startup item updated: {updatedItem.DisplayName ?? updatedItem.Path}", "AppsManager", "ItemUpdated");
                ReorderStartupItems();
                OnStartupItemsChanged();
            }
        }

        public void SetStartupItemEnabled(string id, bool isEnabled)
        {
            var item = _beforeStartupItems.FirstOrDefault(i => i.Id == id)
                       ?? _afterStartupItems.FirstOrDefault(i => i.Id == id);

            if (item != null)
            {
                item.Enabled = isEnabled;
                _logManager.Info($"Startup item '{item.DisplayName ?? item.Path}' enabled state set to {isEnabled}.", "AppsManager", "ItemEnabledUpdated");
                OnStartupItemsChanged();
            }
            else
            {
                _logManager.Warn($"Startup item with ID '{id}' not found.", "AppsManager", "ItemNotFound");
            }
        }

        public StartupItem? RemoveStartupItem(string id)
        {
            var item = _beforeStartupItems.FirstOrDefault(i => i.Id == id)
                       ?? _afterStartupItems.FirstOrDefault(i => i.Id == id);

            if (item != null)
            {
                var targetList = GetTargetList(item.RunOption);
                targetList.Remove(item);
                _logManager.Info($"Startup item removed: {item.DisplayName ?? item.Path}", "AppsManager", "ItemRemoved");
                ReorderStartupItems();
                OnStartupItemsChanged();
            }

            return item;
        }

        public void MoveStartupItem(string id, bool moveDown)
        {
            var item = _beforeStartupItems.FirstOrDefault(i => i.Id == id)
                       ?? _afterStartupItems.FirstOrDefault(i => i.Id == id);

            if (item == null)
            {
                _logManager.Warn($"Startup item with ID '{id}' not found.", "AppsManager", "ItemNotFound");
                return;
            }

            // Determine the current list and index
            var currentList = GetTargetList(item.RunOption);
            var currentIndex = currentList.IndexOf(item);

            // Calculate the new index
            int newIndex = moveDown ? currentIndex + 1 : currentIndex - 1;

            // Check if the item needs to move to another group
            if (newIndex < 0 || newIndex >= currentList.Count)
            {
                if (item.RunOption == RunOption.BeforeSimStarts && moveDown)
                {
                    // Move from the first group to the second group
                    currentList.RemoveAt(currentIndex);
                    item.RunOption = RunOption.AfterSimStarts;
                    _afterStartupItems.Insert(0, item);
                }
                else if (item.RunOption == RunOption.AfterSimStarts && !moveDown)
                {
                    // Move from the second group to the first group
                    currentList.RemoveAt(currentIndex);
                    item.RunOption = RunOption.BeforeSimStarts;
                    _beforeStartupItems.Add(item);
                }
                else
                {
                    // No valid move
                    return;
                }
            }
            else
            {
                // Move within the current list
                currentList.RemoveAt(currentIndex);
                currentList.Insert(newIndex, item);
            }

            _logManager.Info($"Startup item '{item.DisplayName ?? item.Path}' moved to index {newIndex} in group {item.RunOption}.", "AppsManager", "ItemMoved");
            ReorderStartupItems();
            OnStartupItemsChanged();
        }

        public void SaveChanges()
        {
            if (IsDirty)
            {
                try
                {
                    // Combine lists before saving
                    var allItems = _beforeStartupItems.Concat(_afterStartupItems).ToList();
                    _settingsManager.UpdateStartupItems(allItems);
                    IsDirty = false;
                    _logManager.Info("Startup items saved successfully.", "AppsManager", "ItemsSaved");
                }
                catch (Exception ex)
                {
                    _logManager.Error("Failed to save startup items.", "AppsManager", ex);
                    throw;
                }
            }
        }

        private void ReorderStartupItems()
        {
            for (int i = 0; i < _beforeStartupItems.Count; i++)
                _beforeStartupItems[i].Order = i + 1;

            for (int i = 0; i < _afterStartupItems.Count; i++)
                _afterStartupItems[i].Order = i + 1;
        }

        private List<StartupItem> GetTargetList(RunOption runOption)
        {
            return runOption == RunOption.BeforeSimStarts
                ? _beforeStartupItems
                : _afterStartupItems;
        }

        private void OnStartupItemsChanged()
        {
            IsDirty = true;
            StartupItemsChanged?.Invoke();
        }

        private void LoadData()
        {
            _beforeStartupItems.Clear();
            _afterStartupItems.Clear();

            var allItems = _settingsManager.CurrentSettings?.StartupItems ?? new List<StartupItem>();

            _beforeStartupItems.AddRange([..allItems
                .Where(i => i.RunOption == RunOption.BeforeSimStarts)
                .OrderBy(i => i.Order)]);

            _afterStartupItems.AddRange([..allItems
                .Where(i => i.RunOption == RunOption.AfterSimStarts)
                .OrderBy(i => i.Order)]);

            StartupItemsChanged?.Invoke();
        }
    }
}