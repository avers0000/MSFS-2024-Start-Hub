using FS24StartHub.Core.Configs;
using FS24StartHub.Core.Launcher;
using FS24StartHub.Core.Launcher.Progress;
using FS24StartHub.Core.Launcher.Tasks;
using FS24StartHub.Core.Logging;
using System.Diagnostics;

public sealed class ApplyConfigTask : ILaunchTask
{
    private readonly IConfigManager _configManager;
    private readonly ILogManager _logManager;
    private const string Module = "ApplyConfigTask";

    public string Name => "Apply Graphics Profile";
    public bool IsOptional => false;

    public ApplyConfigTask(IConfigManager configManager, ILogManager logManager)
    {
        _configManager = configManager;
        _logManager = logManager;
    }

    public Task<StepProgress> ExecuteAsync(LaunchRequest request, IProgress<StepProgress>? progress, CancellationToken ct)
    {
        var sw = Stopwatch.StartNew();
        try
        {
            var selected = _configManager.SelectedConfigId;
            var current = _configManager.CurrentConfigId;

            if (string.IsNullOrWhiteSpace(selected) || selected == current)
            {
                sw.Stop();
                _logManager.Info("No profile to apply, skipping.", Module);
                return Task.FromResult(new StepProgress(Name, ProgressType.StepCompleted, "Skipped", null, sw.Elapsed, true, null));
            }

            _configManager.ApplySelectedConfig();

            sw.Stop();
            _logManager.Info($"Profile '{selected}' applied successfully.", Module);
            return Task.FromResult(new StepProgress(Name, ProgressType.StepCompleted, "Profile applied", null, sw.Elapsed, true, null));
        }
        catch (IOException ex)
        {
            sw.Stop();
            _logManager.Error($"Failed to apply profile: {ex.Message}", Module, ex);
            return Task.FromResult(new StepProgress(Name, ProgressType.StepCompleted, "Failed to apply profile", null, sw.Elapsed, false, ex.Message));
        }
    }
}