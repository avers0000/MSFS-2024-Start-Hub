using FS24StartHub.Core.Launcher.Progress;

namespace FS24StartHub.Core.Launcher
{
    /// <summary>
    /// Orchestrates the launch pipeline.
    /// </summary>
    public interface ISimLauncherManager
    {
        /// <summary>
        /// Executes the launch pipeline for the given request.
        /// </summary>
        Task<LaunchResult> LaunchAsync(LaunchRequest request, IProgress<StepProgress>? progress, CancellationToken ct);
    }
}