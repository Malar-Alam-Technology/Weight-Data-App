# Garbage Truck Weight IoT System

A Windows Desktop Application for monitoring and logging garbage truck weight data from IoT hardware sensors via serial port communication.

## Features

### 1. No Authentication
- Direct access to the application without login requirements
- Quick start for immediate use

### 2. Port Input Data
- Real-time serial port communication with hardware weight sensors
- Configurable COM port selection
- Multiple baud rate options (9600, 19200, 38400, 57600, 115200)
- Automatic port detection
- Connection status monitoring

### 3. Standard Settings (Unit Selection)
- Multiple measurement units supported:
  - **kg** (Kilograms)
  - **g** (Grams)
  - **ton** (Metric Tons)
- Real-time unit conversion display
- Persistent settings storage

### 4. Activity Logging (Daily/Monthly)
- Automatic logging of all weight measurements
- Timestamp for each entry
- Filter options:
  - All records
  - Today's records
  - This month's records
- Export logs to CSV format
- Clear logs functionality

### 5. Configure Save (Data Persistence)
- Manual save button for current weight readings
- Settings auto-save on configuration changes
- CSV-based log storage for easy data access
- Export functionality for external analysis

### 6. Alert/Notify System
- Real-time status updates with color coding:
  - **Blue**: Information messages
  - **Green**: Success messages
  - **Orange**: Warnings
  - **Red**: Error messages
- Dialog notifications for important events
- Status bar for continuous feedback

## System Requirements

- **Operating System**: Windows 10 or later
- **.NET Runtime**: .NET 8.0 or later
- **Development IDE**: Visual Studio 2022 (for building from source)
- **Hardware**: Compatible serial port for weight sensor connection

## Installation & Setup

### Option 1: Build from Source

1. **Install Prerequisites**
   - Download and install [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
   - (Optional) Install [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/) for IDE development

2. **Clone or Download the Repository**
   ```bash
   git clone <repository-url>
   cd Weight-Data-App
   ```

3. **Build the Project**

   **Using Visual Studio:**
   - Open `WeightApp.sln` in Visual Studio 2022
   - Select `Build > Build Solution` (or press `Ctrl+Shift+B`)
   - Run the application by pressing `F5` or clicking the "Start" button

   **Using Command Line:**
   ```bash
   # Navigate to the project directory
   cd Weight-Data-App

   # Restore dependencies
   dotnet restore

   # Build the project
   dotnet build --configuration Release

   # Run the application
   dotnet run --project WeightApp/WeightApp.csproj
   ```

### Option 2: Run Pre-built Executable

If you have a pre-built executable:

1. Navigate to the build output directory:
   ```
   Weight-Data-App\WeightApp\bin\Release\net8.0-windows\
   ```

2. Double-click `WeightApp.exe` to launch the application

## How to Use

### 1. Connecting to Hardware

1. Launch the application
2. Go to the **Main** tab
3. In the **Port Configuration** section:
   - Select your COM port from the dropdown (e.g., COM3, COM4)
   - Select the appropriate baud rate (default: 9600)
   - Click **Connect** button
4. The status bar will show "Connected to [PORT] successfully!" in green

### 2. Receiving Weight Data

- Once connected, the hardware should send weight values via serial port
- The application expects numeric values (e.g., "1234.56")
- Each line received should contain a single weight value
- Current weight displays in large text with the selected unit
- Converted weights appear below in other units

### 3. Changing Measurement Unit

1. Navigate to the **Settings** tab
2. Select your preferred unit from the **Measurement Unit** dropdown:
   - kg (Kilograms)
   - g (Grams)
   - ton (Metric Tons)
3. Click **Save Settings** button
4. The display will update with the new unit
5. Settings persist across application restarts

### 4. Saving Weight Data

**Manual Save:**
- On the **Main** tab, click **Save Current Data** button
- A confirmation dialog shows the saved weight and conversions
- Data is logged with timestamp to CSV file

**Automatic Logging:**
- All connection events are automatically logged
- Each manual save creates a log entry

### 5. Viewing Activity Logs

1. Navigate to the **Activity Logs** tab
2. View all logged activities in the data grid
3. Use the **Filter Logs** dropdown to filter by:
   - **All**: Show all records
   - **Today**: Show only today's records
   - **This Month**: Show only this month's records
4. Click **Refresh** to reload logs from file

### 6. Managing Logs

**Export Logs:**
- Click **Export to CSV** button
- Choose a location and filename
- Logs are saved in CSV format for Excel or other tools

**Clear Logs:**
- Click **Clear Logs** button
- Confirm the action in the dialog
- All log entries will be permanently deleted

### 7. Disconnecting

1. On the **Main** tab
2. Click **Disconnect** button
3. Port becomes available for other applications

## Hardware Integration

### Serial Data Format

The application expects weight data in the following format:

```
123.45\n
456.78\n
789.01\n
```

- Each line should contain a numeric value
- Values should be terminated with newline (`\n`)
- The application interprets values in the currently selected unit
- Non-numeric data will trigger a warning in the status bar

### Example Arduino Code

```cpp
#include <HX711.h>

const int LOADCELL_DOUT_PIN = 2;
const int LOADCELL_SCK_PIN = 3;

HX711 scale;

void setup() {
  Serial.begin(9600);
  scale.begin(LOADCELL_DOUT_PIN, LOADCELL_SCK_PIN);

  // Calibrate your scale here
  scale.set_scale(2280.f);  // Calibration factor
  scale.tare();             // Reset to zero
}

void loop() {
  if (scale.is_ready()) {
    float weight = scale.get_units(10);  // Average of 10 readings
    Serial.println(weight);              // Send weight to serial
  }
  delay(1000);  // Send data every second
}
```

## File Structure

```
Weight-Data-App/
├── WeightApp/
│   ├── Constants/              # Application-wide constants
│   │   └── AppConstants.cs
│   ├── Models/                 # Data models and entities
│   │   ├── AppSettings.cs
│   │   ├── SerialPortConfig.cs
│   │   └── WeightReading.cs
│   ├── Services/               # Business logic layer
│   │   ├── LoggingService.cs
│   │   ├── SerialPortService.cs
│   │   └── SettingsService.cs
│   ├── Forms/                  # UI layer (Windows Forms)
│   │   ├── MainForm.cs         # Main application logic
│   │   ├── MainForm.Designer.cs # UI design code
│   │   └── MainForm.resx       # UI resources
│   ├── Helpers/                # Utility classes
│   │   ├── StatusHelper.cs
│   │   └── UnitConverter.cs
│   ├── Program.cs              # Application entry point
│   ├── WeightApp.csproj        # Project configuration
│   └── WeightApp.csproj.user   # User-specific settings
├── WeightApp.sln               # Solution file
├── ARCHITECTURE.md             # Architecture documentation
├── QUICKSTART.md               # Quick start guide
├── README.md                   # This file
└── .gitignore                  # Git ignore rules
```

**See [ARCHITECTURE.md](ARCHITECTURE.md) for detailed explanation of the project structure and design patterns.**

### Generated Files (Runtime)

- `settings.txt` - Stores the selected measurement unit
- `weight_logs.csv` - Contains all logged weight data
- Exported CSV files from log exports

## Troubleshooting

### Port Connection Issues

**Problem**: Cannot connect to COM port

**Solutions**:
- Verify the hardware is connected and powered on
- Check Device Manager to confirm COM port number
- Ensure no other application is using the port
- Try different baud rates
- Check cable connections
- Install necessary USB-to-Serial drivers if needed

### No Data Received

**Problem**: Connected but no weight data appears

**Solutions**:
- Verify hardware is sending data (check with serial monitor)
- Confirm baud rate matches hardware configuration
- Check data format (must be numeric with newline)
- Verify hardware is properly calibrated
- Check serial cable quality

### Application Won't Start

**Problem**: Application fails to launch

**Solutions**:
- Install .NET 8.0 Runtime from Microsoft
- Run as Administrator if permission issues occur
- Check Windows compatibility mode
- Verify antivirus is not blocking the application

### Logs Not Saving

**Problem**: Data not appearing in Activity Logs

**Solutions**:
- Check write permissions in application directory
- Verify disk space is available
- Look for error messages in status bar
- Try running application as Administrator

## Data Format

### Log File (weight_logs.csv)

```csv
Timestamp,Weight,Unit,Status
2025-11-26 10:30:15,1234.56,kg,Manual Save
2025-11-26 10:31:22,1250.00,kg,Manual Save
2025-11-26 10:32:08,0.00,kg,Port Connected
```

### Settings File (settings.txt)

```
kg
```

## Technical Details

- **Framework**: .NET 8.0 Windows Forms
- **Language**: C# 12.0
- **Serial Communication**: System.IO.Ports
- **Data Storage**: CSV files
- **UI**: Windows Forms with custom styling

## Development

### Building in Debug Mode

```bash
dotnet build --configuration Debug
```

### Running Tests

```bash
dotnet test
```

### Publishing for Distribution

```bash
dotnet publish --configuration Release --self-contained true --runtime win-x64
```

This creates a standalone executable that doesn't require .NET installation.

## Support & Contribution

For issues, questions, or contributions:

1. Check existing documentation
2. Review troubleshooting section
3. Create an issue on the repository
4. Submit pull requests for improvements

## License

This project is provided as-is for educational and commercial use.

## Version History

- **v1.0.0** (2025-11-26)
  - Initial release
  - Serial port communication
  - Multi-unit support (kg, g, ton)
  - Activity logging with filters
  - CSV export functionality
  - Real-time alerts and notifications

---

**Last Updated**: November 26, 2025
