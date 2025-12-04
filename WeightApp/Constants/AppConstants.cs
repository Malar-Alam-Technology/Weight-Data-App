namespace WeightApp.Constants
{
    /// <summary>
    /// Application-wide constants
    /// </summary>
    public static class AppConstants
    {
        // File paths
        public const string SettingsFilePath = "settings.txt";
        public const string LogFilePath = "weight_logs.csv";
        public const string CsvHeader = "Timestamp,Weight,Unit,Status";

        // Measurement units
        public const string UnitKilogram = "kg";
        public const string UnitGram = "g";
        public const string UnitTon = "ton";

        // Log statuses
        public const string StatusPortConnected = "Port Connected";
        public const string StatusPortDisconnected = "Port Disconnected";
        public const string StatusManualSave = "Manual Save";
        public const string StatusDataReceived = "Data Received";

        // Log filters
        public const string FilterAll = "All";
        public const string FilterToday = "Today";
        public const string FilterThisMonth = "This Month";

        // Baud rates
        public static readonly int[] BaudRates = { 9600, 19200, 38400, 57600, 115200 };

        // Units
        public static readonly string[] MeasurementUnits = { UnitKilogram, UnitGram, UnitTon };

        // Filters
        public static readonly string[] LogFilters = { FilterAll, FilterToday, FilterThisMonth };

        // UI Messages
        public const string MsgReadyToConnect = "Application started. Ready to connect.";
        public const string MsgNoPortsAvailable = "No ports available";
        public const string MsgSelectValidPort = "Please select a valid COM port.";
        public const string MsgConnectionError = "Connection Error";
        public const string MsgDisconnectionError = "Disconnection Error";
        public const string MsgSaveError = "Save Error";
        public const string MsgSettingsError = "Settings Error";

        // API Configuration
        public const string ApiUrl = "http://220.158.208.202/semaling/registerweight/";
        public const string ApiDeviceId = "eb69df27-83d3-4c59-9185-03586be56d29";
        public const string ApiDirection = "IN";
        public const bool ApiAutoEnable = true;
    }
}
