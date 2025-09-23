using FS24StartHub.Core.Launcher.Progress;

namespace FS24StartHub.Core.Launcher.Tasks
{ 
    public interface ILaunchTask
    {
        string Name { get; }
        bool IsOptional => false;

        Task<StepProgress> ExecuteAsync(LaunchRequest request, IProgress<StepProgress>? progress,CancellationToken ct);
    }
}