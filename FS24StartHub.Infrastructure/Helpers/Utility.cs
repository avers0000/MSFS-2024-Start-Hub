using System.Diagnostics;

namespace FS24StartHub.Infrastructure.Helpers
{
    /// <summary>
    /// General-purpose utility methods. Сontainer for shared logic.
    /// </summary>
    public static class Utility
    {
        public static bool IsSimulatorRunning()
        {
            // MSFS 2024 process name must match exactly (without ".exe")
            return Process.GetProcessesByName("FlightSimulator2024").Any();
        }
    }
}
