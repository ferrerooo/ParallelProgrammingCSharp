  using System;
  using System.Threading.Tasks;
  using System.Threading;

namespace AsyncAndAwait
{
  public class UsingAsyncAndAwait
  {
    public static void Main1()
    {
      Console.WriteLine("Main starts. CurrentThreadId:"+ Thread.CurrentThread.ManagedThreadId.ToString());
      Test();
      Console.WriteLine("Main ends");
      Console.ReadLine();
    }

    public static async void Test() 
    {
        Console.WriteLine("Test starts. CurrentThreadId:"+Thread.CurrentThread.ManagedThreadId.ToString());
        await Task.Run(() => {Work1();});
        await Task.Run(() => {Work2();});
        await Task.Run(() => {Work3();});
        Console.WriteLine("Test ends");
    }

    public static void Work1()
    {
        Console.WriteLine("Work1 start... CurrentThreadId:"+Thread.CurrentThread.ManagedThreadId.ToString());
        Thread.Sleep(6000);
        Console.WriteLine("Work1 ends");
    }

    public static void Work2()
    {
        Console.WriteLine("Work2 start... CurrentThreadId:"+Thread.CurrentThread.ManagedThreadId.ToString());
        Thread.Sleep(6000);
        Console.WriteLine("Work2 ends");
    }

    public static void Work3()
    {
        Console.WriteLine("Work3 start... CurrentThreadId:"+Thread.CurrentThread.ManagedThreadId.ToString());
        Thread.Sleep(6000);
        Console.WriteLine("Work3 ends");
    }
  }

    
}

/*
Output is. Alert: Work1,2,3 are executed one by one, not in parallel.

Main starts. CurrentThreadId:1
Test starts. CurrentThreadId:1
Main ends
Work1 start... CurrentThreadId:4
Work1 ends
Work2 start... CurrentThreadId:6
Work2 ends
Work3 start... CurrentThreadId:4
Work3 ends
Test ends

*/
