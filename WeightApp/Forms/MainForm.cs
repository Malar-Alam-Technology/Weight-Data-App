using System.Data;
using WeightApp.Constants;
using WeightApp.Helpers;
using WeightApp.Models;
using WeightApp.Services;

namespace WeightApp.Forms
{
    /// <summary>
    /// Main application form for Garbage Truck Weight IoT System
    /// Beautiful, minimalist design with gradient theme
    /// </summary>
    public partial class MainForm : Form
    {
        // Services
        private readonly SerialPortService _serialPortService;
        private readonly LoggingService _loggingService;
        private readonly SettingsService _settingsService;
        private readonly ModemApiService _modemApiService;

        // State
        private string _currentUnit = AppConstants.UnitKilogram;
        private double _currentWeight = 0.0;
        private DataTable _logsDataTable;
        private AppSettings _appSettings;

        public MainForm()
        {
            InitializeComponent();

            // Initialize services
            _serialPortService = new SerialPortService();
            _loggingService = new LoggingService();
            _settingsService = new SettingsService();
            _modemApiService = new ModemApiService();

            // Initialize app settings
            _appSettings = new AppSettings();

            // Initialize data table
            _logsDataTable = new DataTable();
            _logsDataTable.Columns.Add("Timestamp", typeof(string));
            _logsDataTable.Columns.Add("Weight", typeof(string));
            _logsDataTable.Columns.Add("Unit", typeof(string));
            _logsDataTable.Columns.Add("Status", typeof(string));

            // Subscribe to service events
            _serialPortService.DataReceived += OnSerialDataReceived;
            _serialPortService.ErrorOccurred += OnSerialError;
            _serialPortService.InvalidDataReceived += OnInvalidDataReceived;

            // Subscribe to API events
            _modemApiService.ApiSuccess += OnApiSuccess;
            _modemApiService.ApiError += OnApiError;

            // Apply modern styling
            ApplyModernStyling();
        }

        private void ApplyModernStyling()
        {
            // Set form properties for modern look
            this.DoubleBuffered = true;

            // Load logo if exists
            LoadLogo();
        }

        private void LoadLogo()
        {
            try
            {
                string logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "parking_logo.png");
                if (File.Exists(logoPath))
                {
                    pictureBoxLogo.Image = Image.FromFile(logoPath);
                    pictureBoxLogo.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    // Create placeholder logo
                    CreatePlaceholderLogo();
                }
            }
            catch
            {
                CreatePlaceholderLogo();
            }
        }

        private void CreatePlaceholderLogo()
        {
            // Create a simple parking logo graphic
            Bitmap bmp = new Bitmap(120, 120);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                // Draw blue circle background
                using (SolidBrush brush = new SolidBrush(ColorTranslator.FromHtml("#022ff5")))
                {
                    g.FillEllipse(brush, 10, 10, 100, 100);
                }

                // Draw white "P" letter
                using (Font font = new Font("Arial", 60, FontStyle.Bold))
                using (SolidBrush textBrush = new SolidBrush(Color.White))
                {
                    StringFormat sf = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    g.DrawString("P", font, textBrush, new RectangleF(10, 10, 100, 100), sf);
                }
            }
            pictureBoxLogo.Image = bmp;
            pictureBoxLogo.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeControls();
            LoadSettings();
            LoadLogs();
            ShowStatus(AppConstants.MsgReadyToConnect, StatusHelper.StatusType.Info);
        }

        #region Initialization

        private void InitializeControls()
        {
            // Initialize Port Names
            RefreshPortList();

            // Initialize Baud Rates
            cmbBaudRate.Items.Clear();
            foreach (int baudRate in AppConstants.BaudRates)
            {
                cmbBaudRate.Items.Add(baudRate);
            }
            cmbBaudRate.SelectedIndex = 0;

            // Initialize Units
            cmbUnit.Items.Clear();
            cmbUnit.Items.AddRange(AppConstants.MeasurementUnits);
            cmbUnit.SelectedItem = _currentUnit;

            // Initialize Log Filters
            cmbLogFilter.Items.Clear();
            cmbLogFilter.Items.AddRange(AppConstants.LogFilters);
            cmbLogFilter.SelectedIndex = 0;

            // Style DataGridView for modern look
            StyleDataGridView();

            // Initialize API Settings UI
            InitializeApiSettingsUI();
        }

        private System.Windows.Forms.CheckBox chkApiEnabled;
        private System.Windows.Forms.TextBox txtDeviceId;
        private System.Windows.Forms.ComboBox cmbDirection;
        private System.Windows.Forms.Label lblApiTitle;
        private System.Windows.Forms.Label lblDeviceId;
        private System.Windows.Forms.Label lblDirection;
        private RoundedPanel cardPanelApi;

        private void InitializeApiSettingsUI()
        {
            // API Settings are now configured via constants in AppConstants.cs
            // UI panel is hidden as per configuration requirements

            // Create API Settings Card Panel (positioned in second row, center)
            cardPanelApi = new RoundedPanel
            {
                BackgroundColor = Color.White,
                BorderRadius = 20,
                HasShadow = true,
                Location = new Point(510, 390),
                Name = "cardPanelApi",
                Size = new Size(450, 220),
                Visible = false // Hide the API Settings panel
            };

            // API Title
            lblApiTitle = new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#022ff5"),
                Location = new Point(20, 20),
                Name = "lblApiTitle",
                Size = new Size(400, 35),
                TabIndex = 0,
                Text = "API Settings"
            };

            // Enable API Checkbox
            chkApiEnabled = new System.Windows.Forms.CheckBox
            {
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#022ff5"),
                Location = new Point(30, 65),
                Name = "chkApiEnabled",
                Size = new Size(200, 25),
                TabIndex = 1,
                Text = "Enable API Integration",
                UseVisualStyleBackColor = true
            };
            chkApiEnabled.CheckedChanged += ChkApiEnabled_CheckedChanged;

            // Device ID Label
            lblDeviceId = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 10F),
                Location = new Point(30, 100),
                Name = "lblDeviceId",
                Size = new Size(100, 20),
                TabIndex = 2,
                Text = "Device ID:"
            };

            // Device ID TextBox
            txtDeviceId = new System.Windows.Forms.TextBox
            {
                Font = new Font("Segoe UI", 10F),
                Location = new Point(30, 123),
                Name = "txtDeviceId",
                Size = new Size(390, 25),
                TabIndex = 3
            };
            txtDeviceId.TextChanged += TxtDeviceId_TextChanged;

            // Direction Label
            lblDirection = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 10F),
                Location = new Point(30, 155),
                Name = "lblDirection",
                Size = new Size(100, 20),
                TabIndex = 4,
                Text = "Direction:"
            };

            // Direction ComboBox
            cmbDirection = new System.Windows.Forms.ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F),
                FormattingEnabled = true,
                Location = new Point(30, 178),
                Name = "cmbDirection",
                Size = new Size(150, 25),
                TabIndex = 5
            };
            cmbDirection.Items.AddRange(new object[] { "IN", "OUT" });
            cmbDirection.SelectedIndex = 0;
            cmbDirection.SelectedIndexChanged += CmbDirection_SelectedIndexChanged;

            // Add controls to the API panel
            cardPanelApi.Controls.Add(lblApiTitle);
            cardPanelApi.Controls.Add(chkApiEnabled);
            cardPanelApi.Controls.Add(lblDeviceId);
            cardPanelApi.Controls.Add(txtDeviceId);
            cardPanelApi.Controls.Add(lblDirection);
            cardPanelApi.Controls.Add(cmbDirection);

            // Add the API panel to the main panel (but it's hidden)
            gradientMainPanel.Controls.Add(cardPanelApi);
        }

        private void ChkApiEnabled_CheckedChanged(object? sender, EventArgs e)
        {
            _appSettings.ApiEnabled = chkApiEnabled.Checked;
            txtDeviceId.Enabled = chkApiEnabled.Checked;
            cmbDirection.Enabled = chkApiEnabled.Checked;
        }

        private void TxtDeviceId_TextChanged(object? sender, EventArgs e)
        {
            _appSettings.DeviceId = txtDeviceId.Text;
        }

        private void CmbDirection_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cmbDirection.SelectedItem != null)
            {
                _appSettings.Direction = cmbDirection.SelectedItem.ToString() ?? "IN";
            }
        }

        private void StyleDataGridView()
        {
            dataGridViewLogs.BorderStyle = BorderStyle.None;
            dataGridViewLogs.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 255);
            dataGridViewLogs.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewLogs.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#022ff5");
            dataGridViewLogs.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridViewLogs.BackgroundColor = Color.White;
            dataGridViewLogs.EnableHeadersVisualStyles = false;
            dataGridViewLogs.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewLogs.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#022ff5");
            dataGridViewLogs.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewLogs.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewLogs.RowTemplate.Height = 35;
        }

        private void RefreshPortList()
        {
            cmbPortName.Items.Clear();
            string[] ports = SerialPortService.GetAvailablePorts();

            if (ports.Length > 0)
            {
                cmbPortName.Items.AddRange(ports);
                cmbPortName.SelectedIndex = 0;
            }
            else
            {
                cmbPortName.Items.Add(AppConstants.MsgNoPortsAvailable);
                cmbPortName.SelectedIndex = 0;
            }

            // Subscribe to port selection change event for auto-connect
            cmbPortName.SelectedIndexChanged += CmbPortName_SelectedIndexChanged;
            cmbBaudRate.SelectedIndexChanged += CmbBaudRate_SelectedIndexChanged;
        }

        private void CmbPortName_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // Auto-connect when a valid COM port is selected
            // If already connected, disconnect first then reconnect to new port
            if (cmbPortName.SelectedItem != null &&
                cmbPortName.SelectedItem.ToString() != AppConstants.MsgNoPortsAvailable)
            {
                try
                {
                    // Disconnect from current port if connected
                    if (_serialPortService.IsConnected)
                    {
                        _serialPortService.Disconnect();
                        _loggingService.Log(0, _currentUnit, AppConstants.StatusPortDisconnected);
                        ShowStatus("Disconnected from previous port.", StatusHelper.StatusType.Info);
                    }

                    // Connect to new port
                    string portName = cmbPortName.SelectedItem.ToString() ?? "COM1";
                    int baudRate = int.Parse(cmbBaudRate.SelectedItem?.ToString() ?? "9600");

                    var config = new SerialPortConfig(portName, baudRate);
                    _serialPortService.Connect(config);

                    ShowStatus($"Auto-connected to {portName} successfully!", StatusHelper.StatusType.Success);
                    _loggingService.Log(0, _currentUnit, AppConstants.StatusPortConnected);
                }
                catch (Exception ex)
                {
                    ShowStatus($"Error auto-connecting to port: {ex.Message}", StatusHelper.StatusType.Error);
                }
            }
        }

        private void CmbBaudRate_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // Reconnect with new baud rate if port is already selected
            if (cmbPortName.SelectedItem != null &&
                cmbPortName.SelectedItem.ToString() != AppConstants.MsgNoPortsAvailable &&
                cmbBaudRate.SelectedItem != null)
            {
                try
                {
                    // Disconnect from current port if connected
                    if (_serialPortService.IsConnected)
                    {
                        _serialPortService.Disconnect();
                        ShowStatus("Reconnecting with new baud rate...", StatusHelper.StatusType.Info);
                    }

                    // Connect with new baud rate
                    string portName = cmbPortName.SelectedItem.ToString() ?? "COM1";
                    int baudRate = int.Parse(cmbBaudRate.SelectedItem?.ToString() ?? "9600");

                    var config = new SerialPortConfig(portName, baudRate);
                    _serialPortService.Connect(config);

                    ShowStatus($"Connected to {portName} at {baudRate} baud successfully!", StatusHelper.StatusType.Success);
                    _loggingService.Log(0, _currentUnit, AppConstants.StatusPortConnected);
                }
                catch (Exception ex)
                {
                    ShowStatus($"Error reconnecting with new baud rate: {ex.Message}", StatusHelper.StatusType.Error);
                }
            }
        }

        #endregion

        #region Serial Port Connection
        // Note: Connect and Disconnect buttons removed - auto-connect is enabled via COM port selection

        #endregion

        #region Serial Port Events

        private async void OnSerialDataReceived(object? sender, double weight)
        {
            if (this.IsDisposed || this.Disposing)
                return;

            try
            {
                this.Invoke(new Action(() =>
                {
                    if (this.IsDisposed || this.Disposing)
                        return;

                    _currentWeight = weight;
                    UpdateWeightDisplay();
                    ShowStatus($"Data received: {UnitConverter.FormatWeight(weight, _currentUnit)}",
                        StatusHelper.StatusType.Success);

                    // Auto-save weight data to logs
                    try
                    {
                        _loggingService.Log(_currentWeight, _currentUnit, "Auto-saved");
                    }
                    catch (Exception ex)
                    {
                        ShowStatus($"Error auto-saving data: {ex.Message}", StatusHelper.StatusType.Warning);
                    }
                }));

                // Send to API if enabled
                if (_appSettings.ApiEnabled)
                {
                    try
                    {
                        // Convert weight to kg for API (always send in kg)
                        double weightInKg = UnitConverter.ConvertToKilograms(weight, _currentUnit);

                        await _modemApiService.SendWeightDataAsync(
                            _appSettings.DeviceId,
                            weightInKg,
                            _appSettings.Direction
                        );
                    }
                    catch (Exception ex)
                    {
                        if (!this.IsDisposed && !this.Disposing)
                        {
                            this.Invoke(new Action(() =>
                            {
                                if (!this.IsDisposed && !this.Disposing)
                                {
                                    ShowStatus($"API error: {ex.Message}", StatusHelper.StatusType.Error);
                                }
                            }));
                        }
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                // Form is disposed, ignore
            }
            catch (InvalidOperationException)
            {
                // Form handle not created or being disposed, ignore
            }
        }

        private void OnSerialError(object? sender, string errorMessage)
        {
            if (this.IsDisposed || this.Disposing)
                return;

            try
            {
                this.Invoke(new Action(() =>
                {
                    if (!this.IsDisposed && !this.Disposing)
                    {
                        ShowStatus($"Error receiving data: {errorMessage}", StatusHelper.StatusType.Error);
                    }
                }));
            }
            catch (ObjectDisposedException)
            {
                // Form is disposed, ignore
            }
            catch (InvalidOperationException)
            {
                // Form handle not created or being disposed, ignore
            }
        }

        private void OnInvalidDataReceived(object? sender, string invalidData)
        {
            if (this.IsDisposed || this.Disposing)
                return;

            try
            {
                this.Invoke(new Action(() =>
                {
                    if (!this.IsDisposed && !this.Disposing)
                    {
                        ShowStatus($"Received invalid data: {invalidData}", StatusHelper.StatusType.Warning);
                    }
                }));
            }
            catch (ObjectDisposedException)
            {
                // Form is disposed, ignore
            }
            catch (InvalidOperationException)
            {
                // Form handle not created or being disposed, ignore
            }
        }

        #endregion

        #region Modem API Events

        private void OnApiSuccess(object? sender, ApiResponse response)
        {
            if (this.IsDisposed || this.Disposing)
                return;

            try
            {
                this.Invoke(new Action(() =>
                {
                    if (!this.IsDisposed && !this.Disposing)
                    {
                        string message = $"API: {response.Message} - Weight: {response.Data?.ItemWeight} kg";
                        ShowStatus(message, StatusHelper.StatusType.Success);
                        // Note: Auto-save is now handled in OnSerialDataReceived
                    }
                }));
            }
            catch (ObjectDisposedException)
            {
                // Form is disposed, ignore
            }
            catch (InvalidOperationException)
            {
                // Form handle not created or being disposed, ignore
            }
        }

        private void OnApiError(object? sender, string errorMessage)
        {
            if (this.IsDisposed || this.Disposing)
                return;

            try
            {
                this.Invoke(new Action(() =>
                {
                    if (!this.IsDisposed && !this.Disposing)
                    {
                        ShowStatus($"API Error: {errorMessage}", StatusHelper.StatusType.Error);
                    }
                }));
            }
            catch (ObjectDisposedException)
            {
                // Form is disposed, ignore
            }
            catch (InvalidOperationException)
            {
                // Form handle not created or being disposed, ignore
            }
        }

        #endregion

        #region Weight Display

        private void UpdateWeightDisplay()
        {
            lblWeightValue.Text = UnitConverter.FormatWeight(_currentWeight, _currentUnit);
            lblConvertedWeight.Text = UnitConverter.GetConversionText(_currentWeight, _currentUnit);
        }

        // Note: Manual save button removed - auto-save is now enabled for all weight data received

        #endregion

        #region Settings

        private void LoadSettings()
        {
            try
            {
                _appSettings = _settingsService.LoadSettings();
                _currentUnit = _appSettings.MeasurementUnit;
                cmbUnit.SelectedItem = _currentUnit;

                // Load API settings from constants
                _appSettings.ApiEnabled = AppConstants.ApiAutoEnable;
                _appSettings.DeviceId = AppConstants.ApiDeviceId;
                _appSettings.Direction = AppConstants.ApiDirection;

                chkApiEnabled.Checked = _appSettings.ApiEnabled;
                txtDeviceId.Text = _appSettings.DeviceId;
                cmbDirection.SelectedItem = _appSettings.Direction;

                // Enable/disable API controls based on checkbox
                txtDeviceId.Enabled = _appSettings.ApiEnabled;
                cmbDirection.Enabled = _appSettings.ApiEnabled;

                UpdateWeightDisplay();
            }
            catch
            {
                _currentUnit = AppConstants.UnitKilogram;
                _appSettings = new AppSettings
                {
                    ApiEnabled = AppConstants.ApiAutoEnable,
                    DeviceId = AppConstants.ApiDeviceId,
                    Direction = AppConstants.ApiDirection
                };
            }
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbUnit.SelectedItem != null)
                {
                    string newUnit = cmbUnit.SelectedItem.ToString() ?? AppConstants.UnitKilogram;

                    if (!UnitConverter.IsValidUnit(newUnit))
                    {
                        MessageBox.Show("Invalid unit selected.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    _currentUnit = newUnit;
                    _appSettings.MeasurementUnit = _currentUnit;
                    _settingsService.SaveSettings(_appSettings);
                    UpdateWeightDisplay();

                    ShowStatus($"Settings saved. Unit changed to: {_currentUnit}",
                        StatusHelper.StatusType.Success);
                    MessageBox.Show($"Settings saved successfully!\n\nMeasurement Unit: {_currentUnit}",
                        "Settings Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ShowStatus($"Error saving settings: {ex.Message}", StatusHelper.StatusType.Error);
                MessageBox.Show($"Failed to save settings:\n{ex.Message}",
                    AppConstants.MsgSettingsError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Logging

        private void LoadLogs()
        {
            try
            {
                _logsDataTable.Clear();

                var logs = _loggingService.LoadLogs();
                foreach (var log in logs)
                {
                    _logsDataTable.Rows.Add(
                        log.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"),
                        log.Weight.ToString("F2"),
                        log.Unit,
                        log.Status
                    );
                }

                ApplyLogFilter();
            }
            catch (Exception ex)
            {
                ShowStatus($"Error loading logs: {ex.Message}", StatusHelper.StatusType.Error);
            }
        }

        private void ApplyLogFilter()
        {
            try
            {
                DataView dv = _logsDataTable.DefaultView;
                string filter = cmbLogFilter.SelectedItem?.ToString() ?? AppConstants.FilterAll;

                switch (filter)
                {
                    case AppConstants.FilterToday:
                        string today = DateTime.Now.ToString("yyyy-MM-dd");
                        dv.RowFilter = $"Timestamp LIKE '{today}%'";
                        break;
                    case AppConstants.FilterThisMonth:
                        string thisMonth = DateTime.Now.ToString("yyyy-MM");
                        dv.RowFilter = $"Timestamp LIKE '{thisMonth}%'";
                        break;
                    default:
                        dv.RowFilter = "";
                        break;
                }

                dataGridViewLogs.DataSource = dv;
            }
            catch (Exception ex)
            {
                ShowStatus($"Error filtering logs: {ex.Message}", StatusHelper.StatusType.Error);
            }
        }

        private void cmbLogFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyLogFilter();
        }

        private void btnRefreshLogs_Click(object sender, EventArgs e)
        {
            LoadLogs();
            ShowStatus("Logs refreshed.", StatusHelper.StatusType.Info);
        }

        private void btnClearLogs_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to clear all logs?",
                    "Confirm Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    _loggingService.ClearLogs();
                    _logsDataTable.Clear();
                    dataGridViewLogs.DataSource = _logsDataTable;

                    ShowStatus("All logs cleared.", StatusHelper.StatusType.Success);
                    MessageBox.Show("All logs have been cleared successfully.", "Logs Cleared",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ShowStatus($"Error clearing logs: {ex.Message}", StatusHelper.StatusType.Error);
                MessageBox.Show($"Failed to clear logs:\n{ex.Message}", "Clear Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportLogs_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_loggingService.HasLogs())
                {
                    MessageBox.Show("No logs available to export.", "Export Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                    DefaultExt = "csv",
                    FileName = $"weight_logs_export_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    _loggingService.ExportLogs(saveDialog.FileName);
                    ShowStatus($"Logs exported to: {saveDialog.FileName}", StatusHelper.StatusType.Success);
                    MessageBox.Show($"Logs exported successfully to:\n{saveDialog.FileName}",
                        "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ShowStatus($"Error exporting logs: {ex.Message}", StatusHelper.StatusType.Error);
                MessageBox.Show($"Failed to export logs:\n{ex.Message}", "Export Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Status Management

        private void ShowStatus(string message, StatusHelper.StatusType type)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = StatusHelper.GetStatusColor(type);
        }

        #endregion

        #region Form Events

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _serialPortService?.Dispose();
                _modemApiService?.Dispose();
            }
            catch
            {
                // Ignore errors during cleanup
            }
        }

        #endregion
    }
}
