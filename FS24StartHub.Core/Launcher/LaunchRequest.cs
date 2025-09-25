namespace FS24StartHub.Core.Launcher
{
    /// <summary>
    /// Immutable input collected from UI when user presses Start.
    /// </summary>
    public sealed class LaunchRequest
    {
        public bool KeepAppOpen { get; init; } = false;
    }
}