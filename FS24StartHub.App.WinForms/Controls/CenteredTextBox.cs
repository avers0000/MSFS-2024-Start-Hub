using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FS24StartHub.App.WinForms.Controls
{
    public partial class CenteredTextBox : UserControl
    {
        public CenteredTextBox()
        {
            InitializeComponent();

            CenterInnerTextBox();
            this.Resize += (s, e) => CenterInnerTextBox();

            // Forward inner events to outer control
            txtText.TextChanged += (s, e) => base.OnTextChanged(e);
            txtText.KeyDown += (s, e) => base.OnKeyDown(e);
            txtText.KeyPress += (s, e) => base.OnKeyPress(e);
            txtText.KeyUp += (s, e) => base.OnKeyUp(e);
        }

        private void CenterInnerTextBox()
        {
            txtText.Left = this.Padding.Left;
            txtText.Width = this.Width - this.Padding.Left - this.Padding.Right;
            txtText.Top = (this.Height - txtText.Height) / 2;
        }

        [Browsable(true)]
        [Category("Appearance")]
        [Description("The text associated with the control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string? Text
        {
            get => txtText.Text;
            set
            {
                var text = value ?? string.Empty;
                base.Text = text;
                txtText.Text = text;
            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        [Description("The font used to display text in the control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override Font? Font
        {
            get => base.Font;
            set
            {
                var font = value ?? SystemFonts.DefaultFont;
                base.Font = font;
                txtText.Font = font;
                CenterInnerTextBox();
            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        [Description("The foreground color of the control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                base.ForeColor = value;
                txtText.ForeColor = base.ForeColor;
            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        [Description("The background color of the control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                txtText.BackColor = base.BackColor;
            }
        }

        [Browsable(true)]
        [Category("Behavior")]
        [Description("Specifies how text is aligned in the control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public HorizontalAlignment TextAlign
        {
            get => txtText.TextAlign;
            set => txtText.TextAlign = value;
        }

        [Browsable(true)]
        [Category("Behavior")]
        [Description("Indicates whether the text in the control can be changed or not.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ReadOnly
        {
            get => txtText.ReadOnly;
            set => txtText.ReadOnly = value;
        }

        [Browsable(true)]
        [Category("Behavior")]
        [Description("The character used to mask characters of a password in the control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public char PasswordChar
        {
            get => txtText.PasswordChar;
            set => txtText.PasswordChar = value;
        }

        [Browsable(true)]
        [Category("Behavior")]
        [Description("Specifies the maximum number of characters allowed in the control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int MaxLength
        {
            get => txtText.MaxLength;
            set => txtText.MaxLength = value;
        }

        [Browsable(false)]
        public TextBox InnerTextBox => txtText;
    }
}