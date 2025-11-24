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
            pnApps.SuspendLayout();
            SuspendLayout();
            // 
            // btnStart
            // 
            btnStart.BackColor = Color.Blue;
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnStart.ForeColor = Color.White;
            btnStart.Location = new Point(430, 328);
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
            btnAppsEdit.Font = new Font("Segoe UI Semibold", 9.75F);
            btnAppsEdit.ForeColor = Color.White;
            btnAppsEdit.Location = new Point(375, 70);
            btnAppsEdit.Name = "btnAppsEdit";
            btnAppsEdit.Size = new Size(90, 30);
            btnAppsEdit.TabIndex = 2;
            btnAppsEdit.Text = "Edit...";
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
            btnAppsAdd.Font = new Font("Segoe UI Semibold", 9.75F);
            btnAppsAdd.ForeColor = Color.White;
            btnAppsAdd.Location = new Point(375, 35);
            btnAppsAdd.Name = "btnAppsAdd";
            btnAppsAdd.Size = new Size(90, 30);
            btnAppsAdd.TabIndex = 1;
            btnAppsAdd.Text = "Add...";
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
            btnAppsRemove.Font = new Font("Segoe UI Semibold", 9.75F);
            btnAppsRemove.ForeColor = Color.White;
            btnAppsRemove.Location = new Point(375, 105);
            btnAppsRemove.Name = "btnAppsRemove";
            btnAppsRemove.Size = new Size(90, 30);
            btnAppsRemove.TabIndex = 3;
            btnAppsRemove.Text = "Remove";
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
            btnAppsReload.Font = new Font("Segoe UI Semibold", 9.75F);
            btnAppsReload.ForeColor = Color.White;
            btnAppsReload.Location = new Point(375, 175);
            btnAppsReload.Name = "btnAppsReload";
            btnAppsReload.Size = new Size(90, 30);
            btnAppsReload.TabIndex = 4;
            btnAppsReload.Text = "Reload";
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
            btnAppsMoveDown.Font = new Font("Segoe UI Semibold", 9.75F);
            btnAppsMoveDown.ForeColor = Color.White;
            btnAppsMoveDown.Location = new Point(375, 245);
            btnAppsMoveDown.Name = "btnAppsMoveDown";
            btnAppsMoveDown.Size = new Size(90, 30);
            btnAppsMoveDown.TabIndex = 6;
            btnAppsMoveDown.Text = "Down";
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
            btnAppsMoveUp.Font = new Font("Segoe UI Semibold", 9.75F);
            btnAppsMoveUp.ForeColor = Color.White;
            btnAppsMoveUp.Location = new Point(375, 210);
            btnAppsMoveUp.Name = "btnAppsMoveUp";
            btnAppsMoveUp.Size = new Size(90, 30);
            btnAppsMoveUp.TabIndex = 5;
            btnAppsMoveUp.Text = "Up";
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
            clbApps.ItemBackColor = Color.DimGray;
            clbApps.ItemSelectedColor = Color.Empty;
            clbApps.ItemSelectedForeColor = Color.Empty;
            clbApps.Location = new Point(0, 35);
            clbApps.Name = "clbApps";
            clbApps.ReadonlyForeColor = Color.Empty;
            clbApps.ReadonlySelectedForeColor = Color.Empty;
            clbApps.Size = new Size(350, 240);
            clbApps.TabIndex = 0;
            clbApps.ToolTipBackColor = Color.SteelBlue;
            clbApps.ToolTipForeColor = Color.White;
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
            chbKeepOpen.FlatAppearance.BorderColor = Color.Red;
            chbKeepOpen.FlatAppearance.BorderSize = 3;
            chbKeepOpen.FlatAppearance.CheckedBackColor = Color.White;
            chbKeepOpen.FlatStyle = FlatStyle.Flat;
            chbKeepOpen.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chbKeepOpen.ForeColor = Color.White;
            chbKeepOpen.Location = new Point(651, 292);
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
            pnApps.Size = new Size(465, 275);
            pnApps.TabIndex = 1;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Resources.bg_fs24sh;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(784, 461);
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
            Text = "MSFS 2024 Start Hub";
            Load += MainForm_Load;
            pnApps.ResumeLayout(false);
            pnApps.PerformLayout();
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
    }
}
