
using System;
using System.Threading;
using System.Threading.Tasks;
//using System.Management.Instrumentation;

namespace TaskCoordination
{
  class SemaphoreDemo
  {

    static void Main1()
    {
      var semaphore = new SemaphoreSlim(2, 10);

      for (int i = 0; i < 20; ++i)
      {
        Task.Factory.StartNew(() =>
        {
          Console.WriteLine($"Entering task {Task.CurrentId}.");
          semaphore.Wait(); // ReleaseCount--
          Console.WriteLine($"Processing task {Task.CurrentId}.");
        });
      }

      while (semaphore.CurrentCount <= 2)
      {
        Console.WriteLine($"Semaphore count: {semaphore.CurrentCount}");
        Console.ReadKey();
        semaphore.Release(2); // ReleaseCount += n
      }
    }
  }
}
