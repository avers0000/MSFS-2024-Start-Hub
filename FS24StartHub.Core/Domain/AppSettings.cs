namespace FS24StartHub.Core.Domain
{
    /// <summary>
    /// Root application settings, serialized to fs24sh.json.
    /// </summary>
    public class AppSettings
    {
        public string Language { get; set; } = string.Empty;
        public string SimPath { get; set; } = string.Empty;

        public SimType? SimType { get; set; }
        public string? PackageFamilyName { get; set; }
        public string? SimExePath { get; set; }
        public int LaunchTimeoutSeconds { get; set; } = 30;

        public string CurrentCareerId { get; set; } = string.Empty;
        public string CurrentConfigId { get; set; } = string.Empty;

        public CleanupSettings CleanupSettings { get; set; } = new();
        public List<Career> Careers { get; set; } = new();
        public List<Config> Configs { get; set; } = new();
        public List<StartupItem> StartupItems { get; set; } = new();

        public AppSettings Clone()
        {
            // Создаем поверхностную копию
            var clone = (AppSettings)MemberwiseClone();

            // Глубокая копия изменяемых свойств
            clone.CleanupSettings = CleanupSettings.Clone();
            clone.Careers = [.. Careers.Select(c => c.Clone())];
            clone.Configs = [.. Configs.Select(c => c.Clone())];
            clone.StartupItems = [.. StartupItems.Select(s => s.Clone())];

            return clone;
        }
    }
}
