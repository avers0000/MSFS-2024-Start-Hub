using FS24StartHub.Core.Domain;
using FS24StartHub.Core.Logging;
using FS24StartHub.Core.Settings;
using FS24StartHub.Infrastructure.Settings;
using FS24StartHub.Infrastructure.Storage;

namespace FS24StartHub.App.WinForms
{
    public partial class SettingsForm : Form
    {
        private readonly ISettingsManager _settingsManager;
        private readonly ILogManager _logManager;
        private readonly ISimulatorDetector _simulatorDetector;

        private readonly bool _firstRunMode;

        public SettingsForm(
            ISettingsManager settingsManager,
            ILogManager logManager,
            bool firstRunMode = false)
        {
            _settingsManager = settingsManager;
            _logManager = logManager;

            _simulatorDetector = new SimulatorDetector(new FileStorage());

            _firstRunMode = firstRunMode;

            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            BackColor = ColorTranslator.FromHtml("#2f3439");
            ForeColor = Color.White;
            tlpCustom.BackColor = ColorTranslator.FromHtml("#262a2e");

            UIStyler.ApplyStyleToAllButtons(this);
            UIStyler.StyleCheckBox(chbUseCustom);

            var settings = _settingsManager.CurrentSettings;

            ShowDetectedInfo(settings);

            bool isCustom = settings?.SimType == SimType.Custom;
            chbUseCustom.Checked = _firstRunMode || isCustom;

            if (isCustom)
            {
                txtCustomSimPath.Text = settings?.SimPath ?? string.Empty;
                txtCustomSimExePath.Text = settings?.SimExePath ?? string.Empty;
            }

            UpdateCustomPanelVisibility();
        }

        private void ShowDetectedInfo(AppSettings? settings)
        {
            if (settings == null)
            {
                txtInstallationType.ForeColor = Color.Goldenrod;
                txtPackagesPath.ForeColor = Color.Goldenrod;
                txtInstallationType.Text = "Simulator was not found automatically.";
                txtPackagesPath.Text = "Enter paths manually to continue.";
                return;
            }

            txtInstallationType.ForeColor = Color.White;
            txtPackagesPath.ForeColor = Color.White;
            txtInstallationType.Text = settings.SimType?.ToString() ?? "—";
            txtPackagesPath.Text = string.IsNullOrWhiteSpace(settings.SimPath) ? "—" : settings.SimPath;
        }

        private void UpdateCustomPanelVisibility()
        {
            tlpCustom.Visible = chbUseCustom.Checked;
        }

        private void chbUseCustom_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCustomPanelVisibility();
        }

        private void btnRedetect_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            try
            {
                var result = _simulatorDetector.Detect();

                if (result == null)
                {
                    MessageBox.Show(
                        "Installation not found. Enter paths manually.",
                        "Simulator Not Found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                var current = _settingsManager.CurrentSettings;
                bool matches = current != null
                    && current.SimType == result.SimType
                    && string.Equals(current.SimPath, result.SimPath, StringComparison.OrdinalIgnoreCase);

                if (matches)
                {
                    MessageBox.Show(
                        "Detected installation matches current settings.",
                        "No Changes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                var answer = MessageBox.Show(
                    $"New installation parameters detected ({result.SimType}, {result.SimPath}).\nReplace current settings?",
                    "New Installation Found",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (answer == DialogResult.Yes)
                {
                    ApplyDetectionResult(result);
                    _logManager.Info($"Redetect: settings updated to {result.SimType} at {result.SimPath}.", "SettingsForm", "RedetectApplied");
                }
            }
            catch (Exception ex)
            {
                _logManager.Error("Redetect failed with exception.", "SettingsForm", ex);
                MessageBox.Show(
                    $"Detection failed: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                this.Enabled = true;
            }
        }

        private void ApplyDetectionResult(SimulatorDetectionResult result)
        {
            var current = _settingsManager.CurrentSettings ?? new AppSettings();
            current.SimType = result.SimType;
            current.SimPath = result.SimPath;
            current.PackageFamilyName = result.PackageFamilyName;
            current.SimExePath = result.SimExePath;

            _settingsManager.Update(current);
            ShowDetectedInfo(current);
        }

        private void btnBrowseSimPath_Click(object sender, EventArgs e)
        {
            using var dialog = new FolderBrowserDialog
            {
                Description = "Select simulator packages folder (contains Community and Official subfolders)",
                UseDescriptionForTitle = true,
            };

            var current = txtCustomSimPath.Text?.Trim();
            if (!string.IsNullOrWhiteSpace(current))
                dialog.InitialDirectory = current;

            if (dialog.ShowDialog() == DialogResult.OK)
                txtCustomSimPath.Text = dialog.SelectedPath;
        }

        private void btnBrowseSimExePath_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Title = "Select simulator executable",
                Filter = "Flight Simulator 2024|FlightSimulator2024.exe"
            };

            var current = txtCustomSimExePath.Text?.Trim();
            if (!string.IsNullOrWhiteSpace(current))
                dialog.InitialDirectory = Path.GetDirectoryName(current);

            if (dialog.ShowDialog() == DialogResult.OK)
                txtCustomSimExePath.Text = dialog.FileName;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (chbUseCustom.Checked)
            {
                if (!ValidateCustomFields())
                    return;

                var current = _settingsManager.CurrentSettings ?? new AppSettings();
                current.SimType = SimType.Custom;
                current.SimPath = txtCustomSimPath.Text?.Trim() ?? string.Empty;
                current.SimExePath = txtCustomSimExePath.Text?.Trim() ?? string.Empty;
                current.PackageFamilyName = null;

                if (!_settingsManager.ValidateSimConfiguration(current))
                {
                    ShowValidationError("Simulator configuration is invalid. Please check the paths.");
                    return;
                }
                _settingsManager.Update(current);
                _logManager.Info(
                    $"Custom settings saved: SimPath={current.SimPath}, SimExePath={current.SimExePath}",
                    "SettingsForm", "CustomSettingsSaved");
            }
            else
            {
                if (_settingsManager.CurrentSettings == null)
                {
                    ShowValidationError(
                        "Simulator is not configured.\n" +
                        "Use Redetect or enable custom settings to configure simulator.");
                    return;
                }
            }

            DialogResult = DialogResult.OK;

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private bool ValidateCustomFields()
        {
            var simPath = txtCustomSimPath.Text?.Trim() ?? string.Empty;
            var simExePath = txtCustomSimExePath.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(simPath))
            {
                ShowValidationError("Simulator packages path cannot be empty.");
                return false;
            }

            if (!Directory.Exists(simPath))
            {
                ShowValidationError($"Simulator packages path does not exist:\n{simPath}");
                return false;
            }

            if (string.IsNullOrWhiteSpace(simExePath))
            {
                ShowValidationError("Simulator executable path cannot be empty for Custom mode.");
                return false;
            }

            if (!File.Exists(simExePath))
            {
                ShowValidationError($"Simulator executable not found:\n{simExePath}");
                return false;
            }

            return true;
        }

        private void ShowValidationError(string message)
        {
            MessageBox.Show(message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
