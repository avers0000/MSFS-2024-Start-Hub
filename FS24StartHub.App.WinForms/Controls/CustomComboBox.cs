namespace FS24StartHub.App.WinForms.Controls
{
    public class CustomComboBox : ComboBox
    {
        public CustomComboBox()
        {
            SetStyle(ControlStyles.UserPaint, true);
            DrawMode = DrawMode.OwnerDrawFixed;
            FlatStyle = FlatStyle.Flat;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            var item = Items[e.Index];
            string? text = GetItemText(item); // Considers DisplayMember

            Color backColor = (e.State & DrawItemState.Selected) == DrawItemState.Selected
                ? ControlPaint.Light(BackColor)
                : BackColor;

            using (var backBrush = new SolidBrush(backColor))
                e.Graphics.FillRectangle(backBrush, e.Bounds);

            using (var textBrush = new SolidBrush(ForeColor))
            {
                var sf = new StringFormat { LineAlignment = StringAlignment.Center };
                // Ensure e.Font is not null before using it
                var font = e.Font ?? Font ?? SystemFonts.DefaultFont;
                e.Graphics.DrawString(text ?? string.Empty, font, textBrush, e.Bounds, sf);
            }

            e.DrawFocusRectangle();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Background
            using (var backBrush = new SolidBrush(BackColor))
                e.Graphics.FillRectangle(backBrush, ClientRectangle);

            // Border (using background color)
            using (var pen = new Pen(BackColor, 1))
                e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);

            // Arrow
            int arrowX = Width - 15;
            int arrowY = Height / 2 - 2;
            Point[] arrowPoints = {
                new Point(arrowX, arrowY),
                new Point(arrowX + 8, arrowY),
                new Point(arrowX + 4, arrowY + 5)
            };
            using (var brush = new SolidBrush(ForeColor))
                e.Graphics.FillPolygon(brush, arrowPoints);

            // Text of the selected item
            using (var textBrush = new SolidBrush(ForeColor))
            {
                var rect = new Rectangle(2, 2, Width - 20, Height - 4);
                var sf = new StringFormat { LineAlignment = StringAlignment.Center };
                e.Graphics.DrawString(Text, Font, textBrush, rect, sf);
            }
        }
    }
}
