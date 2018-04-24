using System;
using System.Runtime.InteropServices;

namespace PowerManagement.Library
{
  [ComVisible(true)]
  [ClassInterface(ClassInterfaceType.None)]
  public class PowerManager : IPowerManager
  {
    private const int LastSleepTime = 15;
    private const int LastWakeTime = 14;
    private const int SystemBatteryState = 5;
    private const int SystemPowerInformation = 12;
    private const int SystemReserveHiberFile = 10;

    public ulong GetLastSleepTime()
    {
      PowerManagementApi.GetSleepWakeTime(LastSleepTime, IntPtr.Zero, 0, out var sleepTime, Marshal.SizeOf(typeof(ulong)));

      return sleepTime;
    }

    public ulong GetLastWakeTime()
    {
      PowerManagementApi.GetSleepWakeTime(LastWakeTime, IntPtr.Zero, 0, out ulong wakeTime, Marshal.SizeOf(typeof(ulong)));

      return wakeTime;
    }

    public SystemBatteryState GetSystemBatteryState()
    {
      PowerManagementApi.GetSystemBatteryState(SystemBatteryState, IntPtr.Zero, 0, out var batteryState, Marshal.SizeOf(typeof(SystemBatteryState)));

      return batteryState;
    }

    public SystemPowerInformation GetSystemPowerInformation()
    {
      PowerManagementApi.GetSystemPowerInformation(SystemPowerInformation, IntPtr.Zero, 0, out var powerInformation, Marshal.SizeOf(typeof(SystemPowerInformation)));

      return powerInformation;
    }

    public void ReserveHiberFile(bool toReserve)
    {
      PowerManagementApi.ReserveHiberFile(SystemReserveHiberFile, toReserve, Marshal.SizeOf(typeof(bool)), out var info, Marshal.SizeOf(typeof(bool)));
    }

    public void Hibernate()
    {
      PowerManagementApi.Hibernate(true, false, false);
    }
  }
}