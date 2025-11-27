using System.ComponentModel;

namespace FS24StartHub.App.WinForms.Controls
{
    public class HorizontalLineTableLayoutPanel : TableLayoutPanel
    {
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Color of horizontal separator lines between rows.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color LineColor { get; set; } = Color.Gray;

        [Browsable(true)]
        [Category("Appearance")]
        [Description("Thickness of horizontal separator lines between rows.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int LineThickness { get; set; } = 1;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (ColumnCount < 3) return; // at least 3 columns are required

            var rowHeights = GetRowHeights();
            var colWidths = GetColumnWidths();

            // Calculate X-coordinates: from the second to the second-to-last column
            int xStart = 0;
            for (int i = 0; i < 1; i++) xStart += colWidths[i];

            int xEnd = xStart;
            for (int i = 1; i < ColumnCount - 1; i++) xEnd += colWidths[i];

            using var pen = new Pen(LineColor, LineThickness);

            int y = 0;
            for (int row = 0; row < rowHeights.Length - 1; row++)
            {
                y += rowHeights[row];
                e.Graphics.DrawLine(pen, xStart, y, xEnd, y);
            }
        }
    }
}