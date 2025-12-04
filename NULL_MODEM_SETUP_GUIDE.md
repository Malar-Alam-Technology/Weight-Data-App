# Null Modem Simulation Setup Guide for Windows

This guide will help you set up a virtual null modem (virtual serial port pair) on Windows to test the Weight Data App with the Modem API integration.

## Table of Contents
1. [Overview](#overview)
2. [Installing com0com (Virtual Serial Port)](#installing-com0com)
3. [Installing Terminal Emulator](#installing-terminal-emulator)
4. [Testing the Setup](#testing-the-setup)
5. [Sending Weight Data](#sending-weight-data)
6. [Troubleshooting](#troubleshooting)

---

## Overview

A **null modem** simulates a direct connection between two serial ports without physical hardware. This is perfect for:
- Testing serial port applications
- Simulating weight scale hardware
- Development and debugging

We'll use **com0com** to create virtual serial port pairs (e.g., COM3 ↔ COM4).

---

## Installing com0com (Virtual Serial Port)

### Step 1: Download com0com

1. Visit: https://sourceforge.net/projects/com0com/
2. Download the latest version: **com0com-3.0.0.0-i386-and-x64-signed.zip**
3. Extract the ZIP file to a folder (e.g., `C:\com0com`)

### Step 2: Install com0com

1. **Right-click** on `setup_com0com_W7_x64_signed.exe` (or the 32-bit version if you have 32-bit Windows)
2. Select **"Run as administrator"**
3. Follow the installation wizard:
   - Click **"Next"** through the welcome screens
   - Accept the license agreement
   - Choose installation directory (default is fine)
   - Click **"Install"**

4. **Windows may show a security warning** about unsigned drivers:
   - Click **"Install this driver software anyway"**
   - This is safe - com0com is a trusted open-source project

### Step 3: Configure Virtual Port Pair

After installation, the setup tool will open automatically. If not, run:
```
C:\Program Files (x86)\com0com\setupc.exe
```

**Configure the port pair:**

1. In the setup window, you'll see something like:
   ```
   CNCA0 PortName=COM3
   CNCB0 PortName=COM4
   ```

2. This creates a virtual cable between **COM3** and **COM4**:
   - Data sent to COM3 appears on COM4
   - Data sent to COM4 appears on COM3

3. **To change port numbers** (if COM3/COM4 are already in use):
   ```
   command> change CNCA0 PortName=COM10
   command> change CNCB0 PortName=COM11
   ```

4. **To add a new port pair:**
   ```
   command> install PortName=COM5 PortName=COM6
   ```

5. Type `quit` and press Enter to close the setup tool.

### Step 4: Verify Installation

1. Open **Device Manager** (Win + X → Device Manager)
2. Expand **"Ports (COM & LPT)"**
3. You should see:
   ```
   COM3 (com0com - serial port emulator)
   COM4 (com0com - serial port emulator)
   ```

---

## Installing Terminal Emulator

You'll need a terminal program to send weight data to the virtual serial port.

### Option 1: PuTTY (Recommended - Free)

1. Download PuTTY: https://www.putty.org/
2. Download **putty.exe** (no installation needed)
3. Save it somewhere accessible (e.g., `C:\Tools\putty.exe`)

### Option 2: Tera Term (Alternative - Free)

1. Download Tera Term: https://osdn.net/projects/ttssh2/
2. Run the installer
3. Follow the installation wizard

### Option 3: RealTerm (Advanced - Free)

1. Download RealTerm: https://sourceforge.net/projects/realterm/
2. Install or run the portable version

---

## Testing the Setup

### Step 1: Connect Weight App to COM3

1. Launch the **Weight Data App**
2. In the **Port Connection** panel:
   - Select **COM3** from the dropdown
   - Select **9600** baud rate (or the rate your scale uses)
   - Click **"Connect"**
3. Status should show: **"Connected to COM3 successfully!"**

### Step 2: Configure API Settings

In the **API Settings** panel:
1. **Check** "Enable API Integration"
2. **Device ID**: Enter your device UUID (default is pre-filled)
   ```
   eb69df27-83d3-4c59-9185-03586be56d29
   ```
3. **Direction**: Select **IN** or **OUT**
4. Click **"Save Settings"**

### Step 3: Open PuTTY on COM4

1. Run **PuTTY**
2. Select **"Serial"** connection type (not SSH)
3. Configure:
   - **Serial line**: `COM4`
   - **Speed (baud rate)**: `9600`
4. Click **"Open"**

A black terminal window will appear.

### Step 4: Send Test Data

In the PuTTY terminal window, type a weight value and press **Enter**:

```
1234.56
```

**What happens:**
1. PuTTY sends `1234.56\n` to COM4
2. com0com forwards it to COM3
3. Weight App receives the data on COM3
4. App displays: **"1234.56 kg"** (or your selected unit)
5. If API is enabled, app sends data to:
   ```
   http://220.158.208.202/semaling/registerweight/?id=eb69df27-83d3-4c59-9185-03586be56d29&weight=1234.56&direction=IN&transaction=2025-11-03- 16:12:09
   ```
6. API responds with:
   ```json
   {
       "status": "success",
       "message": "Registered successfully",
       "data": {
           "item_id": "eb69df27-83d3-4c59-9185-03586be56d29",
           "item_weight": "1234.56",
           "item_direction": "IN",
           "item_transaction": "2025-11-03- 16:12:09"
       }
   }
   ```
7. App logs the data and shows API success status

---

## Sending Weight Data

### Manual Testing (PuTTY)

Send weight values line by line:
```
100.5
250.75
1000.00
```

Each line triggers:
1. Weight display update
2. API call (if enabled)
3. Success/error status message

### Automated Testing (Script)

Create a batch file to send multiple values:

**test_weights.bat:**
```batch
@echo off
echo 100.50 > \\.\COM4
timeout /t 2 /nobreak
echo 250.75 > \\.\COM4
timeout /t 2 /nobreak
echo 500.00 > \\.\COM4
timeout /t 2 /nobreak
echo 1234.56 > \\.\COM4
```

Run this script to send values automatically with 2-second intervals.

### Using Python Script

**send_weights.py:**
```python
import serial
import time

# Open COM4 (the sender port)
ser = serial.Serial('COM4', 9600, timeout=1)

weights = [100.50, 250.75, 500.00, 1234.56, 2000.00]

print("Sending weight data...")
for weight in weights:
    data = f"{weight:.2f}\n"
    ser.write(data.encode())
    print(f"Sent: {weight:.2f} kg")
    time.sleep(2)

ser.close()
print("Done!")
```

Install pyserial first:
```batch
pip install pyserial
```

Run the script:
```batch
python send_weights.py
```

---

## Troubleshooting

### Problem: "Port not available" or "Access denied"

**Solution:**
1. Close any other programs using the COM port (including other terminal programs)
2. Disconnect from the Weight App
3. Wait 5 seconds
4. Reconnect

### Problem: No data received in Weight App

**Solution:**
1. **Verify port pair:**
   - Weight App: COM3
   - PuTTY/sender: COM4
2. **Check baud rate** matches on both sides (9600)
3. **Verify com0com is running:**
   - Open Device Manager
   - Check "Ports (COM & LPT)"
   - Virtual ports should be listed
4. **Send data with newline:**
   - PuTTY: Press Enter after typing
   - Script: Include `\n` at the end

### Problem: "Invalid data received"

**Solution:**
The Weight App expects **numeric values only**. Send:
```
✓ 1234.56
✓ 100
✓ 0.5
✗ 1234.56 kg  ← includes text
✗ abc         ← not a number
```

### Problem: API error messages

**Solutions:**
1. **"Network error"**: Check internet connection
2. **"Request timeout"**: API server may be down
3. **"Invalid device ID"**: Verify UUID format in settings
4. **Enable API checkbox unchecked**: Check "Enable API Integration"

### Problem: com0com installation fails on Windows 10/11

**Solution:**
1. **Disable Driver Signature Enforcement temporarily:**
   - Press **Win + I** → Recovery → Advanced startup → Restart now
   - Choose: Troubleshoot → Advanced options → Startup Settings → Restart
   - Press **F7** (Disable driver signature enforcement)
   - Install com0com
   - Restart normally

2. **Alternative: Use signed version**
   - The signed installer should work without disabling enforcement
   - Make sure you downloaded the "-signed" version

### Problem: Ports don't appear in Device Manager

**Solution:**
1. Run `setupc.exe` as administrator
2. Add port pair manually:
   ```
   command> install PortName=COM3 PortName=COM4
   command> list
   command> quit
   ```
3. Restart computer
4. Check Device Manager again

---

## Quick Reference

### Port Configuration
- **Weight App**: Connect to COM3 (receiver)
- **Sender (PuTTY/Script)**: Send to COM4 (transmitter)
- **Baud Rate**: 9600 (both sides must match)

### Data Format
- **Valid**: `1234.56\n` (number + newline)
- **Unit**: Always send in kilograms
- **Decimal places**: 2 recommended

### API Integration
- **Enable**: Check "Enable API Integration"
- **Device ID**: UUID format (example provided)
- **Direction**: IN or OUT
- **Endpoint**: Automatically called on data reception

### Testing Commands (PuTTY)
```
100.00
250.50
1234.56
```

---

## Summary

1. ✅ Install **com0com** to create virtual COM port pair
2. ✅ Install **PuTTY** (or another terminal emulator)
3. ✅ Configure **COM3 ↔ COM4** virtual cable
4. ✅ Connect **Weight App** to COM3
5. ✅ Enable **API Integration** in settings
6. ✅ Open **PuTTY** on COM4
7. ✅ Send weight values (e.g., `1234.56`) and press Enter
8. ✅ Verify data appears in Weight App
9. ✅ Verify API call succeeds (check status bar)

---

## Support

For additional help:
- **com0com issues**: https://sourceforge.net/p/com0com/discussion/
- **PuTTY help**: https://www.putty.org/
- **Weight App**: Check the application logs and status bar for detailed error messages

---

**Happy Testing! 🎉**
