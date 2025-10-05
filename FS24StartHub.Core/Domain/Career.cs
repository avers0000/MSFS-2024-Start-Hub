using System.Text.Json.Serialization;

namespace FS24StartHub.Core.Domain
{
    /// <summary>
    /// Represents a career profile with its metadata.
    /// </summary>
    public class Career
    {
        public string Id { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime LastUsed { get; set; }
        public string CurrentDump { get; set; } = string.Empty;

        [JsonIgnore]
        public List<CareerDump> Dumps { get; set; } = new();

        public Career Clone()
        {
            var clone = (Career)MemberwiseClone();
            clone.Dumps = [.. Dumps.Select(d => d.Clone())];
            return clone;
        }
    }
}
