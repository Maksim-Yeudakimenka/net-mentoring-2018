set powerManager = CreateObject("PowerManagement.Library.PowerManager")

lastWakeTime = powerManager.GetLastWakeTime()

WScript.Echo(lastWakeTime)

