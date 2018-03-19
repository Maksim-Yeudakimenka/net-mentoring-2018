using System;
using System.Linq;
using System.Threading.Tasks;

namespace Task2
{
  class Program
  {
    static readonly Random Random = new Random();

    static void Main(string[] args)
    {
      var startTask = new Task<int[]>(CreateArray);

      var continuation = startTask
        .ContinueWith(task => MultiplyArray(task.Result))
        .ContinueWith(task => SortArray(task.Result))
        .ContinueWith(task => CalculateAverage(task.Result));

      startTask.Start();
      continuation.Wait();
    }

    static int[] CreateArray()
    {
      var arr = new int[10];

      for (var i = 0; i < 10; i++)
      {
        arr[i] = Random.Next(10);
      }

      PrintArray(arr, "Newly created array");

      return arr;
    }

    static int[] MultiplyArray(int[] arr)
    {
      var multiplier = Random.Next(10);

      for (var i = 0; i < arr.Length; i++)
      {
        arr[i] *= multiplier;
      }

      PrintArray(arr, $"Multiplied array (x{multiplier})");

      return arr;
    }

    static int[] SortArray(int[] arr)
    {
      Array.Sort(arr);

      PrintArray(arr, "Sorted array");

      return arr;
    }

    static double CalculateAverage(int[] arr)
    {
      var average = arr.Average();

      Console.WriteLine($"Average value: {average}");

      return average;
    }

    static void PrintArray(int[] arr, string description)
    {
      var flatValues = string.Join(" ", arr);

      Console.WriteLine($"{description}: {flatValues}");
    }
  }
}