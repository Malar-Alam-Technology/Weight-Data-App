# Quick Start Guide

## For Users (Running the Application)

### Prerequisites
1. Install .NET 8.0 Runtime: https://dotnet.microsoft.com/download/dotnet/8.0
2. Connect your weight sensor hardware to a COM port

### Steps to Run

#### Option 1: Using Visual Studio (Recommended for Development)
1. Install Visual Studio 2022 (Community Edition is free)
2. Open `WeightApp.sln`
3. Press `F5` to build and run

#### Option 2: Using Command Line
```bash
# Navigate to project directory
cd Weight-Data-App

# Build the project
dotnet build

# Run the application
dotnet run --project WeightApp/WeightApp.csproj
```

#### Option 3: Build Executable
```bash
# Build release version
dotnet build --configuration Release

# Find the executable at:
# WeightApp\bin\Release\net8.0-windows\WeightApp.exe

# Double-click to run
```

## Hardware Setup

### Connecting Your Weight Sensor

1. Connect the weight sensor to your computer's USB or Serial port
2. Note the COM port number (check Device Manager on Windows)
3. Configure your hardware to send numeric weight values via serial
4. Ensure baud rate matches between hardware and application (default: 9600)

### Supported Hardware

Any weight sensor that can send data via serial port, including:
- HX711-based load cell systems
- Industrial weight scales with RS232/RS485
- Arduino-based weight measurement systems
- USB-to-Serial weight sensors

## First Time Use

1. **Launch the Application**
   - Double-click `WeightApp.exe` or run from Visual Studio

2. **Configure Port Connection**
   - Select your COM port from dropdown
   - Choose baud rate (9600 is most common)
   - Click "Connect"

3. **Set Your Preferred Unit**
   - Go to "Settings" tab
   - Choose kg, g, or ton
   - Click "Save Settings"

4. **Start Weighing**
   - Return to "Main" tab
   - Weight data appears automatically from hardware
   - Click "Save Current Data" to log measurements

5. **View History**
   - Go to "Activity Logs" tab
   - Filter by "Today" or "This Month"
   - Export to CSV if needed

## Testing Without Hardware

If you don't have hardware yet, you can test with a serial port emulator:

### Windows: com0com (Free Virtual COM Port)
1. Download: https://sourceforge.net/projects/com0com/
2. Install and create a COM port pair (e.g., COM10 <-> COM11)
3. Connect the app to one port (COM10)
4. Use a terminal program on the other port (COM11) to send test data

### Example Test Data
Open a serial terminal on the paired port and type:
```
100.5
150.25
200.0
175.75
```

Each line will appear as a weight reading in the application.

## Common Issues

### "No ports available"
- Install USB-to-Serial drivers for your hardware
- Check Device Manager to verify port is recognized
- Try unplugging and reconnecting hardware

### "Access denied" or "Port in use"
- Close other programs using the serial port
- Disconnect and reconnect hardware
- Try running as Administrator

### No weight data appearing
- Verify hardware is powered and functioning
- Check baud rate matches hardware configuration
- Test hardware with a serial terminal first
- Ensure data format is numeric with newline

## Tips for Best Results

1. **Calibrate Your Hardware**: Ensure your weight sensor is properly calibrated before use
2. **Regular Saves**: Use the "Save Current Data" button frequently or after each weighing
3. **Export Logs**: Regularly export logs for backup and analysis
4. **Monitor Status Bar**: Watch the status messages for real-time feedback
5. **Disconnect When Done**: Always disconnect properly before closing the application

## Data Management

### Log Files Location
- Logs are saved in the same folder as the executable
- File name: `weight_logs.csv`
- Format: CSV (opens in Excel)

### Settings Location
- Saved in: `settings.txt`
- Contains: Currently selected unit

### Backup Your Data
Recommended backup files:
- `weight_logs.csv` - All your weight measurements
- `settings.txt` - Your configuration

## Need Help?

- Read the full README.md for detailed documentation
- Check the Troubleshooting section
- Verify hardware is sending correct data format
- Ensure .NET 8.0 Runtime is installed

## Next Steps

1. Connect your weight sensor hardware
2. Launch the application
3. Configure the serial port settings
4. Select your preferred measurement unit
5. Start weighing and logging data!

---

Enjoy using the Garbage Truck Weight IoT System!
