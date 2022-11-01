using System;
using System.Threading.Tasks;
using System.Threading;

public class AsyncAwait2
    {
        public static void Main1(string[] args)
        {
            Console.WriteLine(">>>>>>>>>>>>>>>>主线程启动");
            Task<string> task = GetStringAsync1();
            Console.WriteLine("<<<<<<<<<<<<<<<<主线程结束");
            Console.WriteLine($"GetStringAsync1执行结果：{task.Result}");
        }

        static async Task<string> GetStringAsync1()
        {
            Console.WriteLine(">>>>>>>>GetStringAsync1方法启动");
            string str = await GetStringAsync2();
            Console.WriteLine("<<<<<<<<GetStringAsync1方法结束");
            return str;
        }
        static async Task<string> GetStringAsync2()
        {
            Console.WriteLine(">>>>>>>>GetStringAsync2方法启动");
            string str = await GetStringFromTask();
            Console.WriteLine("<<<<<<<<GetStringAsync2方法结束");
            return str;
        }

        static Task<string> GetStringFromTask()
        {
            Console.WriteLine(">>>>GetStringFromTask方法启动");
            Task<string> task = new Task<string>(() =>
             {
                 Console.WriteLine(">>任务线程启动");
                 Thread.Sleep(1000);
                 Console.WriteLine("<<任务线程结束");
                 return "hello world";
             });
            task.Start();
            Console.WriteLine("<<<<GetStringFromTask方法结束");
            return task;
            //return "111";
        }
    }