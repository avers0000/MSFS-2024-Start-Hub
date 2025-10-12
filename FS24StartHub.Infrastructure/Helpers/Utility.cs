using System.ComponentModel;
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

        /// <summary>
        /// Retrieves the description attribute of an enum value, or its name if no description is provided.
        /// </summary>
        /// <param name="value">The enum value.</param>
        /// <returns>The description of the enum value, or its name if no description is provided.</returns>
        public static string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                .FirstOrDefault() as DescriptionAttribute;
            return attribute?.Description ?? value.ToString();
        }
    }
}
