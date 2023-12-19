using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace IntroducingTasks
{
    class TokenAssociationExample
    {

        public static void Main1()
        {
            // WithAssociation();
            WithoutAssociation();
        }

        public static void WithAssociation()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            Task t = new Task(() =>
            {
                try
                {
                    // Simulate long-running work
                    Console.WriteLine("t come to 111");
                    Task.Delay(10000).Wait();
                    Console.WriteLine("t come to 222");
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Task was canceled with token association.");
                }
            }, token);

            t.Start();
            Console.WriteLine("Main come to 333");
            Thread.Sleep(1000); // Simulate work
            Console.WriteLine("Main come to 444");
            cts.Cancel(); // Cancel the task
            Console.WriteLine("Main come to 555");

            try
            {
                Console.WriteLine("Main come to 666");
                /* When you call Wait() on a task, the current thread is blocked until the task finishes its execution */
                t.Wait(token); // This will throw if the task is canceled
                Console.WriteLine("Main come to 777");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Wait was canceled due to token association.");
            }
        }

        public static void WithoutAssociation()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            Task t = new Task(() =>
            {
                try
                {
                    // Simulate long-running work
                    Console.WriteLine("t come to 111");
                    Task.Delay(10000).Wait();
                    Console.WriteLine("t come to 222");
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Task was canceled with token association.");
                }
            });

            t.Start();
            Console.WriteLine("Main come to 333");
            Thread.Sleep(1000); // Simulate work
            Console.WriteLine("Main come to 444");
            cts.Cancel(); // Cancel the task
            Console.WriteLine("Main come to 555");

            try
            {
                Console.WriteLine("Main come to 666");
                /* When you call Wait() on a task, the current thread is blocked until the task finishes its execution */
                t.Wait(token); // This will throw if the task is canceled
                Console.WriteLine("Main come to 777");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Wait was canceled due to token association.");
            }
        }

    }
}

/*
dotnet run Program.cs

When using Task.Delay(10000), below is the output
chunyaozou@Chunyaos-MacBook-Air ParallelProgrammingCSharp % dotnet run Program.cs
==start==
Main come to 333
t come to 111
Main come to 444
Main come to 555
Main come to 666
Wait was canceled due to token association.
==end==

When using Task.Delay(500), below is the output
chunyaozou@Chunyaos-MacBook-Air ParallelProgrammingCSharp % dotnet run Program.cs
==start==
Main come to 333
t come to 111
t come to 222
Main come to 444
Main come to 555
Main come to 666
Main come to 777
==end==

*/
