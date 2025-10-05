namespace FS24StartHub.App.WinForms.Controls
{
    public class CustomCheckedListBoxItem
    {
        public string Id { get; set; }
        public string Text { get; set; }

        public bool Enabled { get; set; } = false;
        public string? Tooltip { get; set; }
        public bool Readonly { get; set; }

        public CustomCheckedListBoxItem(string id, string text, bool enabled = false, string? tooltip = null, bool @readonly = false)
        {
            Id = id;
            Text = text;
            Enabled = enabled;
            Tooltip = tooltip;
            Readonly = @readonly;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}