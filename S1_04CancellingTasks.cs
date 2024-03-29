using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace IntroducingTasks
{
  class CancelingTasks
  {
    public static void Main1()
    {
      //Thread.SpinWait(10000);

      CancelableTasks();

      //MonitoringCancelation();

      //CompositeCancelationToken();
      
      //WaitingForTimeToPass();

      Console.WriteLine("Main program done, press any key.");
      Console.Read();
    }

    public static void CancelableTasks()
    {
      var cts = new CancellationTokenSource();
      var token = cts.Token;
      Task t = new Task(() =>
      {
        int i = 0;
        while (true)
        {
          if (token.IsCancellationRequested) // task cancelation is cooperative, no-one kills your thread
            break;
          else
            Console.WriteLine($"{i++}\t");
        }
      }, token);
      t.Start();

      // don't forget CancellationToken.None

      //Console.ReadKey();
      Thread.Sleep(1000);
      cts.Cancel();
      Console.WriteLine("Task has been canceled.");
    }

    public static void MonitoringCancelation()
    {
      var cts = new CancellationTokenSource();
      var token = cts.Token;

      // register a delegate to fire
      token.Register(() =>
      {
        Console.WriteLine("Cancelation has been requested.");
      });

      Task t = new Task(() =>
      {
        int i = 0;
        while (true)
        {
          if (token.IsCancellationRequested) // 1. Soft exit
                                             // RanToCompletion
          {
            break;
          }
          else
          {
            Console.Write($"{i++}\t");
            Thread.Sleep(100);
          }
        }
      });
      t.Start();

      // canceling multiple tasks
      Task t2 = Task.Factory.StartNew(() =>
      {
        char c = 'a';
        while (true)
        {
          // alternative to what's below
          //token.ThrowIfCancellationRequested(); // 2. Hard exit, Canceled
          //Console.Write($"{c++}\t");
          //Thread.Sleep(200);

          if (token.IsCancellationRequested) // same as above, start HERE
          {
            // release resources, if any
            throw new OperationCanceledException("No longer interested in printing letters.");
          }
          else
          {
            Console.Write($"{c++}\t");
            Thread.Sleep(200);
          }
        }
      }, token);

      // cancellation on a wait handle
      Task t3 = Task.Factory.StartNew(() =>
      {
        token.WaitHandle.WaitOne(5000); // returns true if the token has been cancelled ; return false if the time elapsed
        Console.WriteLine("Wait handle released, thus cancelation was requested");
      });

      //Console.ReadKey();

      Thread.Sleep(10000);
      cts.Cancel();

      Thread.Sleep(1000);

      Console.WriteLine($"Task has been canceled. The status of the canceled task 't' is {t.Status}."); // status enum https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskstatus?view=net-5.0
      Console.WriteLine($"Task has been canceled. The status of the canceled task 't2' is {t2.Status}.");
      Console.WriteLine($"t.IsCanceled = {t.IsCanceled}, t2.IsCanceled = {t2.IsCanceled}, t3.IsCanceled = {t3.IsCanceled}");
    }

    public static void CompositeCancelationToken()
    {
      // it's possible to create a 'composite' cancelation source that involves several tokens
      var planned = new CancellationTokenSource();
      var preventative = new CancellationTokenSource();
      var emergency = new CancellationTokenSource();

      // make a token source that is linked on their tokens
      var paranoid = CancellationTokenSource.CreateLinkedTokenSource(
        planned.Token, preventative.Token, emergency.Token);

      Task.Factory.StartNew(() =>
      {
        int i = 0;
        while (true)
        {
          paranoid.Token.ThrowIfCancellationRequested();
          Console.Write($"{i++}\t");
          Thread.Sleep(100);
        }
      }, paranoid.Token);

      paranoid.Token.Register(() => Console.WriteLine("Cancelation requested"));

      Console.ReadKey();

      // use any of the aforementioned token soures
      emergency.Cancel();
    }

    public static void WaitingForTimeToPass()
    {
      // we've already seen the classic Thread.Sleep

      var cts = new CancellationTokenSource();
      var token = cts.Token;
      var t = new Task(() =>
      {
        Console.WriteLine("You have 5 seconds to disarm this bomb by pressing a key");
        bool canceled = token.WaitHandle.WaitOne(5000); // returns true if the token has been cancelled ; return false if the time elapsed
        Console.WriteLine(canceled ? "Bomb disarmed." : "BOOM!!!!");
      }, token);
      t.Start();

      // unlike sleep and waitone
      // thread does not give up its turn
      Thread.SpinWait(1000); // need more understanding about this
      Console.WriteLine("Are you still here?");

      Console.ReadKey();
      cts.Cancel();
    }

    




  }
}
