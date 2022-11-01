
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskCoordination
{
  class ResetEvents
  {
    public static void Manual()
    {
      var evt = new ManualResetEventSlim(false);  // a binary signal.  IsSet = true/false
      var cts = new CancellationTokenSource();
      var token = cts.Token;
      Console.WriteLine($"event set init status : {evt.IsSet}");
      
      Task.Factory.StartNew(() =>
      {
        Console.WriteLine("Boiling water...");
        for (int i = 0; i < 30; i++)
        {
          token.ThrowIfCancellationRequested();
          Thread.Sleep(100);
        }
        Console.WriteLine("Water is ready.");
        evt.Set();  // evt.IsSet -> true
      }, token);

      var makeTea = Task.Factory.StartNew(() =>
      {
        Console.WriteLine("Waiting for water...");
        var ok = evt.Wait(5000, token); // if evt.IsSet==true, return true and keep evt IsSet=true; if evt.IsSet==false, return false; 
        if (ok)
        {
          Console.WriteLine("Here is your tea!");
        }
        else
        {
          Console.WriteLine("Your tea not ready!");
        }
        
        Console.WriteLine($"Is the event set? {evt.IsSet}");

        Console.WriteLine($"event set  status : {evt.IsSet}");
        //evt.Reset(); // evt.IsSet -> false
        //evt.Set();
        Console.WriteLine($"event set  status : {evt.IsSet}");

        var ok1 = evt.Wait(1000, token);
        if (ok1)
        {
          Console.WriteLine("That was a nice cup of tea!");
        }
        else
        {
          Console.WriteLine("No cup of tea!");
        }
      }, token);

      makeTea.Wait(token);
    }

    static void Main1(string[] args)
    {
      //Manual();
      Automatic();
      Thread.Sleep(10000);
    }

    private static void Automatic()
    {
      // try switching between manual and auto :)
      var evt = new AutoResetEvent(false);

      evt.Set(); // ok, it's set

      //evt.WaitOne(); // makes evt set to not set (this means autoReset)

      if (evt.WaitOne(1000))
      {
        Console.WriteLine("Succeeded");
      }
      else
      {
        Console.WriteLine("Timed out");
      }
    }
  }
}
