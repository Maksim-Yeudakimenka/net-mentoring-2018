using System;
using System.Threading.Tasks;

namespace Task1
{
  class Program
  {
    static void Main(string[] args)
    {
      var tasks = new Task[100];

      for (var i = 0; i < 100; i++)
      {
        var j = i;
        tasks[i] = new Task(() => IterateAndPrint(j));
        tasks[i].Start();
      }

      Task.WaitAll(tasks);
    }

    static void IterateAndPrint(int taskIndex)
    {
      for (var k = 0; k < 1000; k++)
      {
        Console.WriteLine($"Task #{taskIndex} – {k}");
      }
    }
  }
}