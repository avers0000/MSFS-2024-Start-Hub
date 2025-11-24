using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FS24StartHub.App.WinForms.Controls
{
    public partial class SideButtonsNumeric : UserControl, ISupportInitialize
    {
        private Color _buttonBackColor = Color.Empty;
        private Color _buttonForeColor = Color.Empty;
        private Color _textBoxBackColor = Color.Empty;
        private Color _textBoxForeColor = Color.Empty;

        private decimal _value = 0m;
        private decimal _minimum = 0m;
        private decimal _maximum = 100m;
        private decimal _increment = 1m;

        private bool _initializing;

        public SideButtonsNumeric()
        {
            InitializeComponent();

            txtValue.Text = _value.ToString();

            Resize += (s, e) => AdjustLayout();
            txtValue.FontChanged += (s, e) => AdjustLayout();

            txtValue.KeyPress += TxtValue_KeyPress;
            txtValue.TextChanged += TxtValue_TextChanged;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            btnLeft.Click += (s, ev) => Value -= Increment;
            btnRight.Click += (s, ev) => Value += Increment;

            btnLeft.Paint += DrawLeftArrow;
            btnRight.Paint += DrawRightArrow;

            AdjustLayout();
        }

        private void AdjustLayout()
        {
            int buttonSize = Height;

            btnLeft.Size = new Size(buttonSize, buttonSize);
            btnLeft.Location = new Point(0, 0);

            btnRight.Size = new Size(buttonSize, buttonSize);
            btnRight.Location = new Point(Width - buttonSize, 0);

            txtValue.Left = btnLeft.Right;
            txtValue.Width = Width - btnLeft.Width - btnRight.Width;
            txtValue.Top = (Height - txtValue.Height) / 2;

            if (!_textBoxBackColor.IsEmpty) txtValue.BackColor = _textBoxBackColor;
            if (!_textBoxForeColor.IsEmpty) txtValue.ForeColor = _textBoxForeColor;

            if (!_buttonBackColor.IsEmpty)
            {
                btnLeft.BackColor = _buttonBackColor;
                btnRight.BackColor = _buttonBackColor;
            }

            if (!_buttonForeColor.IsEmpty)
            {
                btnLeft.ForeColor = _buttonForeColor;
                btnRight.ForeColor = _buttonForeColor;
            }
        }

        [Category("Behavior")]
        [DefaultValue(typeof(decimal), "0")]
        public decimal Value
        {
            get => _value;
            set
            {
                if (value < Minimum) value = Minimum;
                if (value > Maximum) value = Maximum;
                if (_value == value) return;

                _value = value;
                if (!_initializing)
                    txtValue.Text = _value.ToString();
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        [Category("Behavior")]
        [DefaultValue(typeof(decimal), "0")]
        public decimal Minimum
        {
            get => _minimum;
            set
            {
                _minimum = value;
                if (_maximum < _minimum) _maximum = _minimum;
                if (_value < _minimum) Value = _minimum;
            }
        }

        [Category("Behavior")]
        [DefaultValue(typeof(decimal), "100")]
        public decimal Maximum
        {
            get => _maximum;
            set
            {
                _maximum = value;
                if (_minimum > _maximum) _minimum = _maximum;
                if (_value > _maximum) Value = _maximum;
            }
        }

        [Category("Behavior")]
        [DefaultValue(typeof(decimal), "1")]
        public decimal Increment
        {
            get => _increment;
            set => _increment = value <= 0 ? 1m : value;
        }

        public event EventHandler ValueChanged;

        [Category("Appearance")]
        [DefaultValue(typeof(Color), "Empty")]
        public Color ButtonBackColor
        {
            get => _buttonBackColor;
            set { _buttonBackColor = value; AdjustLayout(); }
        }

        [Category("Appearance")]
        [DefaultValue(typeof(Color), "Empty")]
        public Color ButtonForeColor
        {
            get => _buttonForeColor;
            set { _buttonForeColor = value; AdjustLayout(); }
        }

        [Category("Appearance")]
        [DefaultValue(typeof(Color), "Empty")]
        public Color TextBoxBackColor
        {
            get => _textBoxBackColor;
            set { _textBoxBackColor = value; AdjustLayout(); }
        }

        [Category("Appearance")]
        [DefaultValue(typeof(Color), "Empty")]
        public Color TextBoxForeColor
        {
            get => _textBoxForeColor;
            set { _textBoxForeColor = value; AdjustLayout(); }
        }

        private void TxtValue_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (char.IsDigit(e.KeyChar)) return;

            bool allowDecimal = (Minimum % 1 != 0) || (Maximum % 1 != 0) || (Increment % 1 != 0);
            if (allowDecimal && (e.KeyChar == '.' || e.KeyChar == ','))
            {
                if (txtValue.Text.Contains('.') || txtValue.Text.Contains(','))
                    e.Handled = true;
                return;
            }

            if (Minimum < 0 && e.KeyChar == '-' && txtValue.SelectionStart == 0) return;

            e.Handled = true;
        }

        private void TxtValue_TextChanged(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtValue.Text)) return;

            if (!decimal.TryParse(txtValue.Text, out var parsed))
            {
                txtValue.Text = Value.ToString();
                txtValue.SelectionStart = txtValue.Text.Length;
                return;
            }

            if (parsed < Minimum) parsed = Minimum;
            if (parsed > Maximum) parsed = Maximum;

            Value = parsed;
        }

        public void BeginInit() => _initializing = true;

        public void EndInit()
        {
            _initializing = false;
            if (Value < Minimum) Value = Minimum;
            if (Value > Maximum) Value = Maximum;
            txtValue.Text = Value.ToString();
            AdjustLayout();
        }

        public void UpButton() => Value += Increment;
        public void DownButton() => Value -= Increment;

        private void DrawLeftArrow(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var s = btnLeft.Width;

            int arrowSize = s * 2 / 5 + 2;
            int cx = s / 2;
            int cy = s / 2;

            var arrow = new Point[]
            {
                new Point(cx + arrowSize / 2, cy - arrowSize / 2),
                new Point(cx - arrowSize / 2, cy),
                new Point(cx + arrowSize / 2, cy + arrowSize / 2)
            };

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using var brush = new SolidBrush(btnLeft.ForeColor);
            g.FillPolygon(brush, arrow);
        }
        private void DrawRightArrow(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var s = btnRight.Width;

            int arrowSize = s * 2 / 5 + 2;
            int cx = s / 2;
            int cy = s / 2;

            var arrow = new Point[]
            {
                new Point(cx - arrowSize / 2, cy - arrowSize / 2),
                new Point(cx + arrowSize / 2, cy),
                new Point(cx - arrowSize / 2, cy + arrowSize / 2)
            };

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using var brush = new SolidBrush(btnRight.ForeColor);
            g.FillPolygon(brush, arrow);
        }
    }
}