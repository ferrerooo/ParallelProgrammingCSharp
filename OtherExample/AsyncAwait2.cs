using System;
using System.Threading.Tasks;
using System.Threading;

public class AsyncAwait2
    {
        public static void Main1(string[] args)
        {
            Console.WriteLine($">>>>>>>>>>>>>>>>主线程启动 {Thread.CurrentThread.ManagedThreadId}");
            Task<string> task = GetStringAsync1();
            //Task<string> task = GetStringFromTask();
            Console.WriteLine($"<<<<<<<<<<<<<<<<主线程结束 {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"GetStringAsync1执行结果：{task.Result} {Thread.CurrentThread.ManagedThreadId}");
        }

        static async Task<string> GetStringAsync1()
        {
            Console.WriteLine($">>>>>>>>GetStringAsync1方法启动 {Thread.CurrentThread.ManagedThreadId}");
            string str = await GetStringAsync2();
            //string str = await GetStringLong();
            Console.WriteLine($"<<<<<<<<GetStringAsync1方法结束 {Thread.CurrentThread.ManagedThreadId}");
            return str;
        }
        static async Task<string> GetStringAsync2()
        {   Thread.Sleep(5000);
            Console.WriteLine($">>>>>>>>GetStringAsync2方法启动 {Thread.CurrentThread.ManagedThreadId}");
            string str = await GetStringFromTask();
            //Task<string> str = await GetStringFromTask();
            Console.WriteLine($"<<<<<<<<GetStringAsync2方法结束 {Thread.CurrentThread.ManagedThreadId}");
            return str;
        }

        static Task<string> GetStringFromTask()
        {
            Console.WriteLine($">>>>GetStringFromTask方法启动 {Thread.CurrentThread.ManagedThreadId}");
            Task<string> task = new Task<string>(() =>
             {
                 Console.WriteLine($">>任务线程启动 {Thread.CurrentThread.ManagedThreadId}");
                 Thread.Sleep(1000);
                 Console.WriteLine($"<<任务线程结束 {Thread.CurrentThread.ManagedThreadId}");
                 return "hello world";
             });
            task.Start();
            Console.WriteLine($"<<<<GetStringFromTask方法结束 {Thread.CurrentThread.ManagedThreadId}");
            return task;
        }

        static Task<string> GetStringLong()
        {
            return null;
        }
    }