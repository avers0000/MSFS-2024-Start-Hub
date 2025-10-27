using FS24StartHub.Core.Launcher;
using FS24StartHub.Core.Launcher.Progress;
using FS24StartHub.Core.Logging;
using FS24StartHub.Infrastructure.Helpers;

namespace FS24StartHub.App.WinForms
{
    public partial class StartForm : Form
    {
        private CancellationTokenSource? cts;
        private readonly ISimLauncherManager _simLauncherManager;
        private readonly ILogManager _logManager;
        private readonly LaunchRequest _request;

        public StartForm(ISimLauncherManager simLauncherManager, ILogManager logManager, LaunchRequest request)
        {
            InitializeComponent();
            _simLauncherManager = simLauncherManager;
            _logManager = logManager;
            _request = request;
        }

        private async void StartForm_Load(object sender, EventArgs e)
        {
            cts = new CancellationTokenSource();

            var progress = new Progress<StepProgress>(step =>
            {
                lblStatus.Text = $"{step.StepName}: {step.Message}";
            });

            try
            {
                var result = await _simLauncherManager.LaunchAsync(_request, progress, cts.Token);

                if (result.Success)
                {
                    DialogResult = _request.KeepAppOpen ? DialogResult.Cancel : DialogResult.OK;
                }
                else
                {
                    DialogResult = DialogResult.Abort;
                }
            }
            catch (OperationCanceledException)
            {
                _logManager.Warn("Launch aborted by user.", "StartForm", "Cancellation");
                MessageBox.Show("Launch aborted by user.", "Aborted",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = Utility.IsSimulatorRunning() ? DialogResult.OK : DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                _logManager.Error("Unhandled exception in StartForm", "StartForm", ex);
                MessageBox.Show("Unexpected error occurred.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = Utility.IsSimulatorRunning() ? DialogResult.OK : DialogResult.Abort;
            }
            finally
            {
                Close();
            }
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            btnAbort.Enabled = false;
            cts?.Cancel();
        }
    }
}
