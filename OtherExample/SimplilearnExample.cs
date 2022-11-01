using System;
using System.Threading.Tasks;
using System.Threading;

public class SimplilearnExample
{
    public static void Main1()
    {
        Method1();
        Console.WriteLine("Main1Main1");
        Console.ReadLine();
    }

    public static async void Method1()
    {
        await Task.Run(() => {
            for (int i = 0;i<100;i++) 
            {
                Console.WriteLine("Method1 is running");
                Task.Delay(100).Wait();
            }
        });
    }
}