namespace FS24StartHub.Core.Settings
{
    public interface ISimulatorDetector
    {
        /// <summary>
        /// Attempts to detect installed simulator and returns detection result.
        /// </summary>
        /// <returns>
        /// <see cref="SimulatorDetectionResult"/> containing detected simulator type and paths,
        /// or empty values if detection failed.
        /// </returns>
        SimulatorDetectionResult? Detect();
    }
}
