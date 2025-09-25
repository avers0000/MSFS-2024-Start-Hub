namespace FS24StartHub.Core.Launcher
{
    /// <summary>
    /// Defines what should happen with the app after launch.
    /// </summary>
    public enum ExitIntent
    {
        ShutdownAfterLaunch = 0,
        WaitForSimExit = 1,
        UnlockUi = 2
    }
}