using System.IO.Ports;
using WeightApp.Models;

namespace WeightApp.Services
{
    /// <summary>
    /// Service for managing serial port communication
    /// </summary>
    public class SerialPortService : IDisposable
    {
        private SerialPort? _serialPort;
        private SerialPortConfig? _config;

        public event EventHandler<double>? DataReceived;
        public event EventHandler<string>? ErrorOccurred;
        public event EventHandler<string>? InvalidDataReceived;

        public bool IsConnected => _serialPort?.IsOpen ?? false;
        public string? CurrentPortName => _config?.PortName;

        /// <summary>
        /// Get list of available COM ports
        /// </summary>
        public static string[] GetAvailablePorts()
        {
            return SerialPort.GetPortNames();
        }

        /// <summary>
        /// Connect to serial port with given configuration
        /// </summary>
        public void Connect(SerialPortConfig config)
        {
            if (IsConnected)
                throw new InvalidOperationException("Already connected to a port");

            _config = config;
            _serialPort = new SerialPort
            {
                PortName = config.PortName,
                BaudRate = config.BaudRate,
                DataBits = config.DataBits,
                Parity = config.Parity,
                StopBits = config.StopBits
            };

            _serialPort.DataReceived += OnDataReceived;
            _serialPort.Open();
        }

        /// <summary>
        /// Disconnect from serial port
        /// </summary>
        public void Disconnect()
        {
            if (_serialPort != null && _serialPort.IsOpen)
            {
                _serialPort.DataReceived -= OnDataReceived;
                _serialPort.Close();
                _serialPort.Dispose();
                _serialPort = null;
            }
        }

        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (_serialPort != null && _serialPort.IsOpen)
                {
                    string data = _serialPort.ReadLine().Trim();

                    if (double.TryParse(data, out double weight))
                    {
                        DataReceived?.Invoke(this, weight);
                    }
                    else
                    {
                        InvalidDataReceived?.Invoke(this, data);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke(this, ex.Message);
            }
        }

        public void Dispose()
        {
            Disconnect();
        }
    }
}
