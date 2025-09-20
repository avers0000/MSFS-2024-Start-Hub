using FS24StartHub.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FS24StartHub.Core.Settings
{
    /// <summary>
    /// Result of simulator detection attempt.
    /// </summary>
    /// <param name="SimType">Detected simulator type (Store, Steam, Custom), or <c>null</c> if not detected.</param>
    /// <param name="SimPath">Root path of the simulator installation, or <c>null</c> if unknown.</param>
    /// <param name="PackageFamilyName">Package family name (for Store version), or <c>null</c> if not applicable.</param>
    /// <param name="SimExePath">Full path to simulator executable (for Custom version), or <c>null</c> if not applicable.</param>
    public record SimulatorDetectionResult(
        SimType? SimType,
        string SimPath,
        string? PackageFamilyName,
        string? SimExePath
    );
}
