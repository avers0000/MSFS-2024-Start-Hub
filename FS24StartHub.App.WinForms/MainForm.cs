using FS24StartHub.App.WinForms.Controls;
using FS24StartHub.Core.Apps;
using FS24StartHub.Core.Domain;
using FS24StartHub.Core.Launcher;
using FS24StartHub.Core.Logging;
using FS24StartHub.Core.Settings;
using FS24StartHub.Infrastructure.Launcher;

namespace FS24StartHub.App.WinForms
{
    public partial class MainForm : Form
    {
        private readonly ISettingsManager _settingsManager;
        private readonly IAppsManager _appsManager;
        private readonly ILogManager _logManager;

        private bool isUpdating = false;

        public MainForm(ISettingsManager settingsManager, IAppsManager appsManager, ILogManager logManager)
        {
            _settingsManager = settingsManager;
            _appsManager = appsManager;
            _logManager = logManager;

            InitializeComponent();

            // Load startup items on form load
            LoadStartupItems();

            // Subscribe to changes in AppsManager
            _appsManager.DataChanged += OnStartupItemsChanged;
        }

        private void LoadStartupItems()
        {
            if (isUpdating) return;

            isUpdating = true;

            // Remember the currently selected item
            var selectedItem = clbApps.SelectedItem as CustomCheckedListBoxItem;

            clbApps.Items.Clear();
            var listItems = new List<CustomCheckedListBoxItem>();
            listItems.AddRange(_appsManager.GetStartupItems(RunOption.BeforeSimStarts).Select(item => new CustomCheckedListBoxItem(
                item.Id,
                item.DisplayName ?? item.Path,
                item.Enabled,
                !string.IsNullOrWhiteSpace(item.DisplayName) ? item.Path : null
            )));

            listItems.Add(new CustomCheckedListBoxItem(
                "fake_item",
                "--- Launch Flight Simulator ---",
                true,
                null,
                true
            ));

            listItems.AddRange(_appsManager.GetStartupItems(RunOption.AfterSimStarts).Select(item => new CustomCheckedListBoxItem(
                item.Id,
                item.DisplayName ?? item.Path,
                item.Enabled,
                !string.IsNullOrWhiteSpace(item.DisplayName) ? item.Path : null
            )));

            clbApps.LoadItems(listItems);

            // Try to restore the previous selection
            if (selectedItem != null)
            {
                clbApps.SelectedItem = clbApps.Items
                    .OfType<CustomCheckedListBoxItem>()
                    .FirstOrDefault(item => item.Id == selectedItem.Id);
            }

            // If nothing is selected, select the first real item
            if (clbApps.SelectedItem == null)
            {
                clbApps.SelectedItem = clbApps.Items
                    .OfType<CustomCheckedListBoxItem>()
                    .FirstOrDefault();
            }

            isUpdating = false;

            // Update button states after loading items
            UpdateAppsButtonsStates();
        }

        private void OnStartupItemsChanged()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(OnStartupItemsChanged));
                return;
            }

            if (isUpdating) return;

            // Update the list of startup items
            LoadStartupItems();

            // Call OnServiceDataChanged to enable the Save button
            OnServiceDataChanged();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var request = new LaunchRequest();
            var simLauncherManager = new SimLauncherManager(_logManager, _settingsManager, _appsManager);

            using var startForm = new StartForm(simLauncherManager, _logManager, request);
            var result = startForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                Close();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void clbApps_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (clbApps.Items[e.Index] is CustomCheckedListBoxItem item)
            {
                _appsManager.SetStartupItemEnabled(item.Id, e.NewValue == CheckState.Checked);
            }
        }

        private void btnAppsMoveUp_Click(object sender, EventArgs e)
        {
            if (clbApps.SelectedItem is CustomCheckedListBoxItem selectedItem && !selectedItem.Readonly)
            {
                // Call MoveStartupItem with moveDown = false (move up)
                _appsManager.MoveStartupItem(selectedItem.Id, moveDown: false);

                // Selection will be updated automatically via OnStartupItemsChanged
            }
        }

        private void btnAppsMoveDown_Click(object sender, EventArgs e)
        {
            if (clbApps.SelectedItem is CustomCheckedListBoxItem selectedItem && !selectedItem.Readonly)
            {
                // Call MoveStartupItem with moveDown = true (move down)
                _appsManager.MoveStartupItem(selectedItem.Id, moveDown: true);

                // Selection will be updated automatically via OnStartupItemsChanged
            }
        }

        private void btnAppsReload_Click(object sender, EventArgs e)
        {
            _settingsManager.Load();
        }

        private void UpdateAppsButtonsStates()
        {
            if (clbApps.SelectedItem is CustomCheckedListBoxItem selectedItem && !selectedItem.Readonly)
            {
                int selectedIndex = clbApps.SelectedIndex;

                // Disable "Move Up" if the first item is selected
                btnAppsMoveUp.Enabled = selectedIndex > 0;

                // Disable "Move Down" if the last item is selected
                btnAppsMoveDown.Enabled = selectedIndex < clbApps.Items.Count - 1;

                btnAppsRemove.Enabled = true;
                btnAppsEdit.Enabled = true;
            }
            else
            {
                // Disable both buttons if nothing is selected or a fake item is selected
                btnAppsMoveUp.Enabled = false;
                btnAppsMoveDown.Enabled = false;
                btnAppsRemove.Enabled = false;
                btnAppsEdit.Enabled = false;
            }
        }

        private void clbApps_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAppsButtonsStates();
        }

        private void btnAppsRemove_Click(object sender, EventArgs e)
        {
            if (clbApps.SelectedItem is CustomCheckedListBoxItem selectedItem && !selectedItem.Readonly)
            {
                // Determine the next selection before removing the item
                int selectedIndex = clbApps.SelectedIndex;
                if (selectedIndex < clbApps.Items.Count - 1)
                {
                    // Select the next item if it exists
                    clbApps.SelectedIndex = selectedIndex + 1;
                }
                else if (selectedIndex > 0)
                {
                    // Otherwise, select the previous item
                    clbApps.SelectedIndex = selectedIndex - 1;
                }

                // Remove the selected item
                _appsManager.RemoveStartupItem(selectedItem.Id);
            }
        }

        private void btnAppsAdd_Click(object sender, EventArgs e)
        {
            using var form = new StartupItemForm(_appsManager);
            form.ShowDialog();
        }

        private void btnAppsEdit_Click(object sender, EventArgs e)
        {
            if (clbApps.SelectedItem is CustomCheckedListBoxItem selectedItem && !selectedItem.Readonly)
            {
                using var form = new StartupItemForm(_appsManager, selectedItem.Id);
                form.ShowDialog();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                _settingsManager.Save([_appsManager]); 
                MessageBox.Show("Settings saved successfully.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false; // Disable the button after saving
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnServiceDataChanged()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(OnServiceDataChanged));
                return;
            }

            btnSave.Enabled = true; // Enable the Save button when settings change
        }
    }
}
