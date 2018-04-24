using System;
using System.Runtime.InteropServices;

namespace PowerManagement.Library
{
  public class PowerManagementApi
  {
    [DllImport("powrprof.dll", EntryPoint = "CallNtPowerInformation")]
    public static extern void GetSleepWakeTime(
      int informationLevel,
      IntPtr lpInputBuffer,
      int nInputBufferSize,
      out ulong time,
      int nOutputBufferSize);

    [DllImport("powrprof.dll", EntryPoint = "CallNtPowerInformation")]
    public static extern void GetSystemBatteryState(
      int informationLevel,
      IntPtr lpInputBuffer,
      int nInputBufferSize,
      out SystemBatteryState batteryState,
      int nOutputBufferSize);

    [DllImport("powrprof.dll", EntryPoint = "CallNtPowerInformation")]
    public static extern void GetSystemPowerInformation(
      int informationLevel,
      IntPtr lpInputBuffer,
      int nInputBufferSize,
      out SystemPowerInformation powerInformation,
      int nOutputBufferSize);

    [DllImport("powrprof.dll", EntryPoint = "CallNtPowerInformation")]
    public static extern void ReserveHiberFile(
      int informationLevel,
      bool lpInputBuffer,
      int nInputBufferSize,
      out bool info,
      int nOutputBufferSize);

    [DllImport("powrprof.dll", EntryPoint = "SetSuspendState")]
    public static extern void Hibernate(
      bool hibernate,
      bool forceCritical,
      bool disableWakeEvent);
  }
}