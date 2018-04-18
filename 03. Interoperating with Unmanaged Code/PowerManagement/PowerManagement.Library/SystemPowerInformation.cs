namespace PowerManagement.Library
{
  public struct SystemPowerInformation
  {
    public uint MaxIdlenessAllowed;
    public uint Idleness;
    public uint TimeRemaining;
    public byte CoolingMode;
  }
}