using FS24StartHub.Core.Configs;
using FS24StartHub.Core.Domain;

namespace FS24StartHub.App.WinForms
{
    public partial class ConfigDetailsForm : Form
    {
        private readonly IConfigManager _configManager;
        private readonly Config _config;

        public ConfigDetailsForm(IConfigManager configManager, Config config)
        {
            InitializeComponent();
            _configManager = configManager;
            _config = config;
        }

        private void ConfigDetailsForm_Load(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(47, 52, 57);
            ForeColor = Color.White;
            tlpConfigDetails.BackColor = Color.FromArgb(38, 42, 46);

            UIStyler.ApplyStyleToAllButtons(this);

            Text = $"Config details — {_config.Name}";

            txtName.Text = _config.Name;
            txtDescription.Text = _config.Description ?? string.Empty;

            lblCreatedValue.Text = _config.CreatedDate.ToString("g");

            lblLastUsedValue.Text = _config.LastUsed.ToString("g");

            LoadPreview();
        }

        private void LoadPreview()
        {
            rtbPreview.Clear();

            var snapshotPath = _configManager.GetConfigFilePath(_config.Id);
            if (string.IsNullOrEmpty(snapshotPath) || !File.Exists(snapshotPath))
            {
                rtbPreview.Text = "(no snapshot available)";
                return;
            }

            rtbPreview.Text = File.ReadAllText(snapshotPath);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var name = txtName.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show(
                    "Name cannot be empty.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            _config.Name = name;
            _config.Description = string.IsNullOrWhiteSpace(txtDescription.Text)
                ? null
                : txtDescription.Text.Trim();

            try
            {
                _configManager.UpdateConfig(_config);
                DialogResult = DialogResult.OK;
                Close();
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}