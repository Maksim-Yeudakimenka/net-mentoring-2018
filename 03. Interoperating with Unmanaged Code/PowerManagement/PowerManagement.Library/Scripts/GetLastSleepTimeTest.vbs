set powerManager = CreateObject("PowerManagement.Library.PowerManager")

lastSleepTime = powerManager.GetLastSleepTime()

WScript.Echo(lastSleepTime)

