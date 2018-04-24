using System.Runtime.InteropServices;

namespace PowerManagement.Library
{
  [ComVisible(true)]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  public interface IPowerManager
  {
    ulong GetLastSleepTime();
    ulong GetLastWakeTime();
    SystemBatteryState GetSystemBatteryState();
    SystemPowerInformation GetSystemPowerInformation();
    void ReserveHiberFile(bool toReserve);
    void Hibernate();
  }
}