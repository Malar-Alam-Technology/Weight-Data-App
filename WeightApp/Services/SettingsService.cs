using System.Text.Json;
using WeightApp.Constants;
using WeightApp.Models;

namespace WeightApp.Services
{
    /// <summary>
    /// Service for managing application settings
    /// </summary>
    public class SettingsService
    {
        private readonly string _settingsFilePath;
        private readonly string _jsonSettingsFilePath;

        public SettingsService(string? settingsFilePath = null)
        {
            _settingsFilePath = settingsFilePath ?? AppConstants.SettingsFilePath;
            _jsonSettingsFilePath = Path.Combine(
                Path.GetDirectoryName(_settingsFilePath) ?? "",
                "settings.json"
            );
        }

        /// <summary>
        /// Load settings from file
        /// </summary>
        public AppSettings LoadSettings()
        {
            var settings = new AppSettings();

            try
            {
                // Try loading from JSON first (new format)
                if (File.Exists(_jsonSettingsFilePath))
                {
                    string jsonContent = File.ReadAllText(_jsonSettingsFilePath);
                    var loadedSettings = JsonSerializer.Deserialize<AppSettings>(jsonContent);
                    if (loadedSettings != null)
                    {
                        return loadedSettings;
                    }
                }
                // Fallback to old text format (backward compatibility)
                else if (File.Exists(_settingsFilePath))
                {
                    string content = File.ReadAllText(_settingsFilePath).Trim();
                    if (!string.IsNullOrEmpty(content))
                    {
                        settings.MeasurementUnit = content;
                    }
                }
            }
            catch
            {
                // Return default settings on error
            }

            return settings;
        }

        /// <summary>
        /// Save settings to file
        /// </summary>
        public void SaveSettings(AppSettings settings)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                string jsonContent = JsonSerializer.Serialize(settings, options);
                File.WriteAllText(_jsonSettingsFilePath, jsonContent);

                // Also save to old format for backward compatibility
                File.WriteAllText(_settingsFilePath, settings.MeasurementUnit);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to save settings: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Save only the measurement unit (backward compatible)
        /// </summary>
        public void SaveMeasurementUnit(string unit)
        {
            var settings = LoadSettings();
            settings.MeasurementUnit = unit;
            SaveSettings(settings);
        }

        /// <summary>
        /// Load only the measurement unit
        /// </summary>
        public string LoadMeasurementUnit()
        {
            var settings = LoadSettings();
            return settings.MeasurementUnit;
        }
    }
}
