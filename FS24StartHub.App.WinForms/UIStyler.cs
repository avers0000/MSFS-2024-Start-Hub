using FS24StartHub.App.WinForms.Controls;
using Svg;
using System.Drawing.Drawing2D;
using System.IO;

public static class UIStyler
{
    public static void StyleButton(Button btn)
    {
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.BackColor = Color.FromArgb(60, 0, 0, 0);
        btn.ForeColor = Color.White;

        btn.FlatAppearance.MouseOverBackColor = Color.White;
        btn.FlatAppearance.MouseDownBackColor = Color.LightGray;

        btn.MouseEnter += (s, e) =>
        {
            if (btn.Enabled)
                btn.ForeColor = Color.Black;
        };

        btn.MouseLeave += (s, e) =>
        {
            if (btn.Enabled)
                btn.ForeColor = Color.White;
        };
        
        btn.Paint += (s, e) =>
        {
            if (!btn.Enabled)
            {
                TextRenderer.DrawText(
                    e.Graphics,
                    btn.Text,
                    btn.Font,
                    btn.ClientRectangle,
                    Color.Gray,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                );
            }
        };
    }

    public static void StyleStartButton(Button btn)
    {
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;

        btn.BackColor = Color.FromArgb(20, 72, 147);
        btn.ForeColor = Color.White;
        //btn.Font = new Font(btn.Font.FontFamily, btn.Font.Size + 2, FontStyle.Bold);
        btn.UseCompatibleTextRendering = true;

        btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 222, 3);
        btn.FlatAppearance.MouseDownBackColor = Color.White;

        Color svgColor = Color.FromArgb(80, 255, 255, 255);
        float strokeWidth = 8f;

        btn.MouseEnter += (s, e) =>
        {
            if (!btn.Enabled) return;

            // единый цвет для текста и иконки
            Color hoverColor = Color.FromArgb(200, 80, 80, 80); // тёмно‑серый, но не чёрный

            btn.ForeColor = hoverColor;
            svgColor = hoverColor;
            btn.Invalidate();
        };

        btn.MouseLeave += (s, e) =>
        {
            if (!btn.Enabled) return;

            btn.ForeColor = Color.White;
            svgColor = Color.FromArgb(80, 255, 255, 255); // исходный светлый цвет
            btn.Invalidate();
        };

        btn.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            var iconRect = new Rectangle(
                btn.Width - btn.Height + 1,
                1,
                btn.Height - 2,
                btn.Height - 2
            );

            try
            {
                string svgPath = Path.Combine(Application.StartupPath, "Resources", "FS24SH-arrow.svg");
                var svgDoc = SvgDocument.Open(svgPath);

                // Allow strokes to render outside viewBox
                svgDoc.Overflow = SvgOverflow.Visible;

                foreach (var path in svgDoc.Children.OfType<SvgPath>())
                {
                    path.Stroke = new SvgColourServer(svgColor);
                    path.StrokeWidth = strokeWidth;
                }

                using (var bmp = svgDoc.Draw(iconRect.Width, iconRect.Height))
                {
                    e.Graphics.DrawImage(bmp, iconRect);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SVG load error: " + ex.Message);
            }
        };
    }

    public static void ApplyStyleToAllButtons(Control parent)
    {
        foreach (Control ctrl in parent.Controls)
        {
            if (ctrl is Button btn)
            {
                switch (btn.Tag?.ToString())
                {
                    case "Start":
                        StyleStartButton(btn);
                        break;
                    default:
                        StyleButton(btn);
                        break;
                }
            }

            if (ctrl.HasChildren)
                ApplyStyleToAllButtons(ctrl);
        }
    }

    public static void StyleCheckBox(CheckBox cb)
    {
        cb.FlatStyle = FlatStyle.Flat;
        cb.Appearance = Appearance.Normal;
        cb.BackColor = Color.Transparent;
        cb.ForeColor = Color.White;
        cb.AutoSize = false;

        cb.Padding = new Padding(30, 0, 0, 0);

        int textWidth = TextRenderer.MeasureText(cb.Text, cb.Font).Width;
        cb.Width = textWidth + cb.Padding.Left + 6;
        cb.Height = Math.Max(cb.Height, 24);

        cb.Paint += (s, e) =>
        {
            if (s == null) return;
            var b = (CheckBox)s;
            e.Graphics.Clear(Color.Transparent);

            var boxSize = 18;
            var boxRect = new Rectangle(4, (b.Height - boxSize) / 2, boxSize, boxSize);

            using (var backBrush = new SolidBrush(Color.FromArgb(20, 72, 147)))
                e.Graphics.FillRectangle(backBrush, boxRect);

            if (b.Checked)
            {
                using (var pen = new Pen(Color.White, 2))
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    var points = new[]
                    {
                        new Point(boxRect.Left + 3, boxRect.Top + 8),
                        new Point(boxRect.Left + 7, boxRect.Bottom - 6),
                        new Point(boxRect.Right - 4, boxRect.Top + 4)
                    };

                    e.Graphics.DrawLines(pen, points);
                }
            }
            var textRect = new Rectangle(b.Padding.Left, 0, b.Width - b.Padding.Left, b.Height);
            TextRenderer.DrawText(
                e.Graphics,
                b.Text,
                b.Font,
                textRect,
                b.Enabled ? Color.White : Color.Gray,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Left
            );

            if (b.Focused)
            {
                var rect = new Rectangle(b.Padding.Left, 0, b.Width - b.Padding.Left, b.Height);
                rect.Inflate(-1, -1);
                using (var pen = new Pen(Color.Gray, 1))
                    e.Graphics.DrawRectangle(pen, rect);
            }
        };
    }

    public static void StyleCustomCheckedListBox(CustomCheckedListBox listBox)
    {
        if (listBox == null) return;
        listBox.CheckBoxMode = CustomCheckedListBox.CheckBoxDrawMode.Custom;

        listBox.CustomCheckBoxBackColor = Color.FromArgb(20, 72, 147);
        listBox.CustomCheckMarkColor = Color.White;
        listBox.CustomCheckBoxBorder = false;

        listBox.BackColor = Color.FromArgb(24, 36, 54);

        listBox.ItemBackColor = Color.FromArgb(24, 36, 54);
        listBox.ItemSelectedColor = Color.FromArgb(64, 96, 128);
        listBox.ReadonlyForeColor = Color.SkyBlue;
    }
}