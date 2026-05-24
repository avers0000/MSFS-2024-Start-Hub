using FS24StartHub.App.WinForms.Controls;
using Svg;
using System.Drawing.Drawing2D;
using System.Media;

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
            {
                btn.ForeColor = Color.Black;

                var stream = FS24StartHub.App.WinForms.Resources.ResourceManager.GetStream("fs24sh-hover");
                if (stream != null)
                    new SoundPlayer(stream).Play();
            }
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

    public static void StyleAccentButton(Button btn)
    {
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;

        // same colors as in StyleStartButton
        btn.BackColor = Color.FromArgb(20, 72, 147);   // dark blue background
        btn.ForeColor = Color.White;

        btn.FlatAppearance.MouseOverBackColor = Color.White;
        btn.FlatAppearance.MouseDownBackColor = Color.LightGray;

        btn.MouseEnter += (s, e) =>
        {
            if (btn.Enabled)
            {
                btn.ForeColor = Color.Black;
                var stream = FS24StartHub.App.WinForms.Resources.ResourceManager.GetStream("fs24sh-hover");
                if (stream != null)
                    new SoundPlayer(stream).Play();
            }
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

            // unified color for text and icon
            Color hoverColor = Color.FromArgb(200, 80, 80, 80); // dark gray, but not black

            btn.ForeColor = hoverColor;
            svgColor = hoverColor;
            var stream = FS24StartHub.App.WinForms.Resources.ResourceManager.GetStream("fs24sh-hover");
            if (stream != null)
                new SoundPlayer(stream).Play();
            btn.Invalidate();
        };

        btn.MouseLeave += (s, e) =>
        {
            if (!btn.Enabled) return;

            btn.ForeColor = Color.White;
            svgColor = Color.FromArgb(80, 255, 255, 255); // original light color
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
                var obj = FS24StartHub.App.WinForms.Resources.FS24SH_arrow;
                if (obj is byte[] bytes)
                {
                    string svgXml = System.Text.Encoding.UTF8.GetString(bytes);
                    // further work with SvgDocument

                    var svgDoc = SvgDocument.FromSvg<SvgDocument>(svgXml);

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
                else
                {
                    throw new InvalidOperationException("Resource 'FS24SH_arrow' is missing or wrong type");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("SVG load error: " + ex.Message);
            }
        };
    }

    public static void StyleSettingsButton(Button btn)
    {
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.BackColor = Color.FromArgb(20, 72, 147);
        btn.ForeColor = Color.White;
        btn.Text = string.Empty;

        btn.FlatAppearance.MouseOverBackColor = Color.White;
        btn.FlatAppearance.MouseDownBackColor = Color.LightGray;

        Color svgColor = Color.White;

        btn.MouseEnter += (s, e) =>
        {
            if (!btn.Enabled) return;
            svgColor = Color.Black;
            var stream = FS24StartHub.App.WinForms.Resources.ResourceManager.GetStream("fs24sh-hover");
            if (stream != null) new SoundPlayer(stream).Play();
            btn.Invalidate();
        };

        btn.MouseLeave += (s, e) =>
        {
            if (!btn.Enabled) return;
            svgColor = Color.White;
            btn.Invalidate();
        };

        btn.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int padding = 6;
            var iconRect = new Rectangle(padding, padding, btn.Width - padding * 2, btn.Height - padding * 2);

            try
            {
                var obj = FS24StartHub.App.WinForms.Resources.FS24SH_settings;
                if (obj is byte[] bytes)
                {
                    string svgXml = System.Text.Encoding.UTF8.GetString(bytes);
                    var svgDoc = SvgDocument.FromSvg<SvgDocument>(svgXml);

                    foreach (var path in svgDoc.Descendants().OfType<SvgPath>())
                    {
                        path.Fill = new SvgColourServer(svgColor);
                        path.Stroke = SvgPaintServer.None;
                    }

                    using var bmp = svgDoc.Draw(iconRect.Width, iconRect.Height);
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
                    case "Accent":
                        StyleAccentButton(btn);
                        break;
                    case "Settings":
                        StyleSettingsButton(btn);
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
            e.Graphics.Clear(b.Parent?.BackColor ?? Color.Transparent);

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

    public static void StyleDataGridView(DataGridView dataGridView)
    {
        if (dataGridView == null) return;

        dataGridView.BackgroundColor = Color.FromArgb(24, 36, 54);
        dataGridView.DefaultCellStyle.BackColor = Color.FromArgb(24, 36, 54);
        dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(64, 96, 128);
        dataGridView.RowsDefaultCellStyle.BackColor = Color.FromArgb(24, 36, 54);
        dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 72, 147);
        dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = dataGridView.ColumnHeadersDefaultCellStyle.BackColor;
        dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        dataGridView.GridColor = Color.FromArgb(70, 95, 115);

        var iconColumns = new[] { "colInfo", "colEdit", "colDelete" };
        int hoveredRowIndex = -1;
        int hoveredColIndex = -1;

        if (iconColumns.Any(c => dataGridView.Columns.Contains(c)))
        {
            dataGridView.MouseMove += (s, e) =>
            {
                var hit = dataGridView.HitTest(e.X, e.Y);
                bool isOverIconCol = hit.RowIndex >= 0
                    && hit.ColumnIndex >= 0
                    && iconColumns.Contains(dataGridView.Columns[hit.ColumnIndex].Name)
                    && dataGridView.Columns[hit.ColumnIndex].Name != "colInfo";

                int newRow = isOverIconCol ? hit.RowIndex : -1;
                int newCol = isOverIconCol ? hit.ColumnIndex : -1;

                if (newRow != hoveredRowIndex || newCol != hoveredColIndex)
                {
                    hoveredRowIndex = newRow;
                    hoveredColIndex = newCol;
                    foreach (var colName in iconColumns)
                        if (dataGridView.Columns.Contains(colName))
                            dataGridView.InvalidateColumn(dataGridView.Columns[colName].Index);
                }
            };

            dataGridView.MouseLeave += (s, e) =>
            {
                hoveredRowIndex = -1;
                hoveredColIndex = -1;
                foreach (var colName in iconColumns)
                    if (dataGridView.Columns.Contains(colName))
                        dataGridView.InvalidateColumn(dataGridView.Columns[colName].Index);
            };
        }

        dataGridView.CellPainting += (s, e) =>
        {
            // header
            if (e.RowIndex == -1)
            {
                e.Paint(e.ClipBounds, DataGridViewPaintParts.All);
                using var pen = new Pen(Color.FromArgb(70, 95, 115));
                e.Graphics.DrawLine(pen,
                    e.CellBounds.Left, e.CellBounds.Bottom - 1,
                    e.CellBounds.Right, e.CellBounds.Bottom - 1);
                e.Handled = true;
                return;
            }

            // icon columns
            var colName = e.ColumnIndex >= 0 ? dataGridView.Columns[e.ColumnIndex].Name : "";
            if (!iconColumns.Contains(colName)) return;

            bool isHovered = e.RowIndex == hoveredRowIndex && e.ColumnIndex == hoveredColIndex;
            bool isSelected = e.State.HasFlag(DataGridViewElementStates.Selected);

            // for colDelete - hide if current config
            if (colName == "colDelete")
            {
                var config = dataGridView.Rows[e.RowIndex].Tag as FS24StartHub.Core.Domain.Config;
                bool isCurrent = config?.IsCurrent ?? true;
                if (isCurrent)
                {
                    Color bgColorEmpty = isSelected ? Color.FromArgb(64, 96, 128) : Color.FromArgb(24, 36, 54);
                    using var emptyBrush = new SolidBrush(bgColorEmpty);
                    e.Graphics.FillRectangle(emptyBrush, e.CellBounds);
                    using var emptyGridPen = new Pen(Color.FromArgb(70, 95, 115));
                    e.Graphics.DrawLine(emptyGridPen,
                        e.CellBounds.Left, e.CellBounds.Bottom - 1,
                        e.CellBounds.Right, e.CellBounds.Bottom - 1);
                    e.Handled = true;
                    return;
                }
            }

            // background
            Color bgColor = isHovered
                ? Color.White
                : isSelected
                    ? Color.FromArgb(64, 96, 128)
                    : Color.FromArgb(24, 36, 54);

            using var bgBrush = new SolidBrush(bgColor);
            e.Graphics.FillRectangle(bgBrush, e.CellBounds);

            // horizontal line at the bottom
            using var gridPen = new Pen(Color.FromArgb(70, 95, 115));
            e.Graphics.DrawLine(gridPen,
                e.CellBounds.Left, e.CellBounds.Bottom - 1,
                e.CellBounds.Right, e.CellBounds.Bottom - 1);

            // icon
            var icon = colName switch
            {
                "colDelete" => "\uE711",
                "colInfo" => "\uE946",
                _ => "\uE70F"  // colEdit
            };
            using var font = new Font("Segoe MDL2 Assets", 9f, FontStyle.Bold);
            Color iconColor = isHovered ? Color.Black
                : isSelected ? Color.White
                : Color.FromArgb(160, 180, 200);
            using var brush = new SolidBrush(iconColor);
            var iconSize = e.Graphics.MeasureString(icon, font);
            float x = e.CellBounds.Left + (e.CellBounds.Width - iconSize.Width) / 2f;
            float y = e.CellBounds.Top + (e.CellBounds.Height - iconSize.Height) / 2f;
            e.Graphics.DrawString(icon, font, brush, x, y);

            e.Handled = true;
        };

        var toolTip = new ToolTip();

        dataGridView.CellMouseEnter += (s, e) =>
        {
            if (e.RowIndex < 0) return;

            var colName = dataGridView.Columns[e.ColumnIndex].Name;

            // for colDelete - don't show tooltip if current config
            if (colName == "colDelete")
            {
                var config = dataGridView.Rows[e.RowIndex].Tag as FS24StartHub.Core.Domain.Config;
                if (config?.IsCurrent ?? true) return;
            }

            string? tip = colName switch
            {
                "colEdit" => "View/Edit",
                "colDelete" => "Delete",
                "colInfo" => BuildInfoTooltip(dataGridView, e.RowIndex),
                _ => null
            };

            if (tip == null) return;

            var cellRect = dataGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            toolTip.Show(tip, dataGridView, cellRect.Left, cellRect.Bottom + 2);
        };

        dataGridView.CellMouseLeave += (s, e) =>
        {
            toolTip.Hide(dataGridView);
        };
    }

    private static string BuildInfoTooltip(DataGridView dgv, int rowIndex)
    {
        var config = dgv.Rows[rowIndex].Tag as FS24StartHub.Core.Domain.Config;
        if (config == null) return "";

        var lastUsed = config.LastUsed == default ? "—" : config.LastUsed.ToString("dd.MM.yyyy HH:mm");
        var tip = $"Name:          {config.Name ?? "—"}\nLast Used:    {lastUsed}";

        if (!string.IsNullOrWhiteSpace(config.Description))
            tip += "\nDescription: " + WrapText(config.Description, 50);

        return tip;
    }

    private static string WrapText(string text, int maxWidth)
    {
        var words = text.Split(' ');
        var lines = new List<string>();
        var current = "";
        foreach (var word in words)
        {
            var candidate = current.Length == 0 ? word : current + " " + word;
            if (candidate.Length > maxWidth && current.Length > 0)
            {
                lines.Add(current);
                current = word;
            }
            else
            {
                current = candidate;
            }
        }
        if (current.Length > 0) lines.Add(current);
        return string.Join("\n                      ", lines); // indent so continuation aligns under Description text
    }

    public static void ApplyStyleToAllComboBoxes(Control parent)
    {
        foreach (Control control in parent.Controls)
        {
            if (control is ComboBox combo)
            {
                // Just set colors and style
                combo.BackColor = Color.FromArgb(20, 72, 147);
                combo.ForeColor = Color.White;
                combo.FlatStyle = FlatStyle.Flat;
            }

            if (control.HasChildren)
                ApplyStyleToAllComboBoxes(control);
        }
    }

    public static void ApplyStyleToAllCheckBoxes(Control parent)
    {
        foreach (Control ctrl in parent.Controls)
        {
            if (ctrl is CheckBox cb)
                StyleCheckBox(cb);

            if (ctrl.HasChildren)
                ApplyStyleToAllCheckBoxes(ctrl);
        }
    }
}