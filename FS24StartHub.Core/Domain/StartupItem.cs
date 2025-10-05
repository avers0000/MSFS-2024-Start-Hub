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
        public int? DelayBeforeMs { get; set; }
        public int? DelayAfterMs { get; set; }
        public string? DisplayName { get; set; }

        public StartupItem() { }

        public StartupItem(StartupItem other)
        {
            Id = other.Id;
            Type = other.Type;
            Path = other.Path;
            RunOption = other.RunOption;
            Order = other.Order;
            Enabled = other.Enabled;
            DelayBeforeMs = other.DelayBeforeMs;
            DelayAfterMs = other.DelayAfterMs;
            DisplayName = other.DisplayName;
        }
    }

}
