using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerManagement.Library;

namespace PowerManagement.Tests
{
  [TestClass]
  public class PowerManagerTests
  {
    private readonly IPowerManager _powerManager = new PowerManager();

    [TestMethod]
    public void GetLastSleepTimeTest()
    {
      var lastSleepTime = _powerManager.GetLastSleepTime();

      Console.WriteLine("Last sleep time: " + lastSleepTime);
    }

    [TestMethod]
    public void GetLastWakeTimeTest()
    {
      var lastWakeTime = _powerManager.GetLastWakeTime();

      Console.WriteLine("Last wake time: " + lastWakeTime);
    }

    [TestMethod]
    public void GetSystemBatteryStateTest()
    {
      var batteryState = _powerManager.GetSystemBatteryState();

      Console.WriteLine("Battery present: " + batteryState.BatteryPresent);
      Console.WriteLine("Remaining capacity: " + batteryState.RemainingCapacity);
      Console.WriteLine("Max capacity: " + batteryState.MaxCapacity);
      Console.WriteLine("Rate: " + batteryState.Rate);
    }

    [TestMethod]
    public void GetSystemPowerInformationTest()
    {
      var powerInformation = _powerManager.GetSystemPowerInformation();

      Console.WriteLine("Time remaining: " + powerInformation.TimeRemaining);
      Console.WriteLine("Idleness: " + powerInformation.Idleness);
      Console.WriteLine("Cooling mode: " + powerInformation.CoolingMode);
      Console.WriteLine("Max idleness allowed: " + powerInformation.MaxIdlenessAllowed);
    }

    [TestMethod]
    public void ReserveHiberFileTest()
    {
      _powerManager.ReserveHiberFile(true);
    }

    [TestMethod]
    public void HibernateTest()
    {
      _powerManager.Hibernate();
    }
  }
}
