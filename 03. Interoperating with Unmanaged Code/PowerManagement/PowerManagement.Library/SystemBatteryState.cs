namespace PowerManagement.Library
{
  public struct SystemBatteryState
  {
    public bool AcOnLine;
    public bool BatteryPresent;
    public bool Charging;
    public bool Discharging;
    public bool[] Spare1;
    public uint MaxCapacity;
    public uint RemainingCapacity;
    public uint Rate;
    public uint EstimatedTime;
    public uint DefaultAlert1;
    public uint DefaultAlert2;
  }
}