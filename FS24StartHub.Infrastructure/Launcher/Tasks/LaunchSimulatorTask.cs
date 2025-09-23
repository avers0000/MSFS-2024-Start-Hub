using FS24StartHub.Core.Launcher;
using FS24StartHub.Core.Launcher.Progress;
using FS24StartHub.Core.Launcher.Tasks;
using FS24StartHub.Core.Logging;
using FS24StartHub.Infrastructure.Helpers;
using System.ComponentModel;
using System.Diagnostics;

namespace FS24StartHub.Infrastructure.Launcher.Tasks
{
    public sealed class LaunchSimulatorTask : ILaunchTask
    {
        private readonly ILogManager _logManager;
        private const string Module = "LaunchSimulatorTask";

        public LaunchSimulatorTask(ILogManager logManager)
        {
            _logManager = logManager;
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

                // Заглушка: позже заменим на Process.Start
                await Task.Delay(100, ct);

                _logManager.Info("Simulator launched successfully.", Module);
                return new StepProgress(Name, ProgressType.StepCompleted, "Simulator launched successfully", null, sw.Elapsed, true, null);
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
    }
}