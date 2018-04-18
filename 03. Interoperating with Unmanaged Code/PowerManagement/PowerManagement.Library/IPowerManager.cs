namespace PowerManagement.Library
{
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