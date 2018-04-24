set powerManager = CreateObject("PowerManagement.Library.PowerManager")

powerInformation = powerManager.GetSystemPowerInformation()

WScript.Echo(powerInformation)

