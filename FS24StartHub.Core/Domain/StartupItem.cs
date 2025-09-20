namespace FS24StartHub.Core.Domain
{
    /// <summary>
    /// Defines an item that should be executed on startup.
    /// </summary>
    public class StartupItem
    {
        public string Id { get; set; } = string.Empty;
        public StartupItemType Type { get; set; }
        public string Path { get; set; } = string.Empty;
        public RunOption RunOption { get; set; } = RunOption.BeforeSimStarts;
        public int Order { get; set; }
        public bool Enabled { get; set; }
    }

}
