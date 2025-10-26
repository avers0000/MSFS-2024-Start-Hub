using FS24StartHub.Core.Apps;
using FS24StartHub.Core.Domain;
using FS24StartHub.Core.Launcher;
using FS24StartHub.Core.Launcher.Progress;
using FS24StartHub.Core.Launcher.Tasks;
using FS24StartHub.Core.Logging;
using System.Diagnostics;

namespace FS24StartHub.Infrastructure.Launcher.Tasks
{
    public class StartupItemsGroupTask : ILaunchTask
    {
        private readonly IAppsManager _appsManager;
        private readonly ILogManager _logManager;
        private readonly RunOption _runOption;

        public StartupItemsGroupTask(IAppsManager appsManager, ILogManager logManager, RunOption runOption)
        {
            _appsManager = appsManager;
            _logManager = logManager;
            _runOption = runOption;
        }

        public string Name => $"Run {_runOption} Apps & Scripts";

        public bool IsOptional => true; // All StartupItems tasks are considered optional

        public async Task<StepProgress> ExecuteAsync(LaunchRequest request, IProgress<StepProgress>? progress, CancellationToken ct)
        {
            var sw = Stopwatch.StartNew();

            var startupItems = _appsManager.GetStartupItems(_runOption).Where(item => item.Enabled).OrderBy(item => item.Order).ToList();

            foreach (var item in startupItems)
            {
                ct.ThrowIfCancellationRequested();

                progress?.Report(new StepProgress(Name, ProgressType.Info, $"Executing: {item.DisplayName ?? item.Path}"));

                if (item.DelayBeforeMs.HasValue)
                {
                    await Task.Delay(item.DelayBeforeMs.Value, ct);
                }

                ExecuteStartupItem(item);

                await Task.Delay(item.DelayAfterMs ?? 200, ct);
            }

            sw.Stop();
            return new StepProgress(Name, ProgressType.StepCompleted, $"{_runOption} Apps & Scripts run successfully", null, sw.Elapsed, true, null);
        }

        private void ExecuteStartupItem(StartupItem item)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(item.Path))
                {
                    _logManager.Warn($"Startup item '{item.DisplayName ?? item.Path}' has an invalid or empty path.", "StartupItemsGroupTask");
                    return;
                }

                if (!File.Exists(item.Path))
                {
                    _logManager.Warn($"{item.Type} '{item.DisplayName ?? item.Path}' does not exist at the specified path.", "StartupItemsGroupTask");
                    return;
                }

                _logManager.Info($"Starting execution of {item.Type}: {item.DisplayName ?? item.Path}", "StartupItemsGroupTask");

                var startInfo = new ProcessStartInfo
                {
                    FileName = item.Type == StartupItemType.App ? item.Path : "powershell.exe",
                    Arguments = item.Type == StartupItemType.Script ? $"-ExecutionPolicy Bypass -NoExit -File \"{item.Path}\"" : string.Empty,
                    UseShellExecute = true, // Use shell execution to open the window
                    CreateNoWindow = false, // Ensure the window is visible
                    RedirectStandardOutput = false, // Do not redirect output
                    RedirectStandardError = false  // Do not redirect errors
                };

                using var process = Process.Start(startInfo);
                if (process == null)
                {
                    _logManager.Error($"Failed to start process for startup item: {item.DisplayName ?? item.Path}", "StartupItemsGroupTask");
                    return;
                }

                _logManager.Info($"{item.Type} '{item.DisplayName ?? item.Path}' started successfully.", "StartupItemsGroupTask");
            }
            catch (Exception ex)
            {
                _logManager.Error($"Error while starting {item.Type} '{item.DisplayName ?? item.Path}': {ex.Message}", "StartupItemsGroupTask", ex);
            }
        }
    }
}