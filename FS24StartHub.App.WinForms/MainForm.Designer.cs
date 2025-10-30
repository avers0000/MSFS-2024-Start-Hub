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
            gbApps = new GroupBox();
            btnAppsEdit = new Button();
            btnAppsAdd = new Button();
            btnAppsRemove = new Button();
            btnAppsReload = new Button();
            btnAppsMoveDown = new Button();
            btnAppsMoveUp = new Button();
            clbApps = new FS24StartHub.App.WinForms.Controls.CustomCheckedListBox();
            btnSave = new Button();
            chbKeepOpen = new CheckBox();
            gbApps.SuspendLayout();
            SuspendLayout();
            // 
            // btnStart
            // 
            btnStart.Font = new Font("Segoe UI", 28F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnStart.Location = new Point(12, 218);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(320, 111);
            btnStart.TabIndex = 0;
            btnStart.Text = "START";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnExit
            // 
            btnExit.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnExit.Location = new Point(338, 289);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(114, 40);
            btnExit.TabIndex = 1;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // gbApps
            // 
            gbApps.Controls.Add(btnAppsEdit);
            gbApps.Controls.Add(btnAppsAdd);
            gbApps.Controls.Add(btnAppsRemove);
            gbApps.Controls.Add(btnAppsReload);
            gbApps.Controls.Add(btnAppsMoveDown);
            gbApps.Controls.Add(btnAppsMoveUp);
            gbApps.Controls.Add(clbApps);
            gbApps.Location = new Point(12, 12);
            gbApps.Name = "gbApps";
            gbApps.Padding = new Padding(6);
            gbApps.Size = new Size(440, 200);
            gbApps.TabIndex = 2;
            gbApps.TabStop = false;
            gbApps.Text = "Startup Applications and Scripts";
            // 
            // btnAppsEdit
            // 
            btnAppsEdit.Location = new Point(356, 54);
            btnAppsEdit.Name = "btnAppsEdit";
            btnAppsEdit.Size = new Size(75, 23);
            btnAppsEdit.TabIndex = 6;
            btnAppsEdit.Text = "Edit...";
            btnAppsEdit.UseVisualStyleBackColor = true;
            btnAppsEdit.Click += btnAppsEdit_Click;
            // 
            // btnAppsAdd
            // 
            btnAppsAdd.Location = new Point(356, 25);
            btnAppsAdd.Name = "btnAppsAdd";
            btnAppsAdd.Size = new Size(75, 23);
            btnAppsAdd.TabIndex = 5;
            btnAppsAdd.Text = "Add...";
            btnAppsAdd.UseVisualStyleBackColor = true;
            btnAppsAdd.Click += btnAppsAdd_Click;
            // 
            // btnAppsRemove
            // 
            btnAppsRemove.Location = new Point(356, 83);
            btnAppsRemove.Name = "btnAppsRemove";
            btnAppsRemove.Size = new Size(75, 23);
            btnAppsRemove.TabIndex = 4;
            btnAppsRemove.Text = "Remove";
            btnAppsRemove.UseVisualStyleBackColor = true;
            btnAppsRemove.Click += btnAppsRemove_Click;
            // 
            // btnAppsReload
            // 
            btnAppsReload.Location = new Point(356, 110);
            btnAppsReload.Name = "btnAppsReload";
            btnAppsReload.Size = new Size(75, 23);
            btnAppsReload.TabIndex = 3;
            btnAppsReload.Text = "Reload";
            btnAppsReload.UseVisualStyleBackColor = true;
            btnAppsReload.Click += btnAppsReload_Click;
            // 
            // btnAppsMoveDown
            // 
            btnAppsMoveDown.Location = new Point(356, 168);
            btnAppsMoveDown.Name = "btnAppsMoveDown";
            btnAppsMoveDown.Size = new Size(75, 23);
            btnAppsMoveDown.TabIndex = 2;
            btnAppsMoveDown.Text = "Down";
            btnAppsMoveDown.UseVisualStyleBackColor = true;
            btnAppsMoveDown.Click += btnAppsMoveDown_Click;
            // 
            // btnAppsMoveUp
            // 
            btnAppsMoveUp.Location = new Point(356, 139);
            btnAppsMoveUp.Name = "btnAppsMoveUp";
            btnAppsMoveUp.Size = new Size(75, 23);
            btnAppsMoveUp.TabIndex = 1;
            btnAppsMoveUp.Text = "Up";
            btnAppsMoveUp.UseVisualStyleBackColor = true;
            btnAppsMoveUp.Click += btnAppsMoveUp_Click;
            // 
            // clbApps
            // 
            clbApps.FormattingEnabled = true;
            clbApps.Location = new Point(9, 25);
            clbApps.Name = "clbApps";
            clbApps.Size = new Size(341, 166);
            clbApps.TabIndex = 0;
            clbApps.ItemCheck += clbApps_ItemCheck;
            clbApps.SelectedIndexChanged += clbApps_SelectedIndexChanged;
            // 
            // btnSave
            // 
            btnSave.Enabled = false;
            btnSave.Font = new Font("Segoe UI", 14.25F);
            btnSave.Location = new Point(338, 243);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(114, 40);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // chbKeepOpen
            // 
            chbKeepOpen.AutoSize = true;
            chbKeepOpen.Location = new Point(338, 218);
            chbKeepOpen.Name = "chbKeepOpen";
            chbKeepOpen.Size = new Size(82, 19);
            chbKeepOpen.TabIndex = 4;
            chbKeepOpen.Text = "Keep open";
            chbKeepOpen.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(464, 339);
            Controls.Add(chbKeepOpen);
            Controls.Add(btnSave);
            Controls.Add(gbApps);
            Controls.Add(btnExit);
            Controls.Add(btnStart);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MSFS 2024 Start Hub";
            gbApps.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnStart;
        private Button btnExit;
        private GroupBox gbApps;
        private Controls.CustomCheckedListBox clbApps;
        private Button btnAppsMoveUp;
        private Button btnAppsMoveDown;
        private Button btnAppsReload;
        private Button btnAppsRemove;
        private Button btnAppsAdd;
        private Button btnAppsEdit;
        private Button btnSave;
        private CheckBox chbKeepOpen;
    }
}
