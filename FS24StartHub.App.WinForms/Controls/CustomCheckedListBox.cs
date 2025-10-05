namespace FS24StartHub.App.WinForms.Controls
{
    public class CustomCheckedListBox : CheckedListBox
    {
        private readonly ToolTip _toolTip = new ToolTip();
        private bool isInitializing = false;

        public CustomCheckedListBox()
        {
            DrawMode = DrawMode.OwnerDrawVariable;
            MouseMove += CustomCheckedListBox_MouseMove;
        }

        public void LoadItems(IEnumerable<CustomCheckedListBoxItem> items)
        {
            isInitializing = true;
            Items.Clear();

            foreach (var item in items)
            {
                Items.Add(item, item.Enabled);
            }
            isInitializing = false;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= Items.Count) return;

            var item = Items[e.Index] as CustomCheckedListBoxItem;

            if (item == null || !item.Readonly)
            {
                base.OnDrawItem(e);
                return;
            }

            e.DrawBackground();

            using (var brush = new SolidBrush(e.ForeColor))
            {
                var font = e.Font ?? SystemFonts.DefaultFont;
                e.Graphics.DrawString(
                    item.Text,
                    font,
                    brush,
                    e.Bounds.X + 18,
                    e.Bounds.Y
                );
            }

            CheckBoxRenderer.DrawCheckBox(
                e.Graphics,
                new Point(e.Bounds.X + 1, e.Bounds.Y + (e.Bounds.Height - 13) / 2),
                GetItemChecked(e.Index)
                    ? System.Windows.Forms.VisualStyles.CheckBoxState.CheckedDisabled
                    : System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedDisabled
            );

            e.DrawFocusRectangle();
        }

        private void CustomCheckedListBox_MouseMove(object? sender, MouseEventArgs e)
        {
            int index = IndexFromPoint(e.Location);
            if (index >= 0 && Items[index] is CustomCheckedListBoxItem item && !string.IsNullOrEmpty(item.Tooltip))
            {
                _toolTip.SetToolTip(this, item.Tooltip);
            }
            else
            {
                _toolTip.RemoveAll();
            }
        }

        protected override void OnItemCheck(ItemCheckEventArgs ice)
        {
            var item = Items[ice.Index] as CustomCheckedListBoxItem;

            if (item == null || !item.Readonly || isInitializing)
            {
                base.OnItemCheck(ice);
                return;
            }

            ice.NewValue = ice.CurrentValue;
        }
    }
}