set powerManager = CreateObject("PowerManagement.Library.PowerManager")

batteryState = powerManager.GetSystemBatteryState()

WScript.Echo(batteryState)

