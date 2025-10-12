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
            cmbType = new ComboBox();
            btnOK = new Button();
            btnCancel = new Button();
            txtPath = new TextBox();
            lblPath = new Label();
            btnBrowse = new Button();
            ofdPath = new OpenFileDialog();
            cmbRunOption = new ComboBox();
            lblRunOption = new Label();
            txtDisplayName = new TextBox();
            lblDisplayName = new Label();
            lblDelayBefore = new Label();
            lblDelayAfter = new Label();
            numDelayBefore = new NumericUpDown();
            numDelayAfter = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numDelayBefore).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numDelayAfter).BeginInit();
            SuspendLayout();
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Location = new Point(12, 15);
            lblType.Name = "lblType";
            lblType.Size = new Size(32, 15);
            lblType.TabIndex = 0;
            lblType.Text = "Type";
            // 
            // cmbType
            // 
            cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbType.FormattingEnabled = true;
            cmbType.Location = new Point(108, 12);
            cmbType.Name = "cmbType";
            cmbType.Size = new Size(198, 23);
            cmbType.TabIndex = 1;
            cmbType.SelectedIndexChanged += cmbType_SelectedIndexChanged;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(355, 140);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 23);
            btnOK.TabIndex = 2;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(436, 140);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // txtPath
            // 
            txtPath.Location = new Point(108, 41);
            txtPath.Name = "txtPath";
            txtPath.Size = new Size(314, 23);
            txtPath.TabIndex = 4;
            // 
            // lblPath
            // 
            lblPath.AutoSize = true;
            lblPath.Location = new Point(12, 44);
            lblPath.Name = "lblPath";
            lblPath.Size = new Size(31, 15);
            lblPath.TabIndex = 5;
            lblPath.Text = "Path";
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(436, 41);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(75, 23);
            btnBrowse.TabIndex = 6;
            btnBrowse.Text = "Browse...";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // ofdPath
            // 
            ofdPath.Title = "Select a File";
            // 
            // cmbRunOption
            // 
            cmbRunOption.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRunOption.FormattingEnabled = true;
            cmbRunOption.Location = new Point(108, 70);
            cmbRunOption.Name = "cmbRunOption";
            cmbRunOption.Size = new Size(198, 23);
            cmbRunOption.TabIndex = 7;
            // 
            // lblRunOption
            // 
            lblRunOption.AutoSize = true;
            lblRunOption.Location = new Point(12, 73);
            lblRunOption.Name = "lblRunOption";
            lblRunOption.Size = new Size(68, 15);
            lblRunOption.TabIndex = 8;
            lblRunOption.Text = "Run Option";
            // 
            // txtDisplayName
            // 
            txtDisplayName.Location = new Point(108, 99);
            txtDisplayName.MaxLength = 50;
            txtDisplayName.Name = "txtDisplayName";
            txtDisplayName.Size = new Size(198, 23);
            txtDisplayName.TabIndex = 9;
            // 
            // lblDisplayName
            // 
            lblDisplayName.AutoSize = true;
            lblDisplayName.Location = new Point(12, 102);
            lblDisplayName.Name = "lblDisplayName";
            lblDisplayName.Size = new Size(80, 15);
            lblDisplayName.TabIndex = 10;
            lblDisplayName.Text = "Display Name";
            // 
            // lblDelayBefore
            // 
            lblDelayBefore.AutoSize = true;
            lblDelayBefore.Location = new Point(322, 73);
            lblDelayBefore.Name = "lblDelayBefore";
            lblDelayBefore.Size = new Size(100, 15);
            lblDelayBefore.TabIndex = 11;
            lblDelayBefore.Text = "Delay Before (ms)";
            // 
            // lblDelayAfter
            // 
            lblDelayAfter.AutoSize = true;
            lblDelayAfter.Location = new Point(322, 102);
            lblDelayAfter.Name = "lblDelayAfter";
            lblDelayAfter.Size = new Size(92, 15);
            lblDelayAfter.TabIndex = 12;
            lblDelayAfter.Text = "Delay After (ms)";
            // 
            // numDelayBefore
            // 
            numDelayBefore.Location = new Point(436, 70);
            numDelayBefore.Maximum = new decimal(new int[] { 86400, 0, 0, 0 });
            numDelayBefore.Name = "numDelayBefore";
            numDelayBefore.Size = new Size(75, 23);
            numDelayBefore.TabIndex = 13;
            numDelayBefore.TextAlign = HorizontalAlignment.Right;
            // 
            // numDelayAfter
            // 
            numDelayAfter.Location = new Point(436, 99);
            numDelayAfter.Maximum = new decimal(new int[] { 86400, 0, 0, 0 });
            numDelayAfter.Name = "numDelayAfter";
            numDelayAfter.Size = new Size(75, 23);
            numDelayAfter.TabIndex = 14;
            numDelayAfter.TextAlign = HorizontalAlignment.Right;
            // 
            // StartupItemForm
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(523, 172);
            Controls.Add(numDelayAfter);
            Controls.Add(numDelayBefore);
            Controls.Add(lblDelayAfter);
            Controls.Add(lblDelayBefore);
            Controls.Add(lblDisplayName);
            Controls.Add(txtDisplayName);
            Controls.Add(lblRunOption);
            Controls.Add(cmbRunOption);
            Controls.Add(btnBrowse);
            Controls.Add(lblPath);
            Controls.Add(txtPath);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(cmbType);
            Controls.Add(lblType);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "StartupItemForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Startup Item";
            ((System.ComponentModel.ISupportInitialize)numDelayBefore).EndInit();
            ((System.ComponentModel.ISupportInitialize)numDelayAfter).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblType;
        private ComboBox cmbType;
        private Button btnOK;
        private Button btnCancel;
        private TextBox txtPath;
        private Label lblPath;
        private Button btnBrowse;
        private OpenFileDialog ofdPath;
        private ComboBox cmbRunOption;
        private Label lblRunOption;
        private TextBox txtDisplayName;
        private Label lblDisplayName;
        private Label lblDelayBefore;
        private Label lblDelayAfter;
        private NumericUpDown numDelayBefore;
        private NumericUpDown numDelayAfter;
    }
}