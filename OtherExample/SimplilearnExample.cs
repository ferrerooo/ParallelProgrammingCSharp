using System;
using System.Threading.Tasks;
using System.Threading;

public class SimplilearnExample
{
    public static void Main1()
    {
        Method1();
        Method2();
        Console.WriteLine($"Main1 {Thread.CurrentThread.ManagedThreadId}");
        Console.WriteLine($"Main1Main1 {Thread.CurrentThread.ManagedThreadId}");
        Console.WriteLine($"Main1Main1Main1 {Thread.CurrentThread.ManagedThreadId}");
        Console.ReadLine();
    }

    public static async void Method1()
    {
        await Task.Run(() => {
            for (int i = 0;i<10;i++) 
            {
                Console.WriteLine($"Method1 is running {Thread.CurrentThread.ManagedThreadId}");
                Task.Delay(100).Wait();
            }
        });

        Console.WriteLine($"Post-Method1 {Thread.CurrentThread.ManagedThreadId}");
    }

    public static async void Method2()
    {
        await Task.Run(() => {
            for (int i = 0;i<5;i++) 
            {
                Console.WriteLine($"Method2 is running {Thread.CurrentThread.ManagedThreadId}");
                Task.Delay(100).Wait();
            }
        });

        Console.WriteLine($"Post-Method2 {Thread.CurrentThread.ManagedThreadId}");
    }
}