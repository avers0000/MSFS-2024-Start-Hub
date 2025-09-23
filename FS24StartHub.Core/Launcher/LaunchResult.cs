using FS24StartHub.Core.Launcher.Progress;

namespace FS24StartHub.Core.Launcher
{
    /// <summary>
    /// Result of launch execution.
    /// </summary>
    public sealed class LaunchResult
    {
        /// <summary>
        /// True if all steps completed successfully.
        /// </summary>
        public bool Success { get; init; } = true;

        /// <summary>
        /// Optional error message if something failed.
        /// </summary>
        public string? ErrorMessage { get; init; }

        /// <summary>
        /// What should happen with the app after launch.
        /// </summary>
        public ExitIntent ExitIntent { get; init; } = ExitIntent.None;

        /// <summary>
        /// List of executed steps with their results.
        /// </summary>
        public IReadOnlyList<StepProgress> Steps { get; init; } = new List<StepProgress>();
    }
}