namespace FS24StartHub.App.WinForms
{
    partial class StartForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnAbort = new Button();
            lblStatus = new Label();
            progressBar = new ProgressBar();
            SuspendLayout();
            // 
            // btnAbort
            // 
            btnAbort.BackColor = Color.Black;
            btnAbort.FlatAppearance.BorderSize = 0;
            btnAbort.FlatStyle = FlatStyle.Flat;
            btnAbort.Font = new Font("Segoe UI Semibold", 12F);
            btnAbort.Location = new Point(344, 112);
            btnAbort.Name = "btnAbort";
            btnAbort.Size = new Size(96, 30);
            btnAbort.TabIndex = 0;
            btnAbort.Tag = "Accent";
            btnAbort.Text = "Abort";
            btnAbort.UseVisualStyleBackColor = false;
            btnAbort.Click += btnAbort_Click;
            // 
            // lblStatus
            // 
            lblStatus.Font = new Font("Segoe UI", 10F);
            lblStatus.Location = new Point(20, 20);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(420, 72);
            lblStatus.TabIndex = 1;
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(20, 129);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(304, 10);
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.TabIndex = 2;
            // 
            // StartForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            CancelButton = btnAbort;
            ClientSize = new Size(460, 162);
            ControlBox = false;
            Controls.Add(progressBar);
            Controls.Add(lblStatus);
            Controls.Add(btnAbort);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "StartForm";
            Opacity = 0.92D;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Load += StartForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnAbort;
        private Label lblStatus;
        private ProgressBar progressBar;
    }
}