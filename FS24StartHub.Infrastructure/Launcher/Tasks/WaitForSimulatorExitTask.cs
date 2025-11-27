using FS24StartHub.Core.Launcher;
using FS24StartHub.Core.Launcher.Progress;
using FS24StartHub.Core.Launcher.Tasks;
using FS24StartHub.Core.Logging;
using FS24StartHub.Infrastructure.Helpers;

namespace FS24StartHub.Infrastructure.Launcher.Tasks
{
    public class WaitForSimulatorExitTask : ILaunchTask
    {
        private readonly ILogManager _logManager;

        public string Name => "Wait for Simulator Exit";
        public bool IsOptional => false;

        public WaitForSimulatorExitTask(ILogManager logManager)
        {
            _logManager = logManager;
        }

        public async Task<StepProgress> ExecuteAsync(LaunchRequest request, IProgress<StepProgress>? progress, CancellationToken ct)
        {
            _logManager.Info("Waiting for simulator to exit...", "WaitForSimulatorExitTask");
            progress?.Report(new StepProgress(Name, ProgressType.StepStarted, "Waiting for simulator to exit..."));

            while (Utility.IsSimulatorRunning())
            {
                ct.ThrowIfCancellationRequested();
                await Task.Delay(10000, ct); // Check every second
            }

            _logManager.Info("Simulator has exited.", "WaitForSimulatorExitTask");
            progress?.Report(new StepProgress(Name, ProgressType.StepCompleted, "Simulator has exited."));

            return new StepProgress(Name, ProgressType.StepCompleted, "Completed");
        }
    }
}