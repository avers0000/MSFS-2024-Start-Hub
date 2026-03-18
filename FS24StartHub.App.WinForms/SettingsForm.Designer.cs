using FS24StartHub.App.WinForms.Controls;

namespace FS24StartHub.App.WinForms
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblDetectedSettings = new Label();
            pnDetectedInfo = new Panel();
            lblInstallationType = new Label();
            txtInstallationType = new TextBox();
            lblPackagesPath = new Label();
            txtPackagesPath = new TextBox();
            btnRedetect = new Button();
            chbUseCustom = new CheckBox();
            tlpCustom = new HorizontalLineTableLayoutPanel();
            lblCustomSimPath = new Label();
            txtCustomSimPath = new CenteredTextBox();
            btnBrowseSimPath = new Button();
            lblCustomSimExePath = new Label();
            txtCustomSimExePath = new CenteredTextBox();
            btnBrowseSimExePath = new Button();
            btnOK = new Button();
            btnCancel = new Button();
            pnDetectedInfo.SuspendLayout();
            tlpCustom.SuspendLayout();
            SuspendLayout();
            // 
            // lblDetectedSettings
            // 
            lblDetectedSettings.AutoSize = true;
            lblDetectedSettings.Font = new Font("Segoe UI Semibold", 10F);
            lblDetectedSettings.ForeColor = Color.White;
            lblDetectedSettings.Location = new Point(20, 18);
            lblDetectedSettings.Name = "lblDetectedSettings";
            lblDetectedSettings.Size = new Size(185, 19);
            lblDetectedSettings.TabIndex = 0;
            lblDetectedSettings.Text = "Detected Simulator Settings";
            // 
            // pnDetectedInfo
            // 
            pnDetectedInfo.BackColor = Color.Black;
            pnDetectedInfo.Controls.Add(lblInstallationType);
            pnDetectedInfo.Controls.Add(txtInstallationType);
            pnDetectedInfo.Controls.Add(lblPackagesPath);
            pnDetectedInfo.Controls.Add(txtPackagesPath);
            pnDetectedInfo.Location = new Point(20, 44);
            pnDetectedInfo.Name = "pnDetectedInfo";
            pnDetectedInfo.Padding = new Padding(8, 6, 8, 6);
            pnDetectedInfo.Size = new Size(592, 54);
            pnDetectedInfo.TabIndex = 0;
            // 
            // lblInstallationType
            // 
            lblInstallationType.AutoSize = true;
            lblInstallationType.Font = new Font("Segoe UI", 10F);
            lblInstallationType.ForeColor = Color.Lime;
            lblInstallationType.Location = new Point(11, 9);
            lblInstallationType.Name = "lblInstallationType";
            lblInstallationType.Size = new Size(108, 19);
            lblInstallationType.TabIndex = 0;
            lblInstallationType.Text = "Installation Type";
            // 
            // txtInstallationType
            // 
            txtInstallationType.BackColor = Color.Black;
            txtInstallationType.BorderStyle = BorderStyle.None;
            txtInstallationType.Font = new Font("Segoe UI", 10F);
            txtInstallationType.ForeColor = Color.White;
            txtInstallationType.Location = new Point(124, 9);
            txtInstallationType.Name = "txtInstallationType";
            txtInstallationType.ReadOnly = true;
            txtInstallationType.Size = new Size(454, 18);
            txtInstallationType.TabIndex = 0;
            txtInstallationType.TabStop = false;
            // 
            // lblPackagesPath
            // 
            lblPackagesPath.AutoSize = true;
            lblPackagesPath.Font = new Font("Segoe UI", 10F);
            lblPackagesPath.ForeColor = Color.Lime;
            lblPackagesPath.Location = new Point(11, 27);
            lblPackagesPath.Name = "lblPackagesPath";
            lblPackagesPath.Size = new Size(97, 19);
            lblPackagesPath.TabIndex = 1;
            lblPackagesPath.Text = "Packages Path";
            // 
            // txtPackagesPath
            // 
            txtPackagesPath.BackColor = Color.Black;
            txtPackagesPath.BorderStyle = BorderStyle.None;
            txtPackagesPath.Font = new Font("Segoe UI", 10F);
            txtPackagesPath.ForeColor = Color.White;
            txtPackagesPath.Location = new Point(124, 27);
            txtPackagesPath.Name = "txtPackagesPath";
            txtPackagesPath.ReadOnly = true;
            txtPackagesPath.Size = new Size(454, 18);
            txtPackagesPath.TabIndex = 1;
            txtPackagesPath.TabStop = false;
            // 
            // btnRedetect
            // 
            btnRedetect.BackColor = Color.Black;
            btnRedetect.FlatAppearance.BorderSize = 0;
            btnRedetect.FlatStyle = FlatStyle.Flat;
            btnRedetect.Font = new Font("Segoe UI Semibold", 10F);
            btnRedetect.ForeColor = Color.White;
            btnRedetect.Location = new Point(516, 13);
            btnRedetect.Name = "btnRedetect";
            btnRedetect.Size = new Size(96, 26);
            btnRedetect.TabIndex = 1;
            btnRedetect.Tag = "Accent";
            btnRedetect.Text = "Redetect";
            btnRedetect.UseVisualStyleBackColor = false;
            btnRedetect.Click += btnRedetect_Click;
            // 
            // chbUseCustom
            // 
            chbUseCustom.AutoSize = true;
            chbUseCustom.BackColor = Color.Transparent;
            chbUseCustom.FlatStyle = FlatStyle.Flat;
            chbUseCustom.Font = new Font("Segoe UI Semibold", 10F);
            chbUseCustom.ForeColor = Color.White;
            chbUseCustom.Location = new Point(20, 120);
            chbUseCustom.Name = "chbUseCustom";
            chbUseCustom.Size = new Size(155, 23);
            chbUseCustom.TabIndex = 2;
            chbUseCustom.Text = "Use Custom Settings";
            chbUseCustom.UseVisualStyleBackColor = false;
            chbUseCustom.CheckedChanged += chbUseCustom_CheckedChanged;
            // 
            // tlpCustom
            // 
            tlpCustom.ColumnCount = 5;
            tlpCustom.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 5F));
            tlpCustom.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            tlpCustom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpCustom.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95F));
            tlpCustom.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 5F));
            tlpCustom.Controls.Add(lblCustomSimPath, 1, 1);
            tlpCustom.Controls.Add(txtCustomSimPath, 2, 1);
            tlpCustom.Controls.Add(btnBrowseSimPath, 3, 1);
            tlpCustom.Controls.Add(lblCustomSimExePath, 1, 2);
            tlpCustom.Controls.Add(txtCustomSimExePath, 2, 2);
            tlpCustom.Controls.Add(btnBrowseSimExePath, 3, 2);
            tlpCustom.LineColor = Color.DimGray;
            tlpCustom.LineThickness = 1;
            tlpCustom.Location = new Point(20, 154);
            tlpCustom.Name = "tlpCustom";
            tlpCustom.RowCount = 4;
            tlpCustom.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            tlpCustom.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tlpCustom.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tlpCustom.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            tlpCustom.Size = new Size(592, 78);
            tlpCustom.TabIndex = 3;
            tlpCustom.Visible = false;
            // 
            // lblCustomSimPath
            // 
            lblCustomSimPath.Anchor = AnchorStyles.Left;
            lblCustomSimPath.AutoSize = true;
            lblCustomSimPath.Location = new Point(5, 12);
            lblCustomSimPath.Margin = new Padding(0, 0, 3, 0);
            lblCustomSimPath.Name = "lblCustomSimPath";
            lblCustomSimPath.Size = new Size(98, 19);
            lblCustomSimPath.TabIndex = 0;
            lblCustomSimPath.Text = "Packages Path";
            // 
            // txtCustomSimPath
            // 
            txtCustomSimPath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtCustomSimPath.BackColor = Color.Black;
            txtCustomSimPath.Font = new Font("Segoe UI", 10F);
            txtCustomSimPath.ForeColor = Color.White;
            txtCustomSimPath.Location = new Point(148, 10);
            txtCustomSimPath.MaxLength = 32767;
            txtCustomSimPath.Name = "txtCustomSimPath";
            txtCustomSimPath.Padding = new Padding(5, 0, 5, 0);
            txtCustomSimPath.PasswordChar = '\0';
            txtCustomSimPath.ReadOnly = false;
            txtCustomSimPath.Size = new Size(341, 24);
            txtCustomSimPath.TabIndex = 4;
            txtCustomSimPath.TextAlign = HorizontalAlignment.Left;
            // 
            // btnBrowseSimPath
            // 
            btnBrowseSimPath.Anchor = AnchorStyles.Right;
            btnBrowseSimPath.BackColor = Color.Black;
            btnBrowseSimPath.FlatAppearance.BorderSize = 0;
            btnBrowseSimPath.FlatStyle = FlatStyle.Flat;
            btnBrowseSimPath.Font = new Font("Segoe UI Semibold", 10F);
            btnBrowseSimPath.ForeColor = Color.White;
            btnBrowseSimPath.Location = new Point(501, 9);
            btnBrowseSimPath.Margin = new Padding(3, 3, 0, 3);
            btnBrowseSimPath.Name = "btnBrowseSimPath";
            btnBrowseSimPath.Size = new Size(86, 26);
            btnBrowseSimPath.TabIndex = 5;
            btnBrowseSimPath.Tag = "Accent";
            btnBrowseSimPath.Text = "Browse...";
            btnBrowseSimPath.UseVisualStyleBackColor = false;
            btnBrowseSimPath.Click += btnBrowseSimPath_Click;
            // 
            // lblCustomSimExePath
            // 
            lblCustomSimExePath.Anchor = AnchorStyles.Left;
            lblCustomSimExePath.AutoSize = true;
            lblCustomSimExePath.Location = new Point(5, 46);
            lblCustomSimExePath.Margin = new Padding(0, 0, 3, 0);
            lblCustomSimExePath.Name = "lblCustomSimExePath";
            lblCustomSimExePath.Size = new Size(108, 19);
            lblCustomSimExePath.TabIndex = 6;
            lblCustomSimExePath.Text = "Executable Path";
            // 
            // txtCustomSimExePath
            // 
            txtCustomSimExePath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtCustomSimExePath.BackColor = Color.Black;
            txtCustomSimExePath.Font = new Font("Segoe UI", 10F);
            txtCustomSimExePath.ForeColor = Color.White;
            txtCustomSimExePath.Location = new Point(148, 44);
            txtCustomSimExePath.MaxLength = 32767;
            txtCustomSimExePath.Name = "txtCustomSimExePath";
            txtCustomSimExePath.Padding = new Padding(5, 0, 5, 0);
            txtCustomSimExePath.PasswordChar = '\0';
            txtCustomSimExePath.ReadOnly = false;
            txtCustomSimExePath.Size = new Size(341, 24);
            txtCustomSimExePath.TabIndex = 6;
            txtCustomSimExePath.TextAlign = HorizontalAlignment.Left;
            // 
            // btnBrowseSimExePath
            // 
            btnBrowseSimExePath.Anchor = AnchorStyles.Right;
            btnBrowseSimExePath.BackColor = Color.Black;
            btnBrowseSimExePath.FlatAppearance.BorderSize = 0;
            btnBrowseSimExePath.FlatStyle = FlatStyle.Flat;
            btnBrowseSimExePath.Font = new Font("Segoe UI Semibold", 10F);
            btnBrowseSimExePath.ForeColor = Color.White;
            btnBrowseSimExePath.Location = new Point(501, 43);
            btnBrowseSimExePath.Margin = new Padding(3, 3, 0, 3);
            btnBrowseSimExePath.Name = "btnBrowseSimExePath";
            btnBrowseSimExePath.Size = new Size(86, 26);
            btnBrowseSimExePath.TabIndex = 7;
            btnBrowseSimExePath.Tag = "Accent";
            btnBrowseSimExePath.Text = "Browse...";
            btnBrowseSimExePath.UseVisualStyleBackColor = false;
            btnBrowseSimExePath.Click += btnBrowseSimExePath_Click;
            // 
            // btnOK
            // 
            btnOK.BackColor = Color.Black;
            btnOK.FlatAppearance.BorderSize = 0;
            btnOK.FlatStyle = FlatStyle.Flat;
            btnOK.Font = new Font("Segoe UI Semibold", 12F);
            btnOK.ForeColor = Color.White;
            btnOK.Location = new Point(420, 252);
            btnOK.Margin = new Padding(0);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(96, 30);
            btnOK.TabIndex = 8;
            btnOK.Tag = "Accent";
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = false;
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Black;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI Semibold", 12F);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(516, 252);
            btnCancel.Margin = new Padding(0);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(96, 30);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // SettingsForm
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            CancelButton = btnCancel;
            ClientSize = new Size(631, 300);
            Controls.Add(lblDetectedSettings);
            Controls.Add(btnRedetect);
            Controls.Add(pnDetectedInfo);
            Controls.Add(chbUseCustom);
            Controls.Add(tlpCustom);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            Font = new Font("Segoe UI Semibold", 10F);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Settings";
            Load += SettingsForm_Load;
            pnDetectedInfo.ResumeLayout(false);
            pnDetectedInfo.PerformLayout();
            tlpCustom.ResumeLayout(false);
            tlpCustom.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblDetectedSettings;
        private Panel pnDetectedInfo;
        private Label lblInstallationType;
        private Label lblPackagesPath;
        private TextBox txtInstallationType;
        private TextBox txtPackagesPath;
        private Button btnRedetect;
        private CheckBox chbUseCustom;
        private HorizontalLineTableLayoutPanel tlpCustom;
        private Label lblCustomSimPath;
        private Label lblCustomSimExePath;
        private CenteredTextBox txtCustomSimPath;
        private CenteredTextBox txtCustomSimExePath;
        private Button btnBrowseSimPath;
        private Button btnBrowseSimExePath;
        private Button btnOK;
        private Button btnCancel;
    }
}