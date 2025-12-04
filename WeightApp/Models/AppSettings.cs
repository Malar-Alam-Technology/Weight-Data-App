namespace WeightApp.Models
{
    /// <summary>
    /// Application settings model
    /// </summary>
    public class AppSettings
    {
        public string MeasurementUnit { get; set; }
        public string LastUsedPort { get; set; }
        public int LastUsedBaudRate { get; set; }

        // API Settings
        public bool ApiEnabled { get; set; }
        public string DeviceId { get; set; }
        public string Direction { get; set; }

        public AppSettings()
        {
            MeasurementUnit = "kg";
            LastUsedPort = string.Empty;
            LastUsedBaudRate = 9600;

            // Default API settings
            ApiEnabled = false;
            DeviceId = "eb69df27-83d3-4c59-9185-03586be56d29"; // Default UUID
            Direction = "IN";
        }
    }
}
