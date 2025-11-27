using System.ComponentModel;
using System.Windows.Forms.VisualStyles;

namespace FS24StartHub.App.WinForms.Controls
{
    public class CustomCheckedListBox : CheckedListBox
    {
        [Category("Appearance")]
        [Description("Background color for list item rows. If Empty, the control uses its BackColor.")]
        [DefaultValue(typeof(Color), "Empty")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ItemBackColor { get; set; } = Color.Empty;

        [Category("Appearance")]
        [Description("Background color for selected item rows. If Empty, the control uses system Highlight.")]
        [DefaultValue(typeof(Color), "Empty")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ItemSelectedColor { get; set; } = Color.Empty;

        [Category("Appearance")]
        [Description("Optional foreground color for readonly items. If Empty, uses default ForeColor.")]
        [DefaultValue(typeof(Color), "Empty")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ReadonlyForeColor { get; set; } = Color.Empty;

        [Category("Appearance")]
        [Description("Foreground color for selected normal items. If Empty, uses system HighlightText.")]
        [DefaultValue(typeof(Color), "Empty")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ItemSelectedForeColor { get; set; } = Color.Empty;

        [Category("Appearance")]
        [Description("Foreground color for selected readonly items. If Empty, uses ReadonlyForeColor.")]
        [DefaultValue(typeof(Color), "Empty")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ReadonlySelectedForeColor { get; set; } = Color.Empty;

        public enum CheckBoxDrawMode
        {
            System,
            Custom
        }

        [Category("Appearance")]
        [Description("Determines whether checkboxes are drawn using the system style or custom style.")]
        [DefaultValue(CheckBoxDrawMode.System)]
        public CheckBoxDrawMode CheckBoxMode { get; set; } = CheckBoxDrawMode.System;

        [Category("Appearance")]
        [Description("Background color for custom checkbox. Used only if CheckBoxMode=Custom.")]
        [DefaultValue(typeof(Color), "0, 120, 215")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color CustomCheckBoxBackColor { get; set; } = Color.FromArgb(0, 120, 215);

        [Category("Appearance")]
        [Description("Check mark color for custom checkbox. Used only if CheckBoxMode=Custom.")]
        [DefaultValue(typeof(Color), "White")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color CustomCheckMarkColor { get; set; } = Color.White;

        [Category("Appearance")]
        [Description("If false, custom checkbox is drawn without border. Used only if CheckBoxMode=Custom.")]
        [DefaultValue(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool CustomCheckBoxBorder { get; set; } = true;

        private Color _toolTipBackColor = Color.Empty;

        [Category("Appearance")]
        [Description("Custom background color for tooltip. If Empty, system color is used.")]
        [DefaultValue(typeof(Color), "Empty")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ToolTipBackColor
        {
            get => _toolTipBackColor;
            set
            {
                _toolTipBackColor = value;
                UpdateToolTipMode();
            }
        }

        [Category("Appearance")]
        [Description("Custom text color for tooltip. If Empty, system tooltip is used.")]
        [DefaultValue(typeof(Color), "Empty")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ToolTipForeColor { get; set; } = Color.Empty;


        private readonly ToolTip _toolTip = new ToolTip();
        private bool isInitializing = false;

        public CustomCheckedListBox()
        {
            _toolTip = new ToolTip
            {
                InitialDelay = 400,
                ReshowDelay = 200,
                AutoPopDelay = 8000
            };
            UpdateToolTipMode();

            _toolTip.Draw += ToolTip_Draw;

            DrawMode = DrawMode.OwnerDrawVariable;
            MouseMove += CustomCheckedListBox_MouseMove;
        }

        public void LoadItems(IEnumerable<CustomCheckedListBoxItem> items)
        {
            isInitializing = true;
            Items.Clear();
            foreach (var item in items)
                Items.Add(item, item.Enabled);
            isInitializing = false;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= Items.Count) return;
            var item = Items[e.Index] as CustomCheckedListBoxItem;
            if (item == null) return;

            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            var g = e.Graphics;
            var font = e.Font ?? SystemFonts.DefaultFont;

            // background
            var listBounds = e.Bounds;
            var backColor = isSelected
                ? (ItemSelectedColor.IsEmpty ? SystemColors.Highlight : ItemSelectedColor)
                : (ItemBackColor.IsEmpty ? BackColor : ItemBackColor);

            using (var backBrush = new SolidBrush(backColor))
                g.FillRectangle(backBrush, listBounds);

            // text color
            Color foreColor = e.ForeColor;

            if (item.Readonly)
            {
                if (isSelected)
                {
                    foreColor = !ReadonlySelectedForeColor.IsEmpty
                        ? ReadonlySelectedForeColor
                        : (!ReadonlyForeColor.IsEmpty ? ReadonlyForeColor : SystemColors.HighlightText);
                }
                else if (!ReadonlyForeColor.IsEmpty)
                {
                    foreColor = ReadonlyForeColor;
                }
            }
            else if (isSelected)
            {
                foreColor = !ItemSelectedForeColor.IsEmpty ? ItemSelectedForeColor : SystemColors.HighlightText;
            }

            // checkbox rectangle
            var cbSize = CheckBoxRenderer.GetGlyphSize(g, CheckBoxState.UncheckedNormal);
            var checkBoxRect = new Rectangle(
                listBounds.X + 4,
                listBounds.Y + (listBounds.Height - cbSize.Height) / 2,
                cbSize.Width,
                cbSize.Height
            );

            bool isChecked = GetItemChecked(e.Index);
            bool isDisabled = item.Readonly;

            if (CheckBoxMode == CheckBoxDrawMode.System)
            {
                var state = isChecked
                    ? (isDisabled ? CheckBoxState.CheckedDisabled : CheckBoxState.CheckedNormal)
                    : (isDisabled ? CheckBoxState.UncheckedDisabled : CheckBoxState.UncheckedNormal);

                CheckBoxRenderer.DrawCheckBox(g, checkBoxRect.Location, state);
            }
            else // Custom
            {
                Color back = item.Readonly ? Color.Gray : CustomCheckBoxBackColor;

                Color mark = item.Readonly ? (!ReadonlyForeColor.IsEmpty ? ReadonlyForeColor : SystemColors.HighlightText) : CustomCheckMarkColor;

                // background
                using (var backBrush = new SolidBrush(back))
                    g.FillRectangle(backBrush, checkBoxRect);

                // border
                if (CustomCheckBoxBorder)
                {
                    using var borderPen = new Pen(Color.Black, 1);
                    g.DrawRectangle(borderPen, checkBoxRect);
                }

                // check mark
                if (isChecked)
                {
                    int w = checkBoxRect.Width;
                    int h = checkBoxRect.Height;

                    using var pen = new Pen(mark, Math.Max(2, w / 10));
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    Point p1 = new Point(checkBoxRect.Left + (int)(0.23 * w), checkBoxRect.Top + (int)(0.45 * h));
                    Point p2 = new Point(checkBoxRect.Left + (int)(0.41 * w), checkBoxRect.Top + (int)(0.64 * h));
                    Point p3 = new Point(checkBoxRect.Left + (int)(0.82 * w), checkBoxRect.Top + (int)(0.27 * h));

                    g.DrawLines(pen, new[] { p1, p2, p3 });
                }
            }

            // text rectangle
            var textRect = new Rectangle(
                checkBoxRect.Right + 4,
                listBounds.Y,
                listBounds.Width - cbSize.Width - 8,
                listBounds.Height
            );

            TextRenderer.DrawText(
                g, item.Text, font,
                textRect, foreColor,
                TextFormatFlags.NoPadding | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter
            );

            ControlPaint.DrawFocusRectangle(g, listBounds);
        }

        private void CustomCheckedListBox_MouseMove(object? sender, MouseEventArgs e)
        {
            int index = IndexFromPoint(e.Location);

            if (index >= 0 && Items[index] is CustomCheckedListBoxItem item && !string.IsNullOrEmpty(item.Tooltip))
            {
                string currentTip = _toolTip.GetToolTip(this) ?? string.Empty;

                if (!string.Equals(currentTip, item.Tooltip, StringComparison.Ordinal))
                {
                    _toolTip.SetToolTip(this, item.Tooltip);
                }
            }
            else
            {
                string currentTip = _toolTip.GetToolTip(this) ?? string.Empty;
                if (!string.IsNullOrEmpty(currentTip))
                {
                    _toolTip.SetToolTip(this, string.Empty);
                }
            }
        }

        private void ToolTip_Draw(object? sender, DrawToolTipEventArgs e)
        {
            Color back = ToolTipBackColor.IsEmpty ? SystemColors.Info : ToolTipBackColor;
            Color fore = ToolTipForeColor.IsEmpty ? SystemColors.InfoText : ToolTipForeColor;
            Font font = SystemFonts.DefaultFont;

            using var b = new SolidBrush(back);
            e.Graphics.FillRectangle(b, e.Bounds);

            TextRenderer.DrawText(
                e.Graphics,
                e.ToolTipText,
                font,
                e.Bounds,
                fore,
                TextFormatFlags.SingleLine | TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPrefix
            );
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

        private void ToolTip_Popup(object? sender, PopupEventArgs e)
        {
            if (e.AssociatedControl == null)
                return;

            using var g = e.AssociatedControl.CreateGraphics();
            Font font = SystemFonts.DefaultFont;

            Size textSize = TextRenderer.MeasureText(
                _toolTip.GetToolTip(e.AssociatedControl),
                font,
                new Size(int.MaxValue, int.MaxValue),
                TextFormatFlags.SingleLine | TextFormatFlags.NoPrefix
            );

            e.ToolTipSize = new Size(textSize.Width, textSize.Height + 4);
        }

        private void UpdateToolTipMode()
        {
            bool useCustom = !ToolTipBackColor.IsEmpty || !ToolTipForeColor.IsEmpty;

            _toolTip.OwnerDraw = useCustom;

            if (useCustom)
            {
                _toolTip.Draw -= ToolTip_Draw;
                _toolTip.Draw += ToolTip_Draw;

                _toolTip.Popup -= ToolTip_Popup;
                _toolTip.Popup += ToolTip_Popup;
            }
            else
            {
                _toolTip.Draw -= ToolTip_Draw;
                _toolTip.Popup -= ToolTip_Popup;
            }
        }

        internal Color ResolveItemBackColor() => ItemBackColor.IsEmpty ? this.BackColor : ItemBackColor;
        internal Color ResolveItemSelectedColor() => ItemSelectedColor.IsEmpty ? SystemColors.Highlight : ItemSelectedColor;
    }
}