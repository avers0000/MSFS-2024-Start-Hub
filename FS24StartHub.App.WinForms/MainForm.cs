using FS24StartHub.App.WinForms.Controls;
using FS24StartHub.Core.Apps;
using FS24StartHub.Core.Configs;
using FS24StartHub.Core.Domain;
using FS24StartHub.Core.Launcher;
using FS24StartHub.Core.Logging;
using FS24StartHub.Core.Settings;
using FS24StartHub.Infrastructure.Helpers;
using FS24StartHub.Infrastructure.Launcher;

namespace FS24StartHub.App.WinForms
{
    public partial class MainForm : Form
    {
        private readonly ISettingsManager _settingsManager;
        private readonly IAppsManager _appsManager;
        private readonly ILogManager _logManager;
        private readonly IConfigManager _configManager;

        private bool isUpdating = false;

        public MainForm(ISettingsManager settingsManager, IAppsManager appsManager, IConfigManager configManager, ILogManager logManager)
        {
            _settingsManager = settingsManager;
            _appsManager = appsManager;
            _configManager = configManager;
            _logManager = logManager;

            InitializeComponent();

            // Subscribe to changes in AppsManager
            _appsManager.DataChanged += OnStartupItemsChanged;
            _configManager.DataChanged += OnConfigsDataChanged;

            // Hide debug buttons in release mode
#if !DEBUG
            btnSave.Visible = false;
            btnAppsReload.Visible = false;
#endif
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

        private void LoadConfigs()
        {
            dgvConfigs.Rows.Clear();
            foreach (var config in _configManager.GetConfigs())
            {
                var createdDate = config.CreatedDate == default ? "" : config.CreatedDate.ToString("d");
                var rowIndex = dgvConfigs.Rows.Add("", config.Name ?? string.Empty, createdDate, "", "", "");
                var row = dgvConfigs.Rows[rowIndex];
                row.Tag = config;
                row.Cells["colMarker"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            ApplyConfigRowStyles();
            dgvConfigs.ClearSelection();
            dgvConfigs.CurrentCell = null;
        }

        private void ApplyConfigRowStyles()
        {
            foreach (DataGridViewRow row in dgvConfigs.Rows)
            {
                var config = row.Tag as Config;
                var configId = config?.Id;
                bool isCurrent = config?.IsCurrent ?? false;
                bool isSelected = configId == _configManager.SelectedConfigId;

                var color = isCurrent ? Color.Yellow : (isSelected ? Color.Lime : Color.White);

                row.DefaultCellStyle.ForeColor = color;
                row.DefaultCellStyle.SelectionForeColor = color;

                row.Cells["colMarker"].Value = (isCurrent || isSelected) ? "●" : "";
                row.Cells["colMarker"].Style.ForeColor = color;
                row.Cells["colMarker"].Style.SelectionForeColor = color;
            }
        }

        private string? _lastClickedConfigId;

        private void dgvConfigs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvConfigs.Rows[e.RowIndex];

            if (e.ColumnIndex == colEdit.Index)
            {
                if (row.Tag is Config cfg)
                {
                    using var form = new ConfigDetailsForm(_configManager, cfg);
                    form.ShowDialog();
                }
                return;
            }

            if (e.ColumnIndex == colDelete.Index)
            {
                if (row.Tag is not Config cfg) return;
                if (cfg.IsCurrent) return;

                var name = cfg.Name ?? cfg.Id;
                var result = MessageBox.Show(
                    $"Delete config \"{name}\"?\n\nThis will permanently remove the config file.",
                    "Delete Config",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result != DialogResult.Yes) return;

                try
                {
                    _configManager.DeleteConfig(cfg.Id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Failed to delete config: {ex.Message}",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                return;
            }

            var clickedId = (row.Tag as Config)?.Id;

            if (clickedId == _lastClickedConfigId)
            {
                _configManager.SelectConfig(clickedId);
                ApplyConfigRowStyles();
                _lastClickedConfigId = null;
            }
            else
            {
                _lastClickedConfigId = clickedId;
            }
        }

        private void OnConfigsDataChanged()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(OnConfigsDataChanged));
                return;
            }

            LoadConfigs();
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

        private bool CheckRunningProcessesAndWarn()
        {
            var runningItems = _appsManager.GetStartupItems(RunOption.BeforeSimStarts)
                .Concat(_appsManager.GetStartupItems(RunOption.AfterSimStarts))
                .Where(item => item.Enabled && item.WarnIfRunning && Utility.IsProcessRunning(item.ProcessName))
                .ToList();

            if (!runningItems.Any())
                return true;

            var names = string.Join("\n", runningItems.Select(item => $"  • {item.DisplayName ?? item.ProcessName}"));
            var result = MessageBox.Show(
                $"The following applications are already running:\n\n{names}\n\nContinue anyway?",
                "Processes Already Running",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            return result == DialogResult.Yes;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!CheckRunningProcessesAndWarn())
                return;

            var request = new LaunchRequest
            {
                KeepAppOpen = chbKeepOpen.Checked
            };

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

        private void MainForm_Load(object sender, EventArgs e)
        {
            lblVersion.Text = "v" + Application.ProductVersion.Split('+')[0];

            // Load startup items on form load
            LoadStartupItems();
            LoadConfigs();

            UIStyler.ApplyStyleToAllButtons(this);
            UIStyler.StyleCheckBox(chbKeepOpen);
            UIStyler.StyleCustomCheckedListBox(clbApps);
            UIStyler.StyleDataGridView(dgvConfigs);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using var form = new SettingsForm(_settingsManager, _logManager);
            form.ShowDialog();
        }

        private void dgvConfigs_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == colEdit.Index) return;

            _lastClickedConfigId = null;
            _configManager.SelectConfig((dgvConfigs.Rows[e.RowIndex].Tag as Config)?.Id);
            ApplyConfigRowStyles();
        }

        private void dgvConfigs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && dgvConfigs.SelectedRows.Count > 0)
            {
                _configManager.SelectConfig((dgvConfigs.SelectedRows[0].Tag as Config)?.Id);
                ApplyConfigRowStyles();
                e.Handled = true;
            }
        }

        private async void btnCaptureConfig_Click(object sender, EventArgs e)
        {
            try
            {
                var upToDate = await _configManager.IsCurrentConfigUpToDateAsync();
                if (upToDate)
                {
                    MessageBox.Show(
                        "Current config is already up to date.",
                        "Save Config",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                await _configManager.SaveCurrentConfigAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to save config: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
