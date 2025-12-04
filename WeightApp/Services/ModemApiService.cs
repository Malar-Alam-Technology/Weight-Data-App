using System.Net.Http;
using System.Text.Json;
using WeightApp.Constants;
using WeightApp.Models;

namespace WeightApp.Services
{
    /// <summary>
    /// Service for communicating with the modem API
    /// </summary>
    public class ModemApiService : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = AppConstants.ApiUrl;
        private bool _disposed = false;

        // Events
        public event EventHandler<ApiResponse>? ApiSuccess;
        public event EventHandler<string>? ApiError;

        public ModemApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        /// <summary>
        /// Sends weight data to the modem API
        /// </summary>
        /// <param name="deviceId">Unique device identifier (UUID format)</param>
        /// <param name="weight">Weight value</param>
        /// <param name="direction">Direction (IN or OUT)</param>
        /// <returns>API response</returns>
        public async Task<ApiResponse?> SendWeightDataAsync(string deviceId, double weight, string direction)
        {
            try
            {
                // Format transaction timestamp
                string transaction = DateTime.Now.ToString("yyyy-MM-dd- HH:mm:ss");

                // Build URL with query parameters
                string url = $"{_baseUrl}?id={Uri.EscapeDataString(deviceId)}&weight={weight:F2}&direction={Uri.EscapeDataString(direction)}&transaction={Uri.EscapeDataString(transaction)}";

                // Send GET request
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                // Parse JSON response
                string jsonResponse = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                ApiResponse? apiResponse = JsonSerializer.Deserialize<ApiResponse>(jsonResponse, options);

                if (apiResponse != null && apiResponse.Status == "success")
                {
                    ApiSuccess?.Invoke(this, apiResponse);
                }

                return apiResponse;
            }
            catch (HttpRequestException ex)
            {
                string errorMsg = $"Network error: {ex.Message}";
                ApiError?.Invoke(this, errorMsg);
                return null;
            }
            catch (TaskCanceledException)
            {
                string errorMsg = "Request timeout. Please check your internet connection.";
                ApiError?.Invoke(this, errorMsg);
                return null;
            }
            catch (Exception ex)
            {
                string errorMsg = $"API error: {ex.Message}";
                ApiError?.Invoke(this, errorMsg);
                return null;
            }
        }

        /// <summary>
        /// Tests connection to the API
        /// </summary>
        public async Task<bool> TestConnectionAsync(string deviceId)
        {
            try
            {
                var response = await SendWeightDataAsync(deviceId, 0.0, "TEST");
                return response != null && response.Status == "success";
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _httpClient?.Dispose();
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }
    }
}
