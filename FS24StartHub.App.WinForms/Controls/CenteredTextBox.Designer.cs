namespace FS24StartHub.App.WinForms.Controls
{
    partial class CenteredTextBox
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
            txtText = new TextBox();
            SuspendLayout();
            // 
            // txtText
            // 
            txtText.BackColor = Color.Black;
            txtText.BorderStyle = BorderStyle.None;
            txtText.Font = new Font("Segoe UI", 10F);
            txtText.ForeColor = Color.White;
            txtText.Location = new Point(26, 0);
            txtText.Name = "txtText";
            txtText.Size = new Size(100, 18);
            txtText.TabIndex = 0;
            // 
            // CenteredTextBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            Controls.Add(txtText);
            ForeColor = Color.White;
            Name = "CenteredTextBox";
            Padding = new Padding(5, 0, 5, 0);
            Size = new Size(150, 25);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtText;
    }
}
