namespace FS24StartHub.App.WinForms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            btnStart = new Button();
            btnExit = new Button();
            btnAppsEdit = new Button();
            btnAppsAdd = new Button();
            btnAppsRemove = new Button();
            btnAppsReload = new Button();
            btnAppsMoveDown = new Button();
            btnAppsMoveUp = new Button();
            clbApps = new FS24StartHub.App.WinForms.Controls.CustomCheckedListBox();
            btnSave = new Button();
            chbKeepOpen = new CheckBox();
            lblApps = new Label();
            pnApps = new Panel();
            lblVersion = new Label();
            btnSettings = new Button();
            pnConfigs = new Panel();
            btnCaptureConfig = new Button();
            dgvConfigs = new DataGridView();
            colMarker = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colCreatedDate = new DataGridViewTextBoxColumn();
            colInfo = new DataGridViewButtonColumn();
            colEdit = new DataGridViewButtonColumn();
            colDelete = new DataGridViewButtonColumn();
            lblConfigs = new Label();
            toolTip1 = new ToolTip(components);
            lblLegendCurrent = new Label();
            lblLegendSelected = new Label();
            pnApps.SuspendLayout();
            pnConfigs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvConfigs).BeginInit();
            SuspendLayout();
            // 
            // btnStart
            // 
            btnStart.BackColor = Color.Blue;
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnStart.ForeColor = Color.White;
            btnStart.Location = new Point(440, 328);
            btnStart.Name = "btnStart";
            btnStart.Padding = new Padding(10, 5, 0, 0);
            btnStart.Size = new Size(320, 111);
            btnStart.TabIndex = 0;
            btnStart.Tag = "Start";
            btnStart.Text = "Start";
            btnStart.TextAlign = ContentAlignment.TopLeft;
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += btnStart_Click;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.Transparent;
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 255, 128);
            btnExit.FlatAppearance.MouseOverBackColor = Color.White;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnExit.ForeColor = Color.White;
            btnExit.Location = new Point(21, 399);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(114, 40);
            btnExit.TabIndex = 3;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            // 
            // btnAppsEdit
            // 
            btnAppsEdit.BackColor = Color.Black;
            btnAppsEdit.FlatAppearance.BorderSize = 0;
            btnAppsEdit.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 255, 128);
            btnAppsEdit.FlatAppearance.MouseOverBackColor = Color.White;
            btnAppsEdit.FlatStyle = FlatStyle.Flat;
            btnAppsEdit.Font = new Font("Segoe MDL2 Assets", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAppsEdit.ForeColor = Color.White;
            btnAppsEdit.Location = new Point(330, 70);
            btnAppsEdit.Name = "btnAppsEdit";
            btnAppsEdit.Size = new Size(30, 30);
            btnAppsEdit.TabIndex = 2;
            btnAppsEdit.Text = "";
            toolTip1.SetToolTip(btnAppsEdit, "Edit");
            btnAppsEdit.UseVisualStyleBackColor = false;
            btnAppsEdit.Click += btnAppsEdit_Click;
            // 
            // btnAppsAdd
            // 
            btnAppsAdd.BackColor = Color.Black;
            btnAppsAdd.FlatAppearance.BorderSize = 0;
            btnAppsAdd.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 255, 128);
            btnAppsAdd.FlatAppearance.MouseOverBackColor = Color.White;
            btnAppsAdd.FlatStyle = FlatStyle.Flat;
            btnAppsAdd.Font = new Font("Segoe MDL2 Assets", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAppsAdd.ForeColor = Color.White;
            btnAppsAdd.Location = new Point(330, 35);
            btnAppsAdd.Name = "btnAppsAdd";
            btnAppsAdd.Size = new Size(30, 30);
            btnAppsAdd.TabIndex = 1;
            btnAppsAdd.Text = "";
            toolTip1.SetToolTip(btnAppsAdd, "Add");
            btnAppsAdd.UseVisualStyleBackColor = false;
            btnAppsAdd.Click += btnAppsAdd_Click;
            // 
            // btnAppsRemove
            // 
            btnAppsRemove.BackColor = Color.Black;
            btnAppsRemove.FlatAppearance.BorderSize = 0;
            btnAppsRemove.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 255, 128);
            btnAppsRemove.FlatAppearance.MouseOverBackColor = Color.White;
            btnAppsRemove.FlatStyle = FlatStyle.Flat;
            btnAppsRemove.Font = new Font("Segoe MDL2 Assets", 9.75F, FontStyle.Bold);
            btnAppsRemove.ForeColor = Color.White;
            btnAppsRemove.Location = new Point(330, 105);
            btnAppsRemove.Name = "btnAppsRemove";
            btnAppsRemove.Size = new Size(30, 30);
            btnAppsRemove.TabIndex = 3;
            btnAppsRemove.Text = "";
            toolTip1.SetToolTip(btnAppsRemove, "Delete");
            btnAppsRemove.UseVisualStyleBackColor = false;
            btnAppsRemove.Click += btnAppsRemove_Click;
            // 
            // btnAppsReload
            // 
            btnAppsReload.BackColor = Color.Black;
            btnAppsReload.FlatAppearance.BorderSize = 0;
            btnAppsReload.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 255, 128);
            btnAppsReload.FlatAppearance.MouseOverBackColor = Color.White;
            btnAppsReload.FlatStyle = FlatStyle.Flat;
            btnAppsReload.Font = new Font("Segoe MDL2 Assets", 9.75F, FontStyle.Bold);
            btnAppsReload.ForeColor = Color.White;
            btnAppsReload.Location = new Point(330, 175);
            btnAppsReload.Name = "btnAppsReload";
            btnAppsReload.Size = new Size(30, 30);
            btnAppsReload.TabIndex = 4;
            btnAppsReload.Text = "";
            toolTip1.SetToolTip(btnAppsReload, "Reload");
            btnAppsReload.UseVisualStyleBackColor = false;
            btnAppsReload.Click += btnAppsReload_Click;
            // 
            // btnAppsMoveDown
            // 
            btnAppsMoveDown.BackColor = Color.Black;
            btnAppsMoveDown.FlatAppearance.BorderSize = 0;
            btnAppsMoveDown.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 255, 128);
            btnAppsMoveDown.FlatAppearance.MouseOverBackColor = Color.White;
            btnAppsMoveDown.FlatStyle = FlatStyle.Flat;
            btnAppsMoveDown.Font = new Font("Segoe MDL2 Assets", 9.75F, FontStyle.Bold);
            btnAppsMoveDown.ForeColor = Color.White;
            btnAppsMoveDown.Location = new Point(330, 245);
            btnAppsMoveDown.Name = "btnAppsMoveDown";
            btnAppsMoveDown.Size = new Size(30, 30);
            btnAppsMoveDown.TabIndex = 6;
            btnAppsMoveDown.Text = "";
            toolTip1.SetToolTip(btnAppsMoveDown, "Move Down");
            btnAppsMoveDown.UseVisualStyleBackColor = false;
            btnAppsMoveDown.Click += btnAppsMoveDown_Click;
            // 
            // btnAppsMoveUp
            // 
            btnAppsMoveUp.BackColor = Color.Black;
            btnAppsMoveUp.FlatAppearance.BorderSize = 0;
            btnAppsMoveUp.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 255, 128);
            btnAppsMoveUp.FlatAppearance.MouseOverBackColor = Color.White;
            btnAppsMoveUp.FlatStyle = FlatStyle.Flat;
            btnAppsMoveUp.Font = new Font("Segoe MDL2 Assets", 9.75F, FontStyle.Bold);
            btnAppsMoveUp.ForeColor = Color.White;
            btnAppsMoveUp.Location = new Point(330, 210);
            btnAppsMoveUp.Name = "btnAppsMoveUp";
            btnAppsMoveUp.Size = new Size(30, 30);
            btnAppsMoveUp.TabIndex = 5;
            btnAppsMoveUp.Text = "";
            toolTip1.SetToolTip(btnAppsMoveUp, "Move Up");
            btnAppsMoveUp.UseVisualStyleBackColor = false;
            btnAppsMoveUp.Click += btnAppsMoveUp_Click;
            // 
            // clbApps
            // 
            clbApps.BackColor = Color.DimGray;
            clbApps.BorderStyle = BorderStyle.None;
            clbApps.CustomCheckBoxBackColor = Color.FromArgb(0, 120, 215);
            clbApps.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            clbApps.ForeColor = Color.White;
            clbApps.FormattingEnabled = true;
            clbApps.IntegralHeight = false;
            clbApps.ItemBackColor = Color.Empty;
            clbApps.ItemSelectedColor = Color.Empty;
            clbApps.ItemSelectedForeColor = Color.Empty;
            clbApps.Location = new Point(0, 35);
            clbApps.Name = "clbApps";
            clbApps.ReadonlyForeColor = Color.Empty;
            clbApps.ReadonlySelectedForeColor = Color.Empty;
            clbApps.Size = new Size(324, 240);
            clbApps.TabIndex = 0;
            clbApps.ToolTipBackColor = Color.Empty;
            clbApps.ToolTipForeColor = Color.Empty;
            clbApps.ItemCheck += clbApps_ItemCheck;
            clbApps.SelectedIndexChanged += clbApps_SelectedIndexChanged;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.Transparent;
            btnSave.Enabled = false;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 255, 128);
            btnSave.FlatAppearance.MouseOverBackColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(141, 399);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(114, 40);
            btnSave.TabIndex = 4;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // chbKeepOpen
            // 
            chbKeepOpen.AutoSize = true;
            chbKeepOpen.BackColor = Color.Transparent;
            chbKeepOpen.BackgroundImageLayout = ImageLayout.None;
            chbKeepOpen.FlatAppearance.BorderColor = Color.Red;
            chbKeepOpen.FlatAppearance.BorderSize = 3;
            chbKeepOpen.FlatAppearance.CheckedBackColor = Color.White;
            chbKeepOpen.FlatStyle = FlatStyle.Flat;
            chbKeepOpen.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chbKeepOpen.ForeColor = Color.White;
            chbKeepOpen.Location = new Point(661, 297);
            chbKeepOpen.Name = "chbKeepOpen";
            chbKeepOpen.Size = new Size(89, 21);
            chbKeepOpen.TabIndex = 2;
            chbKeepOpen.Text = "Keep open";
            chbKeepOpen.UseVisualStyleBackColor = false;
            // 
            // lblApps
            // 
            lblApps.AutoSize = true;
            lblApps.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblApps.ForeColor = Color.White;
            lblApps.Location = new Point(0, 7);
            lblApps.Name = "lblApps";
            lblApps.Size = new Size(228, 21);
            lblApps.TabIndex = 7;
            lblApps.Text = "Startup Applications && Scripts";
            // 
            // pnApps
            // 
            pnApps.BackColor = Color.Transparent;
            pnApps.Controls.Add(lblApps);
            pnApps.Controls.Add(btnAppsEdit);
            pnApps.Controls.Add(btnAppsMoveUp);
            pnApps.Controls.Add(btnAppsReload);
            pnApps.Controls.Add(btnAppsRemove);
            pnApps.Controls.Add(clbApps);
            pnApps.Controls.Add(btnAppsAdd);
            pnApps.Controls.Add(btnAppsMoveDown);
            pnApps.Location = new Point(21, 12);
            pnApps.Name = "pnApps";
            pnApps.Size = new Size(360, 275);
            pnApps.TabIndex = 1;
            // 
            // lblVersion
            // 
            lblVersion.BackColor = Color.FromArgb(20, 72, 147);
            lblVersion.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblVersion.ForeColor = Color.White;
            lblVersion.Location = new Point(250, 6);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(100, 30);
            lblVersion.TabIndex = 5;
            lblVersion.Text = "v0.0.0-dev";
            lblVersion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnSettings
            // 
            btnSettings.BackColor = Color.FromArgb(20, 72, 147);
            btnSettings.FlatAppearance.BorderSize = 0;
            btnSettings.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 255, 128);
            btnSettings.FlatAppearance.MouseOverBackColor = Color.White;
            btnSettings.FlatStyle = FlatStyle.Flat;
            btnSettings.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            btnSettings.ForeColor = Color.White;
            btnSettings.Location = new Point(350, 6);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(30, 30);
            btnSettings.TabIndex = 6;
            btnSettings.Tag = "Settings";
            btnSettings.Text = "Settings";
            btnSettings.UseVisualStyleBackColor = false;
            btnSettings.Click += btnSettings_Click;
            // 
            // pnConfigs
            // 
            pnConfigs.BackColor = Color.Transparent;
            pnConfigs.Controls.Add(btnCaptureConfig);
            pnConfigs.Controls.Add(dgvConfigs);
            pnConfigs.Controls.Add(lblConfigs);
            pnConfigs.Controls.Add(lblVersion);
            pnConfigs.Controls.Add(btnSettings);
            pnConfigs.Location = new Point(400, 0);
            pnConfigs.Name = "pnConfigs";
            pnConfigs.Size = new Size(380, 287);
            pnConfigs.TabIndex = 7;
            // 
            // btnCaptureConfig
            // 
            btnCaptureConfig.FlatAppearance.BorderSize = 0;
            btnCaptureConfig.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 255, 128);
            btnCaptureConfig.FlatAppearance.MouseOverBackColor = Color.White;
            btnCaptureConfig.FlatStyle = FlatStyle.Flat;
            btnCaptureConfig.Font = new Font("Segoe MDL2 Assets", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCaptureConfig.ForeColor = Color.White;
            btnCaptureConfig.Location = new Point(149, 14);
            btnCaptureConfig.Name = "btnCaptureConfig";
            btnCaptureConfig.Size = new Size(30, 30);
            btnCaptureConfig.TabIndex = 8;
            btnCaptureConfig.Text = "";
            toolTip1.SetToolTip(btnCaptureConfig, "Capture Current Graphics Settings");
            btnCaptureConfig.UseVisualStyleBackColor = false;
            btnCaptureConfig.Click += btnCaptureConfig_Click;
            // 
            // dgvConfigs
            // 
            dgvConfigs.AllowUserToAddRows = false;
            dgvConfigs.AllowUserToDeleteRows = false;
            dgvConfigs.AllowUserToResizeColumns = false;
            dgvConfigs.AllowUserToResizeRows = false;
            dgvConfigs.BackgroundColor = Color.DimGray;
            dgvConfigs.BorderStyle = BorderStyle.None;
            dgvConfigs.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvConfigs.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvConfigs.ColumnHeadersHeight = 20;
            dgvConfigs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvConfigs.Columns.AddRange(new DataGridViewColumn[] { colMarker, colName, colCreatedDate, colInfo, colEdit, colDelete });
            dgvConfigs.EnableHeadersVisualStyles = false;
            dgvConfigs.GridColor = Color.Silver;
            dgvConfigs.Location = new Point(0, 47);
            dgvConfigs.MultiSelect = false;
            dgvConfigs.Name = "dgvConfigs";
            dgvConfigs.ReadOnly = true;
            dgvConfigs.RowHeadersVisible = false;
            dgvConfigs.RowTemplate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvConfigs.RowTemplate.Height = 20;
            dgvConfigs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvConfigs.ShowCellToolTips = false;
            dgvConfigs.Size = new Size(360, 240);
            dgvConfigs.TabIndex = 9;
            dgvConfigs.CellClick += dgvConfigs_CellClick;
            dgvConfigs.CellDoubleClick += dgvConfigs_CellDoubleClick;
            dgvConfigs.KeyDown += dgvConfigs_KeyDown;
            // 
            // colMarker
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colMarker.DefaultCellStyle = dataGridViewCellStyle1;
            colMarker.HeaderText = "";
            colMarker.Name = "colMarker";
            colMarker.ReadOnly = true;
            colMarker.Resizable = DataGridViewTriState.False;
            colMarker.SortMode = DataGridViewColumnSortMode.NotSortable;
            colMarker.Width = 20;
            // 
            // colName
            // 
            colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colName.HeaderText = "Name";
            colName.Name = "colName";
            colName.ReadOnly = true;
            colName.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // colCreatedDate
            // 
            colCreatedDate.HeaderText = "Created Date";
            colCreatedDate.Name = "colCreatedDate";
            colCreatedDate.ReadOnly = true;
            colCreatedDate.SortMode = DataGridViewColumnSortMode.NotSortable;
            colCreatedDate.Width = 90;
            // 
            // colInfo
            // 
            colInfo.FlatStyle = FlatStyle.Flat;
            colInfo.HeaderText = "";
            colInfo.Name = "colInfo";
            colInfo.ReadOnly = true;
            colInfo.Resizable = DataGridViewTriState.False;
            colInfo.Text = "ℹ";
            colInfo.UseColumnTextForButtonValue = true;
            colInfo.Width = 20;
            // 
            // colEdit
            // 
            colEdit.FlatStyle = FlatStyle.Flat;
            colEdit.HeaderText = "";
            colEdit.Name = "colEdit";
            colEdit.ReadOnly = true;
            colEdit.Resizable = DataGridViewTriState.False;
            colEdit.Text = "";
            colEdit.UseColumnTextForButtonValue = true;
            colEdit.Width = 20;
            // 
            // colDelete
            // 
            colDelete.FlatStyle = FlatStyle.Flat;
            colDelete.HeaderText = "";
            colDelete.Name = "colDelete";
            colDelete.ReadOnly = true;
            colDelete.Resizable = DataGridViewTriState.False;
            colDelete.Text = "";
            colDelete.UseColumnTextForButtonValue = true;
            colDelete.Width = 20;
            // 
            // lblConfigs
            // 
            lblConfigs.AutoSize = true;
            lblConfigs.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblConfigs.ForeColor = Color.White;
            lblConfigs.Location = new Point(0, 19);
            lblConfigs.Name = "lblConfigs";
            lblConfigs.Size = new Size(132, 21);
            lblConfigs.TabIndex = 8;
            lblConfigs.Text = "Graphics Profiles";
            // 
            // lblLegendCurrent
            // 
            lblLegendCurrent.AutoSize = true;
            lblLegendCurrent.BackColor = Color.Transparent;
            lblLegendCurrent.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLegendCurrent.ForeColor = Color.Yellow;
            lblLegendCurrent.Location = new Point(400, 290);
            lblLegendCurrent.Name = "lblLegendCurrent";
            lblLegendCurrent.Size = new Size(94, 15);
            lblLegendCurrent.TabIndex = 8;
            lblLegendCurrent.Text = "● Current profile";
            // 
            // lblLegendSelected
            // 
            lblLegendSelected.AutoSize = true;
            lblLegendSelected.BackColor = Color.Transparent;
            lblLegendSelected.ForeColor = Color.Lime;
            lblLegendSelected.Location = new Point(513, 290);
            lblLegendSelected.Name = "lblLegendSelected";
            lblLegendSelected.Size = new Size(98, 15);
            lblLegendSelected.TabIndex = 9;
            lblLegendSelected.Text = "● Selected profile";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(24, 36, 54);
            BackgroundImage = Resources.bg080_1u;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(784, 461);
            Controls.Add(lblLegendSelected);
            Controls.Add(lblLegendCurrent);
            Controls.Add(pnConfigs);
            Controls.Add(pnApps);
            Controls.Add(chbKeepOpen);
            Controls.Add(btnSave);
            Controls.Add(btnExit);
            Controls.Add(btnStart);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Tag = "Settings";
            Text = "MSFS 2024 Start Hub";
            Load += MainForm_Load;
            pnApps.ResumeLayout(false);
            pnApps.PerformLayout();
            pnConfigs.ResumeLayout(false);
            pnConfigs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvConfigs).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnStart;
        private Button btnExit;
        private Controls.CustomCheckedListBox clbApps;
        private Button btnAppsMoveUp;
        private Button btnAppsMoveDown;
        private Button btnAppsReload;
        private Button btnAppsRemove;
        private Button btnAppsAdd;
        private Button btnAppsEdit;
        private Button btnSave;
        private CheckBox chbKeepOpen;
        private Label lblApps;
        private Panel pnApps;
        private Label lblVersion;
        private Button btnSettings;
        private Panel pnConfigs;
        private Label lblConfigs;
        private DataGridView dgvConfigs;
        private DataGridViewTextBoxColumn colMarker;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colCreatedDate;
        private DataGridViewButtonColumn colInfo;
        private DataGridViewButtonColumn colEdit;
        private DataGridViewButtonColumn colDelete;
        private Button btnCaptureConfig;
        private ToolTip toolTip1;
        private Label lblLegendCurrent;
        private Label lblLegendSelected;
    }
}
