using FS24StartHub.App.WinForms.Controls;

namespace FS24StartHub.App.WinForms
{
    partial class StartupItemForm
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
            lblType = new Label();
            cmbType = new CustomComboBox();
            btnOK = new Button();
            btnCancel = new Button();
            txtPath = new CenteredTextBox();
            lblPath = new Label();
            btnBrowse = new Button();
            ofdPath = new OpenFileDialog();
            cmbRunOption = new CustomComboBox();
            lblRunOption = new Label();
            txtDisplayName = new CenteredTextBox();
            lblDisplayName = new Label();
            lblDelayBefore = new Label();
            lblDelayAfter = new Label();
            numDelayBefore = new SideButtonsNumeric();
            numDelayAfter = new SideButtonsNumeric();
            tlpStartupItem = new HorizontalLineTableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)numDelayBefore).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numDelayAfter).BeginInit();
            tlpStartupItem.SuspendLayout();
            SuspendLayout();
            // 
            // lblType
            // 
            lblType.Anchor = AnchorStyles.Left;
            lblType.AutoSize = true;
            lblType.Location = new Point(5, 13);
            lblType.Margin = new Padding(0, 0, 3, 0);
            lblType.Name = "lblType";
            lblType.Size = new Size(38, 19);
            lblType.TabIndex = 0;
            lblType.Text = "Type";
            // 
            // cmbType
            // 
            cmbType.Anchor = AnchorStyles.Left;
            cmbType.BackColor = Color.MediumBlue;
            cmbType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbType.FlatStyle = FlatStyle.Flat;
            cmbType.Font = new Font("Segoe UI", 10F);
            cmbType.ForeColor = Color.White;
            cmbType.FormattingEnabled = true;
            cmbType.Location = new Point(128, 9);
            cmbType.Name = "cmbType";
            cmbType.Size = new Size(224, 26);
            cmbType.TabIndex = 1;
            cmbType.SelectedIndexChanged += cmbType_SelectedIndexChanged;
            // 
            // btnOK
            // 
            btnOK.BackColor = Color.Black;
            btnOK.FlatAppearance.BorderSize = 0;
            btnOK.FlatAppearance.MouseDownBackColor = Color.FromArgb(224, 224, 224);
            btnOK.FlatAppearance.MouseOverBackColor = Color.White;
            btnOK.FlatStyle = FlatStyle.Flat;
            btnOK.Font = new Font("Segoe UI Semibold", 12F);
            btnOK.Location = new Point(420, 188);
            btnOK.Margin = new Padding(0);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(96, 30);
            btnOK.TabIndex = 2;
            btnOK.Tag = "Accent";
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = false;
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Black;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(224, 224, 224);
            btnCancel.FlatAppearance.MouseOverBackColor = Color.White;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI Semibold", 12F);
            btnCancel.Location = new Point(516, 188);
            btnCancel.Margin = new Padding(0);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(96, 30);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // txtPath
            // 
            txtPath.Anchor = AnchorStyles.Left;
            txtPath.BackColor = Color.Black;
            tlpStartupItem.SetColumnSpan(txtPath, 2);
            txtPath.Font = new Font("Segoe UI", 10F);
            txtPath.ForeColor = Color.White;
            txtPath.Location = new Point(128, 45);
            txtPath.MaxLength = 32767;
            txtPath.Name = "txtPath";
            txtPath.Padding = new Padding(5, 0, 5, 0);
            txtPath.PasswordChar = '\0';
            txtPath.ReadOnly = false;
            txtPath.Size = new Size(358, 24);
            txtPath.TabIndex = 4;
            txtPath.TextAlign = HorizontalAlignment.Left;
            // 
            // lblPath
            // 
            lblPath.Anchor = AnchorStyles.Left;
            lblPath.AutoSize = true;
            lblPath.Location = new Point(5, 48);
            lblPath.Margin = new Padding(0, 0, 3, 0);
            lblPath.Name = "lblPath";
            lblPath.Size = new Size(37, 19);
            lblPath.TabIndex = 5;
            lblPath.Text = "Path";
            // 
            // btnBrowse
            // 
            btnBrowse.Anchor = AnchorStyles.Right;
            btnBrowse.BackColor = Color.Black;
            btnBrowse.FlatAppearance.BorderSize = 0;
            btnBrowse.FlatAppearance.MouseDownBackColor = Color.FromArgb(224, 224, 224);
            btnBrowse.FlatAppearance.MouseOverBackColor = Color.White;
            btnBrowse.FlatStyle = FlatStyle.Flat;
            btnBrowse.Location = new Point(499, 44);
            btnBrowse.Margin = new Padding(3, 3, 0, 3);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(86, 26);
            btnBrowse.TabIndex = 6;
            btnBrowse.Tag = "Accent";
            btnBrowse.Text = "Browse...";
            btnBrowse.UseVisualStyleBackColor = false;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // ofdPath
            // 
            ofdPath.Title = "Select a File";
            // 
            // cmbRunOption
            // 
            cmbRunOption.Anchor = AnchorStyles.Left;
            cmbRunOption.BackColor = Color.MediumBlue;
            cmbRunOption.DrawMode = DrawMode.OwnerDrawFixed;
            cmbRunOption.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRunOption.FlatStyle = FlatStyle.Flat;
            cmbRunOption.Font = new Font("Segoe UI", 10F);
            cmbRunOption.ForeColor = Color.White;
            cmbRunOption.FormattingEnabled = true;
            cmbRunOption.Location = new Point(128, 79);
            cmbRunOption.Name = "cmbRunOption";
            cmbRunOption.Size = new Size(224, 26);
            cmbRunOption.TabIndex = 7;
            // 
            // lblRunOption
            // 
            lblRunOption.Anchor = AnchorStyles.Left;
            lblRunOption.AutoSize = true;
            lblRunOption.Location = new Point(5, 83);
            lblRunOption.Margin = new Padding(0, 0, 3, 0);
            lblRunOption.Name = "lblRunOption";
            lblRunOption.Size = new Size(82, 19);
            lblRunOption.TabIndex = 8;
            lblRunOption.Text = "Run Option";
            // 
            // txtDisplayName
            // 
            txtDisplayName.Anchor = AnchorStyles.Left;
            txtDisplayName.BackColor = Color.Black;
            txtDisplayName.Font = new Font("Segoe UI", 10F);
            txtDisplayName.ForeColor = Color.White;
            txtDisplayName.Location = new Point(128, 115);
            txtDisplayName.MaxLength = 50;
            txtDisplayName.Name = "txtDisplayName";
            txtDisplayName.Padding = new Padding(5, 0, 5, 0);
            txtDisplayName.PasswordChar = '\0';
            txtDisplayName.ReadOnly = false;
            txtDisplayName.Size = new Size(224, 24);
            txtDisplayName.TabIndex = 9;
            txtDisplayName.TextAlign = HorizontalAlignment.Left;
            // 
            // lblDisplayName
            // 
            lblDisplayName.Anchor = AnchorStyles.Left;
            lblDisplayName.AutoSize = true;
            lblDisplayName.Location = new Point(5, 118);
            lblDisplayName.Margin = new Padding(0, 0, 3, 0);
            lblDisplayName.Name = "lblDisplayName";
            lblDisplayName.Size = new Size(96, 19);
            lblDisplayName.TabIndex = 10;
            lblDisplayName.Text = "Display Name";
            // 
            // lblDelayBefore
            // 
            lblDelayBefore.Anchor = AnchorStyles.Left;
            lblDelayBefore.AutoSize = true;
            lblDelayBefore.Location = new Point(370, 83);
            lblDelayBefore.Margin = new Padding(15, 0, 3, 0);
            lblDelayBefore.Name = "lblDelayBefore";
            lblDelayBefore.Size = new Size(120, 19);
            lblDelayBefore.TabIndex = 11;
            lblDelayBefore.Text = "Delay Before (ms)";
            // 
            // lblDelayAfter
            // 
            lblDelayAfter.Anchor = AnchorStyles.Left;
            lblDelayAfter.AutoSize = true;
            lblDelayAfter.Location = new Point(370, 118);
            lblDelayAfter.Margin = new Padding(15, 0, 3, 0);
            lblDelayAfter.Name = "lblDelayAfter";
            lblDelayAfter.Size = new Size(111, 19);
            lblDelayAfter.TabIndex = 12;
            lblDelayAfter.Text = "Delay After (ms)";
            // 
            // numDelayBefore
            // 
            numDelayBefore.Anchor = AnchorStyles.Right;
            numDelayBefore.BackColor = Color.Black;
            numDelayBefore.ButtonBackColor = Color.Empty;
            numDelayBefore.ButtonForeColor = Color.Empty;
            numDelayBefore.Font = new Font("Segoe UI", 10F);
            numDelayBefore.ForeColor = Color.White;
            numDelayBefore.Location = new Point(499, 80);
            numDelayBefore.Margin = new Padding(3, 3, 0, 3);
            numDelayBefore.Maximum = new decimal(new int[] { 86400, 0, 0, 0 });
            numDelayBefore.Name = "numDelayBefore";
            numDelayBefore.Size = new Size(86, 24);
            numDelayBefore.TabIndex = 13;
            numDelayBefore.TextBoxBackColor = Color.Empty;
            numDelayBefore.TextBoxForeColor = Color.Empty;
            // 
            // numDelayAfter
            // 
            numDelayAfter.Anchor = AnchorStyles.Right;
            numDelayAfter.BackColor = Color.Black;
            numDelayAfter.ButtonBackColor = Color.Empty;
            numDelayAfter.ButtonForeColor = Color.Empty;
            numDelayAfter.Font = new Font("Segoe UI", 10F);
            numDelayAfter.ForeColor = Color.White;
            numDelayAfter.Location = new Point(499, 115);
            numDelayAfter.Margin = new Padding(3, 3, 0, 3);
            numDelayAfter.Maximum = new decimal(new int[] { 86400, 0, 0, 0 });
            numDelayAfter.Name = "numDelayAfter";
            numDelayAfter.Size = new Size(86, 24);
            numDelayAfter.TabIndex = 14;
            numDelayAfter.TextBoxBackColor = Color.Empty;
            numDelayAfter.TextBoxForeColor = Color.Empty;
            // 
            // tlpStartupItem
            // 
            tlpStartupItem.BackColor = Color.FromArgb(64, 64, 64);
            tlpStartupItem.ColumnCount = 6;
            tlpStartupItem.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 5F));
            tlpStartupItem.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tlpStartupItem.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 230F));
            tlpStartupItem.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            tlpStartupItem.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            tlpStartupItem.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 5F));
            tlpStartupItem.Controls.Add(lblDisplayName, 1, 4);
            tlpStartupItem.Controls.Add(txtDisplayName, 2, 4);
            tlpStartupItem.Controls.Add(lblDelayAfter, 3, 4);
            tlpStartupItem.Controls.Add(numDelayAfter, 4, 4);
            tlpStartupItem.Controls.Add(lblRunOption, 1, 3);
            tlpStartupItem.Controls.Add(cmbRunOption, 2, 3);
            tlpStartupItem.Controls.Add(lblDelayBefore, 3, 3);
            tlpStartupItem.Controls.Add(numDelayBefore, 4, 3);
            tlpStartupItem.Controls.Add(btnBrowse, 4, 2);
            tlpStartupItem.Controls.Add(txtPath, 2, 2);
            tlpStartupItem.Controls.Add(lblPath, 1, 2);
            tlpStartupItem.Controls.Add(lblType, 1, 1);
            tlpStartupItem.Controls.Add(cmbType, 2, 1);
            tlpStartupItem.LineColor = Color.DimGray;
            tlpStartupItem.LineThickness = 2;
            tlpStartupItem.Location = new Point(20, 20);
            tlpStartupItem.Name = "tlpStartupItem";
            tlpStartupItem.RowCount = 6;
            tlpStartupItem.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            tlpStartupItem.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tlpStartupItem.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tlpStartupItem.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tlpStartupItem.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tlpStartupItem.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            tlpStartupItem.Size = new Size(592, 152);
            tlpStartupItem.TabIndex = 15;
            // 
            // StartupItemForm
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            CancelButton = btnCancel;
            ClientSize = new Size(631, 240);
            Controls.Add(tlpStartupItem);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Font = new Font("Segoe UI Semibold", 10F);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "StartupItemForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Startup Item";
            Load += StartupItemForm_Load;
            ((System.ComponentModel.ISupportInitialize)numDelayBefore).EndInit();
            ((System.ComponentModel.ISupportInitialize)numDelayAfter).EndInit();
            tlpStartupItem.ResumeLayout(false);
            tlpStartupItem.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lblType;
        private CustomComboBox cmbType;
        private Button btnOK;
        private Button btnCancel;
        private CenteredTextBox txtPath;
        private Label lblPath;
        private Button btnBrowse;
        private OpenFileDialog ofdPath;
        private CustomComboBox cmbRunOption;
        private Label lblRunOption;
        private CenteredTextBox txtDisplayName;
        private Label lblDisplayName;
        private Label lblDelayBefore;
        private Label lblDelayAfter;
        private SideButtonsNumeric numDelayBefore;
        private SideButtonsNumeric numDelayAfter;
        private HorizontalLineTableLayoutPanel tlpStartupItem;
    }
}