namespace FS24StartHub.Core.Domain
{
    /// <summary>
    /// Settings that define how many autosaves are retained.
    /// </summary>
    public class CleanupSettings
    {
        public int CareerAutosaveLimit { get; set; }
        public int ConfigAutosaveLimit { get; set; }
    }
}
