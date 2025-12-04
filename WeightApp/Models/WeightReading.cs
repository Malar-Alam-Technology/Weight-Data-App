namespace WeightApp.Models
{
    /// <summary>
    /// Represents a single weight reading with timestamp and status
    /// </summary>
    public class WeightReading
    {
        public DateTime Timestamp { get; set; }
        public double Weight { get; set; }
        public string Unit { get; set; }
        public string Status { get; set; }

        public WeightReading()
        {
            Timestamp = DateTime.Now;
            Weight = 0.0;
            Unit = "kg";
            Status = string.Empty;
        }

        public WeightReading(double weight, string unit, string status)
        {
            Timestamp = DateTime.Now;
            Weight = weight;
            Unit = unit;
            Status = status;
        }

        /// <summary>
        /// Converts the reading to CSV format
        /// </summary>
        public string ToCsvString()
        {
            return $"{Timestamp:yyyy-MM-dd HH:mm:ss},{Weight:F2},{Unit},{Status}";
        }

        /// <summary>
        /// Creates a WeightReading from CSV line
        /// </summary>
        public static WeightReading? FromCsvString(string csvLine)
        {
            if (string.IsNullOrWhiteSpace(csvLine))
                return null;

            string[] parts = csvLine.Split(',');
            if (parts.Length < 4)
                return null;

            try
            {
                return new WeightReading
                {
                    Timestamp = DateTime.Parse(parts[0]),
                    Weight = double.Parse(parts[1]),
                    Unit = parts[2],
                    Status = parts[3]
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
