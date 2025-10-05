namespace FS24StartHub.Core.Domain
{
    /// <summary>
    /// Metadata of a career dump, stored in dump.json inside the dump folder.
    /// </summary>
    public class CareerDump
    {
        public string Id { get; set; } = string.Empty;
        public string FolderName { get; set; } = string.Empty;
        public string? Name { get; set; }
        public bool IsAutoSave { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Description { get; set; }

        public CareerDump Clone()
        {
            return (CareerDump)MemberwiseClone();
        }
    }
}
