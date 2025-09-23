using FS24StartHub.Core.Domain;
using FS24StartHub.Core.Launcher;
using FS24StartHub.Core.Launcher.Progress;
using FS24StartHub.Core.Launcher.Tasks;
using FS24StartHub.Core.Logging;
using FS24StartHub.Core.Settings;
using FS24StartHub.Infrastructure.Helpers;
using System.ComponentModel;
using System.Diagnostics;

namespace FS24StartHub.Infrastructure.Launcher.Tasks
{
    public sealed class LaunchSimulatorTask : ILaunchTask
    {
        private readonly ILogManager _logManager;
        private readonly ISettingsManager _settingsManager;
        private const string Module = "LaunchSimulatorTask";

        public LaunchSimulatorTask(ILogManager logManager, ISettingsManager settingsManager)
        {
            _logManager = logManager;
            _settingsManager = settingsManager;
        }

        public string Name => "Launch Simulator";

        public async Task<StepProgress> ExecuteAsync(LaunchRequest request, IProgress<StepProgress>? progress, CancellationToken ct)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                _logManager.Info("Checking if simulator is running...", Module);
                progress?.Report(new StepProgress(Name, ProgressType.Info, "Checking if simulator is running"));

                if (Utility.IsSimulatorRunning())
                {
                    _logManager.Warn("Simulator already running. Launch aborted.", Module);
                    return new StepProgress(Name, ProgressType.StepCompleted, "Simulator already running", null, sw.Elapsed, false, "Simulator already running");
                }

                _logManager.Info("Starting simulator...", Module);
                progress?.Report(new StepProgress(Name, ProgressType.Info, "Starting simulator..."));

                var settings = _settingsManager.CurrentSettings!;
                var success = await TryLaunchSimulatorAsync(settings, ct);

                if (success)
                {
                    _logManager.Info("Simulator launched successfully.", Module);
                    return new StepProgress(Name, ProgressType.StepCompleted, "Simulator launched successfully", null, sw.Elapsed, true, null);
                }
                else
                {
                    _logManager.Error("Simulator did not start within timeout.", Module);
                    return new StepProgress(Name, ProgressType.StepCompleted, "Simulator did not start within timeout", null, sw.Elapsed, false, "Timeout expired");
                }
            }
            catch (Win32Exception ex)
            {
                _logManager.Error("Failed to start simulator: " + ex.Message, Module);
                return new StepProgress(Name, ProgressType.StepCompleted, "Failed to start simulator", null, sw.Elapsed, false, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logManager.Error("Invalid process configuration: " + ex.Message, Module);
                return new StepProgress(Name, ProgressType.StepCompleted, "Invalid process configuration", null, sw.Elapsed, false, ex.Message);
            }
        }

        /// <summary>
        /// Attempts to launch the simulator depending on SimType (Steam, Store, Custom).
        /// Waits for a configurable timeout and verifies that the simulator process is running.
        /// </summary>
        private async Task<bool> TryLaunchSimulatorAsync(AppSettings settings, CancellationToken ct)
        {
            ProcessStartInfo? startInfo;

            switch (settings.SimType)
            {
                case SimType.Steam:
                    startInfo = new ProcessStartInfo("steam://run/2537590")
                    {
                        UseShellExecute = true
                    };
                    break;

                case SimType.Store:
                    startInfo = new ProcessStartInfo
                    {
                        FileName = "explorer.exe",
                        Arguments = $"shell:appsFolder\\{settings.PackageFamilyName}!App",
                        UseShellExecute = true
                    };
                    break;

                case SimType.Custom:
                    startInfo = new ProcessStartInfo
                    {
                        FileName = settings.SimExePath!,
                        UseShellExecute = false
                    };
                    break;

                default:
                    _logManager.Error($"Unsupported SimType: {settings.SimType}", Module);
                    return false;
            }

            // Execute the launch command
            Process.Start(startInfo);

            // Wait for configurable grace period before checking process
            await Task.Delay(TimeSpan.FromSeconds(settings.LaunchTimeoutSeconds), ct);

            // Verify that the simulator process is running
            return Utility.IsSimulatorRunning();
        }

    }

}
