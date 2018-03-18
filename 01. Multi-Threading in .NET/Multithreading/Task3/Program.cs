using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Task3
{
  class Program
  {
    static void Main(string[] args)
    {
      const int m1 = 1500;
      const int n1 = 1100;
      const int m2 = 1100;
      const int n2 = 1300;

      var matrix1 = InitializeMatrix(m1, n1);
      var matrix2 = InitializeMatrix(m2, n2);

      Console.WriteLine($"Input: A[{m1}x{n1}], B[{m2}x{n2}], both of type int");

      var stopwatch = Stopwatch.StartNew();
      var product = MultiplyMatrices(matrix1, matrix2);
      stopwatch.Stop();

      Console.WriteLine($"Multiplication A x B took: {stopwatch.ElapsedMilliseconds} ms");
    }

    static int[,] InitializeMatrix(int m, int n)
    {
      var matrix = new int[m, n];
      var random = new Random();

      for (var i = 0; i < m; i++)
      {
        for (var j = 0; j < n; j++)
        {
          matrix[i, j] = random.Next(500);
        }
      }

      return matrix;
    }

    static long[,] MultiplyMatrices(int[,] matrix1, int[,] matrix2)
    {
      var m1 = matrix1.GetLength(0);
      var n1 = matrix1.GetLength(1);
      var m2 = matrix2.GetLength(0);
      var n2 = matrix2.GetLength(1);

      if (n1 != m2)
      {
        throw new InvalidOperationException("Cannot multiply the two provided matrices A and B. " +
                                            "The number of columns in A should be equal to the number of rows in B");
      }

      var product = new long[m1, n2];

      Parallel.For(0, m1, i =>
      {
        for (var j = 0; j < n2; j++)
        {
          for (var k = 0; k < n1; k++)
          {
            product[i, j] += matrix1[i, k] * matrix2[k, j];
          }
        }
      });

      return product;
    }
  }
}