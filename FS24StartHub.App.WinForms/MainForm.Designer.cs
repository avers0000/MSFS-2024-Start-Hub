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
            btnStart = new Button();
            btnExit = new Button();
            gbApps = new GroupBox();
            btnAppsReload = new Button();
            btnAppsMoveDown = new Button();
            btnAppsMoveUp = new Button();
            clbApps = new FS24StartHub.App.WinForms.Controls.CustomCheckedListBox();
            btnAppsRemove = new Button();
            gbApps.SuspendLayout();
            SuspendLayout();
            // 
            // btnStart
            // 
            btnStart.Font = new Font("Segoe UI", 28F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnStart.Location = new Point(63, 218);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(320, 141);
            btnStart.TabIndex = 0;
            btnStart.Text = "START";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnExit
            // 
            btnExit.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnExit.Location = new Point(63, 365);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(320, 40);
            btnExit.TabIndex = 1;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // gbApps
            // 
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
            // btnAppsRemove
            // 
            btnAppsRemove.Location = new Point(356, 81);
            btnAppsRemove.Name = "btnAppsRemove";
            btnAppsRemove.Size = new Size(75, 23);
            btnAppsRemove.TabIndex = 4;
            btnAppsRemove.Text = "Remove";
            btnAppsRemove.UseVisualStyleBackColor = true;
            btnAppsRemove.Click += btnAppsRemove_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(464, 415);
            Controls.Add(gbApps);
            Controls.Add(btnExit);
            Controls.Add(btnStart);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MSFS 2024 Start Hub";
            gbApps.ResumeLayout(false);
            ResumeLayout(false);
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
    }
}
