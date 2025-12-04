# Project Architecture

## Overview

The Garbage Truck Weight IoT System has been structured using a layered architecture with clear separation of concerns. This document describes the project structure, components, and their responsibilities.

## Folder Structure

```
WeightApp/
├── Constants/          # Application-wide constants
│   └── AppConstants.cs
├── Models/             # Data models and entities
│   ├── AppSettings.cs
│   ├── SerialPortConfig.cs
│   └── WeightReading.cs
├── Services/           # Business logic and external interactions
│   ├── LoggingService.cs
│   ├── SerialPortService.cs
│   └── SettingsService.cs
├── Forms/              # UI layer (Windows Forms)
│   ├── MainForm.cs
│   ├── MainForm.Designer.cs
│   └── MainForm.resx
├── Helpers/            # Utility classes and helper methods
│   ├── StatusHelper.cs
│   └── UnitConverter.cs
└── Program.cs          # Application entry point
```

## Architecture Layers

### 1. Constants Layer (`Constants/`)

**Purpose**: Centralized storage for application-wide constants

**Files**:
- `AppConstants.cs`: All constant values used throughout the application

**Responsibilities**:
- File paths
- Measurement units
- Log statuses
- UI messages
- Configuration arrays

**Benefits**:
- Single source of truth
- Easy maintenance
- Prevents magic strings
- Compile-time checking

---

### 2. Models Layer (`Models/`)

**Purpose**: Data structures and entities

**Files**:
- `WeightReading.cs`: Represents a single weight measurement
- `AppSettings.cs`: Application configuration settings
- `SerialPortConfig.cs`: Serial port connection configuration

**Responsibilities**:
- Data structure definitions
- Data validation
- Serialization/deserialization
- Type safety

**Key Features**:
- Immutable where appropriate
- Built-in CSV conversion
- Validation methods

---

### 3. Services Layer (`Services/`)

**Purpose**: Business logic and external system interactions

**Files**:
- `SerialPortService.cs`: Manages serial port communication
- `LoggingService.cs`: Handles activity logging operations
- `SettingsService.cs`: Manages application settings persistence

#### SerialPortService

**Responsibilities**:
- Serial port connection/disconnection
- Data reception from hardware
- Event-based data delivery
- Error handling

**Events**:
- `DataReceived`: Fires when valid weight data is received
- `ErrorOccurred`: Fires when an error occurs
- `InvalidDataReceived`: Fires when non-numeric data is received

**Key Methods**:
- `Connect(SerialPortConfig)`: Connect to serial port
- `Disconnect()`: Disconnect from serial port
- `GetAvailablePorts()`: Get list of available COM ports

#### LoggingService

**Responsibilities**:
- Writing logs to file
- Reading logs from file
- Filtering logs by date
- Exporting logs
- Clearing logs

**Key Methods**:
- `LogReading(WeightReading)`: Log a weight reading
- `LoadLogs()`: Load all log entries
- `FilterLogs(logs, filterType)`: Filter logs by date range
- `ExportLogs(path)`: Export logs to file
- `ClearLogs()`: Delete all logs

#### SettingsService

**Responsibilities**:
- Loading settings from file
- Saving settings to file
- Default settings management

**Key Methods**:
- `LoadSettings()`: Load application settings
- `SaveSettings(AppSettings)`: Save application settings
- `LoadMeasurementUnit()`: Load just the unit setting
- `SaveMeasurementUnit(unit)`: Save just the unit setting

---

### 4. Helpers Layer (`Helpers/`)

**Purpose**: Utility functions and reusable logic

**Files**:
- `UnitConverter.cs`: Weight unit conversion utilities
- `StatusHelper.cs`: Status message formatting and color management

#### UnitConverter

**Responsibilities**:
- Converting between kg, g, and ton
- Formatting weight displays
- Generating conversion text
- Validating units

**Key Methods**:
- `Convert(weight, fromUnit, toUnit)`: Convert weight between units
- `GetConversionText(weight, unit)`: Get text showing weight in other units
- `FormatWeight(weight, unit)`: Format weight with unit
- `IsValidUnit(unit)`: Validate unit string

#### StatusHelper

**Responsibilities**:
- Color coding for status messages
- Status type enumeration
- Message formatting

**Key Methods**:
- `GetStatusColor(StatusType)`: Get color for status type
- `FormatMessage(message, type)`: Format message with icon

---

### 5. Forms Layer (`Forms/`)

**Purpose**: User interface (presentation layer)

**Files**:
- `MainForm.cs`: Main application window logic
- `MainForm.Designer.cs`: UI component definitions
- `MainForm.resx`: UI resources

**Responsibilities**:
- Displaying data to user
- Capturing user input
- Coordinating services
- UI state management

**Dependencies**:
- Uses all Services
- Uses all Helpers
- Uses all Models
- References Constants

**Key Regions**:
- Initialization: Setup and configuration
- Serial Port Connection: Connection management
- Serial Port Events: Event handlers for serial data
- Weight Display: Weight visualization
- Settings: Configuration management
- Logging: Log viewing and management
- Status Management: Status display
- Form Events: Form lifecycle events

---

## Data Flow

### 1. Serial Data Reception Flow

```
Hardware Device
    ↓
SerialPortService (receives raw data)
    ↓
SerialPortService (validates and parses)
    ↓
SerialPortService.DataReceived Event
    ↓
MainForm.OnSerialDataReceived
    ↓
MainForm updates UI
    ↓
UnitConverter (formats display)
    ↓
User sees weight
```

### 2. Save Data Flow

```
User clicks "Save Data"
    ↓
MainForm.btnSaveData_Click
    ↓
LoggingService.Log(weight, unit, status)
    ↓
WeightReading model created
    ↓
CSV file updated
    ↓
MainForm refreshes log display
    ↓
User sees confirmation
```

### 3. Settings Flow

```
User changes unit
    ↓
User clicks "Save Settings"
    ↓
MainForm.btnSaveSettings_Click
    ↓
UnitConverter.IsValidUnit (validates)
    ↓
SettingsService.SaveMeasurementUnit
    ↓
File system updated
    ↓
MainForm.UpdateWeightDisplay
    ↓
UI reflects new unit
```

## Design Patterns Used

### 1. Service Layer Pattern
- Business logic separated from UI
- Services are reusable and testable
- Clean separation of concerns

### 2. Event-Driven Architecture
- SerialPortService uses events for communication
- Loose coupling between components
- Asynchronous data handling

### 3. Repository Pattern (Lightweight)
- LoggingService acts as repository for logs
- Abstracted file system access
- Centralized data access logic

### 4. Helper/Utility Pattern
- UnitConverter provides stateless utility functions
- StatusHelper provides reusable formatting
- Pure functions for easy testing

### 5. Dependency Injection (Constructor)
- MainForm receives services via constructor
- Easier testing and mocking
- Clear dependencies

## Benefits of This Architecture

### 1. Maintainability
- Each component has a single responsibility
- Easy to locate and fix bugs
- Clear structure for new developers

### 2. Testability
- Services can be tested independently
- Helpers are pure functions (easy to test)
- UI logic is minimal

### 3. Reusability
- Services can be used in other forms/projects
- Helpers are stateless and reusable
- Models can be serialized/deserialized easily

### 4. Scalability
- Easy to add new features
- New services can be added without affecting UI
- New models can be added for new data types

### 5. Readability
- Clear folder structure
- Descriptive naming
- Well-organized code with regions

## Future Enhancements

### Potential Improvements

1. **Dependency Injection Container**
   - Use IoC container for service management
   - Easier service lifetime management

2. **Interfaces for Services**
   - ISerialPortService, ILoggingService, etc.
   - Better for testing and mocking

3. **MVVM Pattern**
   - Separate ViewModel layer
   - Better data binding
   - More testable UI logic

4. **Database Support**
   - Replace CSV with SQLite
   - Better query capabilities
   - Improved performance for large datasets

5. **Unit Tests**
   - Add test projects
   - Test coverage for services
   - Integration tests for data flow

6. **Configuration System**
   - Use JSON for settings
   - Support multiple configuration profiles
   - Environment-specific settings

7. **Logging Framework**
   - Integrate Serilog or NLog
   - Structured logging
   - Multiple log targets

## File Dependencies

```
Program.cs
    └── MainForm (Forms/)
            ├── SerialPortService (Services/)
            │       └── SerialPortConfig (Models/)
            ├── LoggingService (Services/)
            │       └── WeightReading (Models/)
            ├── SettingsService (Services/)
            │       └── AppSettings (Models/)
            ├── UnitConverter (Helpers/)
            ├── StatusHelper (Helpers/)
            └── AppConstants (Constants/)
```

## Coding Standards

### Naming Conventions
- Services: `*Service.cs`
- Models: Descriptive nouns (e.g., `WeightReading`)
- Helpers: `*Helper.cs` or `*Converter.cs`
- Private fields: `_camelCase`
- Public properties: `PascalCase`
- Constants: `UPPER_CASE` or `PascalCase`

### Organization
- Use `#region` for logical grouping
- Keep methods focused and small
- Document public APIs with XML comments
- Use meaningful variable names

### Error Handling
- Use try-catch at appropriate levels
- Provide user-friendly error messages
- Log errors for debugging
- Don't swallow exceptions silently

## Summary

This architecture provides a solid foundation for the Garbage Truck Weight IoT System. The clear separation of concerns makes the codebase:

- **Easier to understand**: Clear structure and naming
- **Easier to maintain**: Isolated changes
- **Easier to test**: Testable components
- **Easier to extend**: Add features without breaking existing code

The modular design allows each component to evolve independently while maintaining system stability.

---

**Last Updated**: November 26, 2025
**Version**: 2.0 (Restructured Architecture)
