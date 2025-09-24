using FS24StartHub.Core.Launcher;
using FS24StartHub.Core.Launcher.Progress;
using FS24StartHub.Core.Launcher.Tasks;
using FS24StartHub.Core.Logging;
using FS24StartHub.Core.Settings;
using FS24StartHub.Infrastructure.Helpers;
using FS24StartHub.Infrastructure.Launcher.Tasks;

namespace FS24StartHub.Infrastructure.Launcher
{
    /// <summary>
    /// Orchestrates execution of launch tasks in sequence.
    /// Reports progress events and aggregates results.
    /// </summary>
    public sealed class SimLauncherManager : ISimLauncherManager
    {
        private readonly IReadOnlyList<ILaunchTask> _tasks;
        private readonly ILogManager _logManager;

        public SimLauncherManager(ILogManager logManager, ISettingsManager settingsManager)
        {
            _logManager = logManager;

            _tasks = new List<ILaunchTask>
            {
                new LaunchSimulatorTask(logManager, settingsManager)
            };
        }

        public async Task<LaunchResult> LaunchAsync(LaunchRequest request, IProgress<StepProgress>? progress, CancellationToken ct)
        {
            var steps = new List<StepProgress>();

            _logManager.Info("Launch pipeline started.", "SimLauncherManager");

            if (Utility.IsSimulatorRunning())
            {
                _logManager.Warn("Simulator is already running. Aborting launch pipeline.", "SimLauncherManager");
                return new LaunchResult
                {
                    Success = false,
                    ErrorMessage = "Simulator is already running",
                    Steps = steps
                };
            }

            foreach (var task in _tasks)
            {
                ct.ThrowIfCancellationRequested();

                _logManager.Info($"Task '{task.Name}' started.", "SimLauncherManager");
                progress?.Report(new StepProgress(task.Name, ProgressType.StepStarted, "Started"));

                var step = await task.ExecuteAsync(request, progress, ct);
                progress?.Report(step);

                steps.Add(step);
                if (!step.Success && !task.IsOptional)
                {
                    _logManager.Error($"Task '{task.Name}' failed: {step.Error}", "SimLauncherManager");
                    return new LaunchResult
                    {
                        Success = false,
                        ErrorMessage = step.Error,
                        Steps = steps
                    };
                }

                _logManager.Info($"Task '{task.Name}' completed. Success={step.Success}", "SimLauncherManager");
            }

            _logManager.Info("Launch pipeline completed successfully.", "SimLauncherManager");

            return new LaunchResult
            {
                Success = true,
                Steps = steps
            };
        }
    }
}