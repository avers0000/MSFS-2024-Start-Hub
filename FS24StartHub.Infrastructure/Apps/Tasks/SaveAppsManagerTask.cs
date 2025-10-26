using FS24StartHub.Core.Launcher.Tasks;
using FS24StartHub.Core.Logging;
using FS24StartHub.Core.Apps;
using FS24StartHub.Core.Launcher.Progress;
using FS24StartHub.Core.Launcher;
using System.Diagnostics;

namespace FS24StartHub.Infrastructure.Apps.Tasks
{
    public class SaveAppsManagerTask : ILaunchTask
    {
        private readonly IAppsManager _appsManager;
        private readonly ILogManager _logManager;
        private const string Module = "SaveAppsManagerTask";

        public SaveAppsManagerTask(IAppsManager appsManager, ILogManager logManager)
        {
            _appsManager = appsManager;
            _logManager = logManager;
        }

        public string Name => "Save AppsManager Data";

        public bool IsOptional => false;

        public Task<StepProgress> ExecuteAsync(LaunchRequest request, IProgress<StepProgress>? progress, CancellationToken ct)
        {
            var sw = Stopwatch.StartNew(); // Start measuring time
            try
            {
                _logManager.Info("Saving AppsManager data...", Module);
                _appsManager.SaveChanges();
                sw.Stop(); // Stop measuring time
                return Task.FromResult(new StepProgress(Name, ProgressType.StepCompleted, "Data saved successfully", null, sw.Elapsed, true, null));
            }
            catch (UnauthorizedAccessException ex)
            {
                sw.Stop(); // Stop measuring time
                _logManager.Error("Access denied while saving AppsManager data.", Module, ex);
                return Task.FromResult(new StepProgress(Name, ProgressType.StepCompleted, "Failed to save data", null, sw.Elapsed, false, "Access denied"));
            }
            catch (IOException ex)
            {
                sw.Stop(); // Stop measuring time
                _logManager.Error("I/O error occurred while saving AppsManager data.", Module, ex);
                return Task.FromResult(new StepProgress(Name, ProgressType.StepCompleted, "Failed to save data", null, sw.Elapsed, false, "I/O error"));
            }
            catch (InvalidOperationException ex)
            {
                sw.Stop(); // Stop measuring time
                _logManager.Error("Invalid operation while saving AppsManager data.", Module, ex);
                return Task.FromResult(new StepProgress(Name, ProgressType.StepCompleted, "Failed to save data", null, sw.Elapsed, false, "Invalid operation"));
            }
            catch (Exception ex)
            {
                sw.Stop(); // Stop measuring time
                _logManager.Error("Unexpected error occurred while saving AppsManager data.", Module, ex);
                return Task.FromResult(new StepProgress(Name, ProgressType.StepCompleted, "Failed to save data", null, sw.Elapsed, false, "Unexpected error"));
            }
        }
    }
}