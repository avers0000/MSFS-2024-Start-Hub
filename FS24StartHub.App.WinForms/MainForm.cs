using FS24StartHub.Core.Logging;
using FS24StartHub.Core.Settings;
using FS24StartHub.Infrastructure.Logging;

namespace FS24StartHub.App.WinForms
{
    public partial class MainForm : Form
    {
        private readonly ISettingsManager _settingsManager;

        public MainForm(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
            InitializeComponent();
        }
    }
}
