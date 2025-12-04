using System.IO.Ports;

namespace WeightApp.Models
{
    /// <summary>
    /// Serial port configuration model
    /// </summary>
    public class SerialPortConfig
    {
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public int DataBits { get; set; }
        public Parity Parity { get; set; }
        public StopBits StopBits { get; set; }

        public SerialPortConfig()
        {
            PortName = "COM1";
            BaudRate = 9600;
            DataBits = 8;
            Parity = Parity.None;
            StopBits = StopBits.One;
        }

        public SerialPortConfig(string portName, int baudRate)
        {
            PortName = portName;
            BaudRate = baudRate;
            DataBits = 8;
            Parity = Parity.None;
            StopBits = StopBits.One;
        }
    }
}
