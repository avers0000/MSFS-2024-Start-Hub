using FS24StartHub.Core.Launcher;
using FS24StartHub.Core.Logging;
using FS24StartHub.Core.Settings;
using FS24StartHub.Infrastructure.Launcher;
using FS24StartHub.Infrastructure.Logging;

namespace FS24StartHub.App.WinForms
{
    public partial class MainForm : Form
    {
        private readonly ISettingsManager _settingsManager;
        private readonly ILogManager _logManager;

        public MainForm(ISettingsManager settingsManager, ILogManager logManager)
        {
            _settingsManager = settingsManager;
            _logManager = logManager;
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var request = new LaunchRequest();
            var simLauncherManager = new SimLauncherManager(_logManager, _settingsManager);

            using var startForm = new StartForm(simLauncherManager, _logManager, request);
            var result = startForm.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                Close();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
