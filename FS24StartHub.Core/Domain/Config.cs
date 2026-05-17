namespace FS24StartHub.Core.Domain
{
    /// <summary>
    /// Represents a configuration snapshot.
    /// </summary>
    public class Config
    {
        public string Id { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUsed { get; set; }
        public int? Rating { get; set; }

        public Config Clone()
        {
            return (Config)MemberwiseClone();
        }
    }
}