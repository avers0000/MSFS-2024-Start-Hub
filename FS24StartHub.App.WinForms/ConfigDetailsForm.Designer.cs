using FS24StartHub.App.WinForms.Controls;

namespace FS24StartHub.App.WinForms
{
    partial class ConfigDetailsForm
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
            tlpConfigDetails = new HorizontalLineTableLayoutPanel();
            lblName = new Label();
            txtName = new CenteredTextBox();
            lblDescription = new Label();
            txtDescription = new TextBox();
            lblCreated = new Label();
            lblCreatedValue = new Label();
            lblLastUsed = new Label();
            lblLastUsedValue = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            lblPreviewHeader = new Label();
            pnlPreview = new Panel();
            rtbPreview = new RichTextBox();
            tlpConfigDetails.SuspendLayout();
            pnlPreview.SuspendLayout();
            SuspendLayout();
            // 
            // tlpConfigDetails
            // 
            tlpConfigDetails.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tlpConfigDetails.BackColor = Color.FromArgb(38, 42, 46);
            tlpConfigDetails.ColumnCount = 7;
            tlpConfigDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 5F));
            tlpConfigDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tlpConfigDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            tlpConfigDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            tlpConfigDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            tlpConfigDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpConfigDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 5F));
            tlpConfigDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tlpConfigDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tlpConfigDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tlpConfigDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tlpConfigDetails.Controls.Add(lblName, 1, 1);
            tlpConfigDetails.Controls.Add(txtName, 2, 1);
            tlpConfigDetails.Controls.Add(lblDescription, 1, 2);
            tlpConfigDetails.Controls.Add(txtDescription, 2, 2);
            tlpConfigDetails.Controls.Add(lblCreated, 1, 3);
            tlpConfigDetails.Controls.Add(lblCreatedValue, 2, 3);
            tlpConfigDetails.Controls.Add(lblLastUsed, 3, 3);
            tlpConfigDetails.Controls.Add(lblLastUsedValue, 4, 3);
            tlpConfigDetails.LineColor = Color.DimGray;
            tlpConfigDetails.LineThickness = 2;
            tlpConfigDetails.Location = new Point(20, 20);
            tlpConfigDetails.Name = "tlpConfigDetails";
            tlpConfigDetails.RowCount = 5;
            tlpConfigDetails.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            tlpConfigDetails.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tlpConfigDetails.RowStyles.Add(new RowStyle(SizeType.Absolute, 62F));
            tlpConfigDetails.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tlpConfigDetails.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            tlpConfigDetails.Size = new Size(592, 140);
            tlpConfigDetails.TabIndex = 11;
            // 
            // lblName
            // 
            lblName.Anchor = AnchorStyles.Left;
            lblName.AutoSize = true;
            lblName.Location = new Point(5, 12);
            lblName.Margin = new Padding(0, 0, 3, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(46, 19);
            lblName.TabIndex = 3;
            lblName.Text = "Name";
            // 
            // txtName
            // 
            txtName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtName.BackColor = Color.Black;
            tlpConfigDetails.SetColumnSpan(txtName, 4);
            txtName.Font = new Font("Segoe UI", 10F);
            txtName.ForeColor = Color.White;
            txtName.Location = new Point(128, 10);
            txtName.MaxLength = 100;
            txtName.Name = "txtName";
            txtName.Padding = new Padding(5, 0, 5, 0);
            txtName.PasswordChar = '\0';
            txtName.ReadOnly = false;
            txtName.Size = new Size(456, 24);
            txtName.TabIndex = 4;
            txtName.TextAlign = HorizontalAlignment.Left;
            // 
            // lblDescription
            // 
            lblDescription.Anchor = AnchorStyles.Left;
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(5, 60);
            lblDescription.Margin = new Padding(0, 0, 3, 0);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(81, 19);
            lblDescription.TabIndex = 5;
            lblDescription.Text = "Description";
            // 
            // txtDescription
            // 
            txtDescription.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDescription.BackColor = Color.Black;
            txtDescription.BorderStyle = BorderStyle.None;
            tlpConfigDetails.SetColumnSpan(txtDescription, 4);
            txtDescription.Font = new Font("Segoe UI", 10F);
            txtDescription.ForeColor = Color.White;
            txtDescription.Location = new Point(128, 47);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(456, 46);
            txtDescription.TabIndex = 6;
            // 
            // lblCreated
            // 
            lblCreated.Anchor = AnchorStyles.Left;
            lblCreated.AutoSize = true;
            lblCreated.Location = new Point(5, 108);
            lblCreated.Margin = new Padding(0, 0, 3, 0);
            lblCreated.Name = "lblCreated";
            lblCreated.Size = new Size(90, 19);
            lblCreated.TabIndex = 7;
            lblCreated.Text = "Created Date";
            // 
            // lblCreatedValue
            // 
            lblCreatedValue.Anchor = AnchorStyles.Left;
            lblCreatedValue.AutoSize = true;
            lblCreatedValue.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCreatedValue.ForeColor = Color.White;
            lblCreatedValue.Location = new Point(125, 109);
            lblCreatedValue.Margin = new Padding(0, 0, 3, 0);
            lblCreatedValue.Name = "lblCreatedValue";
            lblCreatedValue.Size = new Size(0, 17);
            lblCreatedValue.TabIndex = 8;
            // 
            // lblLastUsed
            // 
            lblLastUsed.Anchor = AnchorStyles.Left;
            lblLastUsed.AutoSize = true;
            lblLastUsed.Location = new Point(293, 108);
            lblLastUsed.Margin = new Padding(8, 0, 3, 0);
            lblLastUsed.Name = "lblLastUsed";
            lblLastUsed.Size = new Size(67, 19);
            lblLastUsed.TabIndex = 9;
            lblLastUsed.Text = "Last used";
            // 
            // lblLastUsedValue
            // 
            lblLastUsedValue.Anchor = AnchorStyles.Left;
            lblLastUsedValue.AutoSize = true;
            lblLastUsedValue.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLastUsedValue.ForeColor = Color.White;
            lblLastUsedValue.Location = new Point(375, 109);
            lblLastUsedValue.Margin = new Padding(0, 0, 3, 0);
            lblLastUsedValue.Name = "lblLastUsedValue";
            lblLastUsedValue.Size = new Size(0, 17);
            lblLastUsedValue.TabIndex = 10;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSave.BackColor = Color.Black;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI Semibold", 12F);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(420, 172);
            btnSave.Margin = new Padding(0);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(96, 30);
            btnSave.TabIndex = 0;
            btnSave.Tag = "Accent";
            btnSave.Text = "OK";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancel.BackColor = Color.Black;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI Semibold", 12F);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(516, 172);
            btnCancel.Margin = new Padding(0);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(96, 30);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // lblPreviewHeader
            // 
            lblPreviewHeader.AutoSize = true;
            lblPreviewHeader.Font = new Font("Segoe UI Semibold", 10F);
            lblPreviewHeader.ForeColor = Color.White;
            lblPreviewHeader.Location = new Point(20, 202);
            lblPreviewHeader.Name = "lblPreviewHeader";
            lblPreviewHeader.Size = new Size(128, 19);
            lblPreviewHeader.TabIndex = 10;
            lblPreviewHeader.Text = "Config file preview";
            // 
            // pnlPreview
            // 
            pnlPreview.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlPreview.BackColor = Color.FromArgb(13, 17, 20);
            pnlPreview.Controls.Add(rtbPreview);
            pnlPreview.Location = new Point(20, 224);
            pnlPreview.Name = "pnlPreview";
            pnlPreview.Padding = new Padding(6);
            pnlPreview.Size = new Size(591, 256);
            pnlPreview.TabIndex = 11;
            // 
            // rtbPreview
            // 
            rtbPreview.BackColor = Color.FromArgb(13, 17, 20);
            rtbPreview.BorderStyle = BorderStyle.None;
            rtbPreview.Dock = DockStyle.Fill;
            rtbPreview.Font = new Font("Consolas", 9.5F);
            rtbPreview.ForeColor = Color.FromArgb(200, 210, 220);
            rtbPreview.Location = new Point(6, 6);
            rtbPreview.Name = "rtbPreview";
            rtbPreview.ReadOnly = true;
            rtbPreview.Size = new Size(579, 244);
            rtbPreview.TabIndex = 2;
            rtbPreview.TabStop = false;
            rtbPreview.Text = "";
            // 
            // ConfigDetailsForm
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(47, 52, 57);
            CancelButton = btnCancel;
            ClientSize = new Size(631, 500);
            Controls.Add(tlpConfigDetails);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            Controls.Add(lblPreviewHeader);
            Controls.Add(pnlPreview);
            Font = new Font("Segoe UI Semibold", 10F);
            ForeColor = Color.White;
            MinimizeBox = false;
            MinimumSize = new Size(592, 440);
            Name = "ConfigDetailsForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Config details";
            Load += ConfigDetailsForm_Load;
            tlpConfigDetails.ResumeLayout(false);
            tlpConfigDetails.PerformLayout();
            pnlPreview.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private HorizontalLineTableLayoutPanel tlpConfigDetails;
        private Label lblName;
        private CenteredTextBox txtName;
        private Label lblDescription;
        private TextBox txtDescription;
        private Label lblCreated;
        private Label lblCreatedValue;
        private Label lblLastUsed;
        private Label lblLastUsedValue;
        private Button btnSave;
        private Button btnCancel;
        private Label lblPreviewHeader;
        private Panel pnlPreview;
        private RichTextBox rtbPreview;
    }
}