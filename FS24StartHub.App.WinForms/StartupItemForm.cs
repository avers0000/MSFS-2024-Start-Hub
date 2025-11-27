using FS24StartHub.Core.Apps;
using FS24StartHub.Core.Domain;
using FS24StartHub.Infrastructure.Helpers;

namespace FS24StartHub.App.WinForms
{
    public partial class StartupItemForm : Form
    {
        private readonly IAppsManager _appsManager;
        private StartupItem StartupItem { get; set; }

        public StartupItemForm(IAppsManager appsManager, string? id = null)
        {
            InitializeComponent();
            _appsManager = appsManager;

            // Use the updated method to retrieve the StartupItem by ID
            if (!string.IsNullOrEmpty(id))
            {
                StartupItem = _appsManager.GetStartupItemById(id)
                    ?? throw new ArgumentException($"StartupItem with Id '{id}' not found.");
            }
            else
            {
                StartupItem = new StartupItem
                {
                    Enabled = true // Set Enabled to true for new items
                };
            }

            // Set the form title based on the mode (Add or Edit)
            Text = id == null ? "Add Startup Item" : "Edit Startup Item";

            // Initialize controls with the StartupItem data
            InitializeControls();
        }

        private void InitializeControls()
        {
            // Populate the ComboBox with descriptions of StartupItemType
            var types = Enum.GetValues(typeof(StartupItemType))
                .Cast<StartupItemType>()
                .Select(type => new { Value = type, Description = Utility.GetEnumDescription(type) })
                .ToList();

            cmbType.DataSource = types;
            cmbType.DisplayMember = "Description"; // Show the description in the ComboBox
            cmbType.ValueMember = "Value";         // Keep the enum value as the underlying data
            cmbType.SelectedValue = StartupItem.Type; // Select the current value

            txtPath.Text = StartupItem.Path;
            cmbRunOption.DataSource = Enum.GetValues(typeof(RunOption))
                    .Cast<RunOption>()
                    .Select(option => new { Value = option, Description = Utility.GetEnumDescription(option) })
                    .ToList();
            cmbRunOption.DisplayMember = "Description";
            cmbRunOption.ValueMember = "Value";
            cmbRunOption.SelectedValue = StartupItem.RunOption;
            txtDisplayName.Text = StartupItem.DisplayName ?? string.Empty;
            numDelayBefore.Value = (StartupItem.DelayBeforeMs ?? 0) / 1000;
            numDelayAfter.Value = (StartupItem.DelayAfterMs ?? 0) / 1000;

            // Add event handler for Type change
            cmbType.SelectedIndexChanged += cmbType_SelectedIndexChanged;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                return; // Stop saving if validation fails
            }

            StartupItem.Type = cmbType.SelectedValue as StartupItemType? ?? StartupItemType.App;
            StartupItem.Path = txtPath.Text;
            StartupItem.RunOption = cmbRunOption.SelectedValue as RunOption? ?? RunOption.BeforeSimStarts;
            StartupItem.DisplayName = string.IsNullOrWhiteSpace(txtDisplayName.Text) ? null : txtDisplayName.Text;
            StartupItem.DelayBeforeMs = numDelayBefore.Value == 0 ? null : (int?)(numDelayBefore.Value * 1000);
            StartupItem.DelayAfterMs = numDelayAfter.Value == 0 ? null : (int?)(numDelayAfter.Value * 1000);

            // Check for duplicates across the entire list, excluding the current item if updating
            var allItems = _appsManager.GetStartupItems(RunOption.BeforeSimStarts)
                .Concat(_appsManager.GetStartupItems(RunOption.AfterSimStarts))
                .Where(item => item.Id != StartupItem.Id) // Exclude the current item
                .ToList();

            // Check for duplicate DisplayName
            if (!string.IsNullOrWhiteSpace(StartupItem.DisplayName))
            {
                var duplicateDisplayNameItem = allItems.FirstOrDefault(item =>
                    !string.IsNullOrWhiteSpace(item.DisplayName) &&
                    item.DisplayName.Equals(StartupItem.DisplayName, StringComparison.OrdinalIgnoreCase));

                if (duplicateDisplayNameItem != null)
                {
                    MessageBox.Show(
                        "An item with the same display name already exists. Please choose a different name.",
                        "Duplicate Display Name",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return; // Cancel the operation
                }
            }

            // Check for duplicate Path
            var duplicatePathItem = allItems.FirstOrDefault(item => item.Path.Equals(StartupItem.Path, StringComparison.OrdinalIgnoreCase));
            if (duplicatePathItem != null)
            {
                var result = MessageBox.Show(
                    "An item with the same path already exists. Are you sure you want to add a duplicate?",
                    "Duplicate Path",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    return; // Cancel the operation if the user does not confirm
                }
            }

            if (string.IsNullOrEmpty(StartupItem.Id))
            {
                StartupItem.Id = Guid.NewGuid().ToString();
                _appsManager.AddStartupItem(StartupItem);
            }
            else
            {
                _appsManager.UpdateStartupItem(StartupItem);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cmbType_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // Update the file filter based on the selected type
            var selectedType = cmbType.SelectedValue as StartupItemType? ?? StartupItemType.App;
            UpdateFileDialogFilter(selectedType);
        }

        private void UpdateFileDialogFilter(StartupItemType type)
        {
            switch (type)
            {
                case StartupItemType.App:
                    // Include only executable files
                    ofdPath.Filter = "Executable Files|*.exe;*.com;*.bat;*.cmd";
                    break;
                case StartupItemType.Script:
                    // Include only PowerShell scripts
                    ofdPath.Filter = "PowerShell Script Files|*.ps1";
                    break;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (ofdPath.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = ofdPath.FileName;

                // Automatically pre-fill DisplayName only for new items
                if (string.IsNullOrEmpty(StartupItem.Id))
                {
                    if (Path.GetExtension(ofdPath.FileName).Equals(".exe", StringComparison.OrdinalIgnoreCase))
                    {
                        // Try to get the ProductName
                        var fileInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(ofdPath.FileName);
                        txtDisplayName.Text = !string.IsNullOrWhiteSpace(fileInfo.ProductName)
                            ? fileInfo.ProductName
                            : Path.GetFileName(ofdPath.FileName); // Fallback to file name with extension
                    }
                    else
                    {
                        // Use the file name with extension for other file types
                        txtDisplayName.Text = Path.GetFileName(ofdPath.FileName);
                    }
                }
            }
        }

        private bool ValidateForm()
        {
            // Validate Path
            if (string.IsNullOrWhiteSpace(txtPath.Text))
            {
                MessageBox.Show("Path cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!File.Exists(txtPath.Text))
            {
                MessageBox.Show("The specified file does not exist.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void StartupItemForm_Load(object sender, EventArgs e)
        {
            BackColor = ColorTranslator.FromHtml("#2f3439");
            ForeColor = Color.White;
            tlpStartupItem.BackColor = ColorTranslator.FromHtml("#262a2e");
            UIStyler.ApplyStyleToAllButtons(this);
            UIStyler.ApplyStyleToAllComboBoxes(this);
        }
    }
}
