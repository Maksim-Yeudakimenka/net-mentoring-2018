using System;
using System.Threading;

namespace Task4
{
  class Program
  {
    static void Main(string[] args)
    {
      var thread = CreateThread();
      thread.Start(10);
      thread.Join();
    }

    static Thread CreateThread()
    {
      return new Thread(state =>
      {
        var value = (int) state;
        value--;
        Console.WriteLine(value);

        if (value > 0)
        {
          var thread = CreateThread();
          thread.Start(value);
          thread.Join();
        }
      });
    }
  }
}