namespace FS24StartHub.App.WinForms.Controls
{
    partial class SideButtonsNumeric
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnLeft = new Button();
            btnRight = new Button();
            txtValue = new TextBox();
            SuspendLayout();
            // 
            // btnLeft
            // 
            btnLeft.FlatAppearance.BorderSize = 0;
            btnLeft.FlatStyle = FlatStyle.Flat;
            btnLeft.Font = new Font("Segoe UI Semibold", 10F);
            btnLeft.ForeColor = Color.White;
            btnLeft.Location = new Point(0, 0);
            btnLeft.Name = "btnLeft";
            btnLeft.Size = new Size(23, 25);
            btnLeft.TabIndex = 0;
            btnLeft.Tag = "Accent";
            btnLeft.UseVisualStyleBackColor = false;
            // 
            // btnRight
            // 
            btnRight.FlatAppearance.BorderSize = 0;
            btnRight.FlatStyle = FlatStyle.Flat;
            btnRight.Font = new Font("Segoe UI Semibold", 10F);
            btnRight.ForeColor = Color.White;
            btnRight.Location = new Point(127, 0);
            btnRight.Name = "btnRight";
            btnRight.Size = new Size(23, 25);
            btnRight.TabIndex = 1;
            btnRight.Tag = "Accent";
            btnRight.UseVisualStyleBackColor = false;
            // 
            // txtValue
            // 
            txtValue.BackColor = Color.Black;
            txtValue.BorderStyle = BorderStyle.None;
            txtValue.Font = new Font("Segoe UI", 10F);
            txtValue.ForeColor = Color.White;
            txtValue.Location = new Point(23, 0);
            txtValue.Name = "txtValue";
            txtValue.Size = new Size(104, 18);
            txtValue.TabIndex = 2;
            txtValue.TextAlign = HorizontalAlignment.Center;
            // 
            // SideButtonsNumeric
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            Controls.Add(txtValue);
            Controls.Add(btnRight);
            Controls.Add(btnLeft);
            ForeColor = Color.White;
            Name = "SideButtonsNumeric";
            Size = new Size(150, 25);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnLeft;
        private Button btnRight;
        private TextBox txtValue;
    }
}
