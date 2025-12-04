using WeightApp.Helpers;

namespace WeightApp.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            gradientMainPanel = new GradientPanel();
            lblStatus = new Label();
            cardPanelLogs = new RoundedPanel();
            lblLogsTitle = new Label();
            cmbLogFilter = new ComboBox();
            dataGridViewLogs = new DataGridView();
            btnRefreshLogs = new ModernButton();
            btnClearLogs = new ModernButton();
            btnExportLogs = new ModernButton();
            cardPanelSettings = new RoundedPanel();
            lblSettingsTitle = new Label();
            cmbUnit = new ComboBox();
            btnSaveSettings = new ModernButton();
            cardPanelWeight = new RoundedPanel();
            lblWeightTitle = new Label();
            lblWeightValue = new Label();
            lblConvertedWeight = new Label();
            //btnSaveData = new ModernButton();
            cardPanelConnection = new RoundedPanel();
            lblConnectionTitle = new Label();
            cmbPortName = new ComboBox();
            cmbBaudRate = new ComboBox();
            //btnConnect = new ModernButton();
            //btnDisconnect = new ModernButton();
            lblAppTitle = new Label();
            pictureBoxLogo = new PictureBox();
            gradientMainPanel.SuspendLayout();
            cardPanelLogs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewLogs).BeginInit();
            cardPanelSettings.SuspendLayout();
            cardPanelWeight.SuspendLayout();
            cardPanelConnection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).BeginInit();
            SuspendLayout();
            // 
            // gradientMainPanel
            // 
            gradientMainPanel.BackColor = Color.Transparent;
            gradientMainPanel.BottomColor = Color.FromArgb(255, 255, 255);
            gradientMainPanel.Controls.Add(lblStatus);
            gradientMainPanel.Controls.Add(cardPanelLogs);
            gradientMainPanel.Controls.Add(cardPanelSettings);
            gradientMainPanel.Controls.Add(cardPanelWeight);
            gradientMainPanel.Controls.Add(cardPanelConnection);
            gradientMainPanel.Controls.Add(lblAppTitle);
            gradientMainPanel.Controls.Add(pictureBoxLogo);
            gradientMainPanel.Dock = DockStyle.Fill;
            gradientMainPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            gradientMainPanel.Location = new Point(0, 0);
            gradientMainPanel.Name = "gradientMainPanel";
            gradientMainPanel.Size = new Size(1400, 780);
            gradientMainPanel.TabIndex = 0;
            gradientMainPanel.TopColor = Color.FromArgb(2, 47, 245);
            // 
            // lblStatus
            //
            lblStatus.BackColor = Color.Transparent;
            lblStatus.Font = new Font("Segoe UI", 10F);
            lblStatus.ForeColor = Color.White;
            lblStatus.Location = new Point(30, 730);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(1340, 30);
            lblStatus.TabIndex = 6;
            lblStatus.Text = "Ready";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cardPanelLogs
            //
            cardPanelLogs.BackColor = Color.Transparent;
            cardPanelLogs.BackgroundColor = Color.White;
            cardPanelLogs.BorderRadius = 20;
            cardPanelLogs.Controls.Add(lblLogsTitle);
            cardPanelLogs.Controls.Add(cmbLogFilter);
            cardPanelLogs.Controls.Add(dataGridViewLogs);
            cardPanelLogs.Controls.Add(btnRefreshLogs);
            cardPanelLogs.Controls.Add(btnClearLogs);
            cardPanelLogs.Controls.Add(btnExportLogs);
            cardPanelLogs.HasShadow = true;
            cardPanelLogs.Location = new Point(30, 370);
            cardPanelLogs.Name = "cardPanelLogs";
            cardPanelLogs.Size = new Size(1340, 340);
            cardPanelLogs.TabIndex = 5;
            // 
            // lblLogsTitle
            // 
            lblLogsTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblLogsTitle.ForeColor = Color.FromArgb(2, 47, 245);
            lblLogsTitle.Location = new Point(20, 20);
            lblLogsTitle.Name = "lblLogsTitle";
            lblLogsTitle.Size = new Size(300, 35);
            lblLogsTitle.TabIndex = 0;
            lblLogsTitle.Text = "Activity Logs";
            // 
            // cmbLogFilter
            // 
            cmbLogFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLogFilter.Font = new Font("Segoe UI", 10F);
            cmbLogFilter.FormattingEnabled = true;
            cmbLogFilter.Location = new Point(1130, 25);
            cmbLogFilter.Name = "cmbLogFilter";
            cmbLogFilter.Size = new Size(180, 25);
            cmbLogFilter.TabIndex = 1;
            cmbLogFilter.SelectedIndexChanged += cmbLogFilter_SelectedIndexChanged;
            // 
            // dataGridViewLogs
            // 
            dataGridViewLogs.AllowUserToAddRows = false;
            dataGridViewLogs.AllowUserToDeleteRows = false;
            dataGridViewLogs.BackgroundColor = Color.White;
            dataGridViewLogs.BorderStyle = BorderStyle.None;
            dataGridViewLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewLogs.Location = new Point(20, 70);
            dataGridViewLogs.Name = "dataGridViewLogs";
            dataGridViewLogs.ReadOnly = true;
            dataGridViewLogs.RowHeadersVisible = false;
            dataGridViewLogs.Size = new Size(1290, 200);
            dataGridViewLogs.TabIndex = 2;
            // 
            // btnRefreshLogs
            // 
            btnRefreshLogs.BorderRadius = 8;
            btnRefreshLogs.Cursor = Cursors.Hand;
            btnRefreshLogs.FlatAppearance.BorderSize = 0;
            btnRefreshLogs.FlatStyle = FlatStyle.Flat;
            btnRefreshLogs.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRefreshLogs.ForeColor = Color.White;
            btnRefreshLogs.HoverColor = Color.FromArgb(3, 65, 255);
            btnRefreshLogs.Location = new Point(920, 285);
            btnRefreshLogs.Name = "btnRefreshLogs";
            btnRefreshLogs.NormalColor = Color.FromArgb(2, 47, 245);
            btnRefreshLogs.Size = new Size(130, 35);
            btnRefreshLogs.TabIndex = 3;
            btnRefreshLogs.Text = "Refresh";
            btnRefreshLogs.UseVisualStyleBackColor = true;
            btnRefreshLogs.Click += btnRefreshLogs_Click;
            // 
            // btnClearLogs
            // 
            btnClearLogs.BorderRadius = 8;
            btnClearLogs.Cursor = Cursors.Hand;
            btnClearLogs.FlatAppearance.BorderSize = 0;
            btnClearLogs.FlatStyle = FlatStyle.Flat;
            btnClearLogs.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClearLogs.ForeColor = Color.White;
            btnClearLogs.HoverColor = Color.FromArgb(255, 193, 7);
            btnClearLogs.Location = new Point(1070, 285);
            btnClearLogs.Name = "btnClearLogs";
            btnClearLogs.NormalColor = Color.FromArgb(255, 160, 0);
            btnClearLogs.Size = new Size(110, 35);
            btnClearLogs.TabIndex = 4;
            btnClearLogs.Text = "Clear";
            btnClearLogs.UseVisualStyleBackColor = true;
            btnClearLogs.Click += btnClearLogs_Click;
            // 
            // btnExportLogs
            // 
            btnExportLogs.BorderRadius = 8;
            btnExportLogs.Cursor = Cursors.Hand;
            btnExportLogs.FlatAppearance.BorderSize = 0;
            btnExportLogs.FlatStyle = FlatStyle.Flat;
            btnExportLogs.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnExportLogs.ForeColor = Color.White;
            btnExportLogs.HoverColor = Color.FromArgb(40, 167, 69);
            btnExportLogs.Location = new Point(1200, 285);
            btnExportLogs.Name = "btnExportLogs";
            btnExportLogs.NormalColor = Color.FromArgb(25, 135, 84);
            btnExportLogs.Size = new Size(110, 35);
            btnExportLogs.TabIndex = 5;
            btnExportLogs.Text = "Export";
            btnExportLogs.UseVisualStyleBackColor = true;
            btnExportLogs.Click += btnExportLogs_Click;
            // 
            // cardPanelSettings
            // 
            cardPanelSettings.BackColor = Color.Transparent;
            cardPanelSettings.BackgroundColor = Color.White;
            cardPanelSettings.BorderRadius = 20;
            cardPanelSettings.Controls.Add(lblSettingsTitle);
            cardPanelSettings.Controls.Add(cmbUnit);
            cardPanelSettings.Controls.Add(btnSaveSettings);
            cardPanelSettings.HasShadow = true;
            cardPanelSettings.Location = new Point(990, 150);
            cardPanelSettings.Name = "cardPanelSettings";
            cardPanelSettings.Size = new Size(380, 200);
            cardPanelSettings.TabIndex = 4;
            // 
            // lblSettingsTitle
            // 
            lblSettingsTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblSettingsTitle.ForeColor = Color.FromArgb(2, 47, 245);
            lblSettingsTitle.Location = new Point(20, 20);
            lblSettingsTitle.Name = "lblSettingsTitle";
            lblSettingsTitle.Size = new Size(340, 35);
            lblSettingsTitle.TabIndex = 0;
            lblSettingsTitle.Text = "Settings";
            // 
            // cmbUnit
            // 
            cmbUnit.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUnit.Font = new Font("Segoe UI", 12F);
            cmbUnit.FormattingEnabled = true;
            cmbUnit.Location = new Point(80, 90);
            cmbUnit.Name = "cmbUnit";
            cmbUnit.Size = new Size(220, 29);
            cmbUnit.TabIndex = 1;
            // 
            // btnSaveSettings
            // 
            btnSaveSettings.BorderRadius = 10;
            btnSaveSettings.Cursor = Cursors.Hand;
            btnSaveSettings.FlatAppearance.BorderSize = 0;
            btnSaveSettings.FlatStyle = FlatStyle.Flat;
            btnSaveSettings.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnSaveSettings.ForeColor = Color.White;
            btnSaveSettings.HoverColor = Color.FromArgb(3, 65, 255);
            btnSaveSettings.Location = new Point(80, 145);
            btnSaveSettings.Name = "btnSaveSettings";
            btnSaveSettings.NormalColor = Color.FromArgb(2, 47, 245);
            btnSaveSettings.Size = new Size(220, 45);
            btnSaveSettings.TabIndex = 2;
            btnSaveSettings.Text = "Save Settings";
            btnSaveSettings.UseVisualStyleBackColor = true;
            btnSaveSettings.Click += btnSaveSettings_Click;
            // 
            // cardPanelWeight
            // 
            cardPanelWeight.BackColor = Color.Transparent;
            cardPanelWeight.BackgroundColor = Color.White;
            cardPanelWeight.BorderRadius = 20;
            cardPanelWeight.Controls.Add(lblWeightTitle);
            cardPanelWeight.Controls.Add(lblWeightValue);
            cardPanelWeight.Controls.Add(lblConvertedWeight);
            cardPanelWeight.HasShadow = true;
            cardPanelWeight.Location = new Point(510, 150);
            cardPanelWeight.Name = "cardPanelWeight";
            cardPanelWeight.Size = new Size(450, 150);
            cardPanelWeight.TabIndex = 3;
            // 
            // lblWeightTitle
            // 
            lblWeightTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblWeightTitle.ForeColor = Color.FromArgb(2, 47, 245);
            lblWeightTitle.Location = new Point(20, 20);
            lblWeightTitle.Name = "lblWeightTitle";
            lblWeightTitle.Size = new Size(400, 35);
            lblWeightTitle.TabIndex = 0;
            lblWeightTitle.Text = "Current Weight";
            // 
            // lblWeightValue
            // 
            lblWeightValue.Font = new Font("Segoe UI", 32F, FontStyle.Bold);
            lblWeightValue.ForeColor = Color.FromArgb(2, 47, 245);
            lblWeightValue.Location = new Point(20, 60);
            lblWeightValue.Name = "lblWeightValue";
            lblWeightValue.Size = new Size(410, 60);
            lblWeightValue.TabIndex = 1;
            lblWeightValue.Text = "0.00 kg";
            lblWeightValue.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblConvertedWeight
            // 
            lblConvertedWeight.Font = new Font("Segoe UI", 10F);
            lblConvertedWeight.ForeColor = Color.Gray;
            lblConvertedWeight.Location = new Point(20, 120);
            lblConvertedWeight.Name = "lblConvertedWeight";
            lblConvertedWeight.Size = new Size(410, 25);
            lblConvertedWeight.TabIndex = 2;
            lblConvertedWeight.Text = "0.00 g | 0.0000 ton";
            lblConvertedWeight.TextAlign = ContentAlignment.MiddleCenter;
            //
            // cardPanelConnection
            // 
            cardPanelConnection.BackColor = Color.Transparent;
            cardPanelConnection.BackgroundColor = Color.White;
            cardPanelConnection.BorderRadius = 20;
            cardPanelConnection.Controls.Add(lblConnectionTitle);
            cardPanelConnection.Controls.Add(cmbPortName);
            cardPanelConnection.Controls.Add(cmbBaudRate);
            cardPanelConnection.HasShadow = true;
            cardPanelConnection.Location = new Point(30, 150);
            cardPanelConnection.Name = "cardPanelConnection";
            cardPanelConnection.Size = new Size(450, 150);
            cardPanelConnection.TabIndex = 2;
            // 
            // lblConnectionTitle
            // 
            lblConnectionTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblConnectionTitle.ForeColor = Color.FromArgb(2, 47, 245);
            lblConnectionTitle.Location = new Point(20, 20);
            lblConnectionTitle.Name = "lblConnectionTitle";
            lblConnectionTitle.Size = new Size(400, 35);
            lblConnectionTitle.TabIndex = 0;
            lblConnectionTitle.Text = "Port Connection";
            // 
            // cmbPortName
            // 
            cmbPortName.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPortName.Font = new Font("Segoe UI", 11F);
            cmbPortName.FormattingEnabled = true;
            cmbPortName.Location = new Point(30, 70);
            cmbPortName.Name = "cmbPortName";
            cmbPortName.Size = new Size(180, 28);
            cmbPortName.TabIndex = 1;
            // 
            // cmbBaudRate
            // 
            cmbBaudRate.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBaudRate.Font = new Font("Segoe UI", 11F);
            cmbBaudRate.FormattingEnabled = true;
            cmbBaudRate.Location = new Point(240, 70);
            cmbBaudRate.Name = "cmbBaudRate";
            cmbBaudRate.Size = new Size(180, 28);
            cmbBaudRate.TabIndex = 2;
            //
            // lblAppTitle
            // 
            lblAppTitle.BackColor = Color.Transparent;
            lblAppTitle.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblAppTitle.ForeColor = Color.White;
            lblAppTitle.Location = new Point(150, 45);
            lblAppTitle.Name = "lblAppTitle";
            lblAppTitle.Size = new Size(800, 50);
            lblAppTitle.TabIndex = 1;
            lblAppTitle.Text = "Garbage Truck Weight System";
            lblAppTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pictureBoxLogo
            // 
            pictureBoxLogo.BackColor = Color.Transparent;
            pictureBoxLogo.Location = new Point(30, 20);
            pictureBoxLogo.Name = "pictureBoxLogo";
            pictureBoxLogo.Size = new Size(100, 100);
            pictureBoxLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxLogo.TabIndex = 0;
            pictureBoxLogo.TabStop = false;
            // 
            // MainForm
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1400, 780);
            Controls.Add(gradientMainPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Garbage Truck Weight IoT System";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            gradientMainPanel.ResumeLayout(false);
            cardPanelLogs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewLogs).EndInit();
            cardPanelSettings.ResumeLayout(false);
            cardPanelWeight.ResumeLayout(false);
            cardPanelConnection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GradientPanel gradientMainPanel;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Label lblAppTitle;
        private RoundedPanel cardPanelConnection;
        private System.Windows.Forms.Label lblConnectionTitle;
        private System.Windows.Forms.ComboBox cmbPortName;
        private System.Windows.Forms.ComboBox cmbBaudRate;
        private RoundedPanel cardPanelWeight;
        private System.Windows.Forms.Label lblWeightTitle;
        private System.Windows.Forms.Label lblWeightValue;
        private System.Windows.Forms.Label lblConvertedWeight;
        private RoundedPanel cardPanelSettings;
        private System.Windows.Forms.Label lblSettingsTitle;
        private System.Windows.Forms.ComboBox cmbUnit;
        private ModernButton btnSaveSettings;
        private RoundedPanel cardPanelLogs;
        private System.Windows.Forms.Label lblLogsTitle;
        private System.Windows.Forms.ComboBox cmbLogFilter;
        private System.Windows.Forms.DataGridView dataGridViewLogs;
        private ModernButton btnRefreshLogs;
        private ModernButton btnClearLogs;
        private ModernButton btnExportLogs;
        private System.Windows.Forms.Label lblStatus;
    }
}
