using WeightApp.Constants;
using WeightApp.Models;

namespace WeightApp.Services
{
    /// <summary>
    /// Service for managing activity logs
    /// </summary>
    public class LoggingService
    {
        private readonly string _logFilePath;

        public LoggingService(string? logFilePath = null)
        {
            _logFilePath = logFilePath ?? AppConstants.LogFilePath;
        }

        /// <summary>
        /// Log a weight reading to file
        /// </summary>
        public void LogReading(WeightReading reading)
        {
            try
            {
                if (!File.Exists(_logFilePath))
                {
                    File.WriteAllText(_logFilePath, AppConstants.CsvHeader + "\n");
                }

                File.AppendAllText(_logFilePath, reading.ToCsvString() + "\n");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to log reading: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Log weight data with specific parameters
        /// </summary>
        public void Log(double weight, string unit, string status)
        {
            var reading = new WeightReading(weight, unit, status);
            LogReading(reading);
        }

        /// <summary>
        /// Load all log entries from file
        /// </summary>
        public List<WeightReading> LoadLogs()
        {
            var readings = new List<WeightReading>();

            if (!File.Exists(_logFilePath))
                return readings;

            try
            {
                string[] lines = File.ReadAllLines(_logFilePath);

                // Skip header line
                for (int i = 1; i < lines.Length; i++)
                {
                    var reading = WeightReading.FromCsvString(lines[i]);
                    if (reading != null)
                    {
                        readings.Add(reading);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to load logs: {ex.Message}", ex);
            }

            return readings;
        }

        /// <summary>
        /// Filter logs by date range
        /// </summary>
        public List<WeightReading> FilterLogs(List<WeightReading> logs, string filterType)
        {
            return filterType switch
            {
                AppConstants.FilterToday => logs.Where(r => r.Timestamp.Date == DateTime.Today).ToList(),
                AppConstants.FilterThisMonth => logs.Where(r =>
                    r.Timestamp.Year == DateTime.Now.Year &&
                    r.Timestamp.Month == DateTime.Now.Month).ToList(),
                _ => logs
            };
        }

        /// <summary>
        /// Clear all logs
        /// </summary>
        public void ClearLogs()
        {
            if (File.Exists(_logFilePath))
            {
                File.Delete(_logFilePath);
            }
        }

        /// <summary>
        /// Export logs to specified file path
        /// </summary>
        public void ExportLogs(string destinationPath)
        {
            if (!File.Exists(_logFilePath))
                throw new FileNotFoundException("No logs available to export");

            File.Copy(_logFilePath, destinationPath, true);
        }

        /// <summary>
        /// Check if logs exist
        /// </summary>
        public bool HasLogs()
        {
            return File.Exists(_logFilePath);
        }
    }
}
