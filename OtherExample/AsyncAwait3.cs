using System;
using System.Threading.Tasks;
using System.Threading;

public class AsyncAwait3
    {
        public static void Main1(string[] args)
        {
            Console.WriteLine($">>>>>>>>>>>>>>>>主线程启动 {Thread.CurrentThread.ManagedThreadId}");
            Task<string> task = GetStringAsync1();
            Console.WriteLine($"<<<<<<<<<<<<<<<<主线程结束 {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"GetStringAsync1执行结果：{task.Result} {Thread.CurrentThread.ManagedThreadId}");
        }

        static async Task<string> GetStringAsync1()
        {
            Console.WriteLine($">>>>>>>>GetStringAsync1方法启动 {Thread.CurrentThread.ManagedThreadId}");
            await Task.Run(() => {
                for (int i = 0;i<5;i++) 
                {
                    Console.WriteLine($"MethodAAAAAAA is running {Thread.CurrentThread.ManagedThreadId}");
                    Task.Delay(1000).Wait();
                }
            });
            Console.WriteLine($"<<<<<<<<GetStringAsync1方法结束 {Thread.CurrentThread.ManagedThreadId}");
            return "123";
        }
    }