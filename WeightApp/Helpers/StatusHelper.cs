namespace WeightApp.Helpers
{
    /// <summary>
    /// Helper class for status message management
    /// </summary>
    public static class StatusHelper
    {
        public enum StatusType
        {
            Info,
            Success,
            Warning,
            Error
        }

        /// <summary>
        /// Get color for status type
        /// </summary>
        public static Color GetStatusColor(StatusType type)
        {
            return type switch
            {
                StatusType.Info => Color.Blue,
                StatusType.Success => Color.Green,
                StatusType.Warning => Color.Orange,
                StatusType.Error => Color.Red,
                _ => Color.Black
            };
        }

        /// <summary>
        /// Create status message with appropriate icon
        /// </summary>
        public static string FormatMessage(string message, StatusType type)
        {
            string icon = type switch
            {
                StatusType.Info => "ℹ",
                StatusType.Success => "✓",
                StatusType.Warning => "⚠",
                StatusType.Error => "✗",
                _ => ""
            };

            return string.IsNullOrEmpty(icon) ? message : $"{icon} {message}";
        }
    }
}
