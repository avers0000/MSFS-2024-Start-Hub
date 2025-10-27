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
            btnAbort.Location = new Point(313, 85);
            btnAbort.Name = "btnAbort";
            btnAbort.Size = new Size(75, 23);
            btnAbort.TabIndex = 0;
            btnAbort.Text = "Abort";
            btnAbort.UseVisualStyleBackColor = true;
            btnAbort.Click += btnAbort_Click;
            // 
            // lblStatus
            // 
            lblStatus.Location = new Point(12, 9);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(376, 64);
            lblStatus.TabIndex = 1;
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(12, 85);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(295, 23);
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.TabIndex = 2;
            // 
            // StartForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(392, 112);
            ControlBox = false;
            Controls.Add(progressBar);
            Controls.Add(lblStatus);
            Controls.Add(btnAbort);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "StartForm";
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