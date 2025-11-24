namespace FS24StartHub.App.WinForms.Controls
{
    public class CustomComboBox : ComboBox
    {
        public CustomComboBox()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.FlatStyle = FlatStyle.Flat;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            var item = this.Items[e.Index];
            string text = this.GetItemText(item); // учитывает DisplayMember

            Color backColor = (e.State & DrawItemState.Selected) == DrawItemState.Selected
                ? ControlPaint.Light(this.BackColor)
                : this.BackColor;

            using (var backBrush = new SolidBrush(backColor))
                e.Graphics.FillRectangle(backBrush, e.Bounds);

            using (var textBrush = new SolidBrush(this.ForeColor))
            {
                var sf = new StringFormat { LineAlignment = StringAlignment.Center };
                e.Graphics.DrawString(text, e.Font, textBrush, e.Bounds, sf);
            }

            e.DrawFocusRectangle();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // фон
            using (var backBrush = new SolidBrush(this.BackColor))
                e.Graphics.FillRectangle(backBrush, this.ClientRectangle);

            // рамка (цветом фона)
            using (var pen = new Pen(this.BackColor, 1))
                e.Graphics.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);

            // стрелка
            int arrowX = this.Width - 15;
            int arrowY = this.Height / 2 - 2;
            Point[] arrowPoints = {
                new Point(arrowX, arrowY),
                new Point(arrowX + 8, arrowY),
                new Point(arrowX + 4, arrowY + 5)
            };
            using (var brush = new SolidBrush(this.ForeColor))
                e.Graphics.FillPolygon(brush, arrowPoints);

            // текст выбранного элемента
            using (var textBrush = new SolidBrush(this.ForeColor))
            {
                var rect = new Rectangle(2, 2, this.Width - 20, this.Height - 4);
                var sf = new StringFormat { LineAlignment = StringAlignment.Center };
                e.Graphics.DrawString(this.Text, this.Font, textBrush, rect, sf);
            }
        }
    }
}
