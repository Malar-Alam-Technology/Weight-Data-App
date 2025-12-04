namespace WeightApp.Models
{
    /// <summary>
    /// Response model from the modem API
    /// </summary>
    public class ApiResponse
    {
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public ApiResponseData? Data { get; set; }
    }

    /// <summary>
    /// Data model from the API response
    /// </summary>
    public class ApiResponseData
    {
        public string ItemId { get; set; } = string.Empty;
        public string ItemWeight { get; set; } = string.Empty;
        public string ItemDirection { get; set; } = string.Empty;
        public string ItemTransaction { get; set; } = string.Empty;
    }
}
