using System;
using System.Threading.Tasks;
using System.Threading;

public class TaskCreation2_WithReturnValue
    {
        public static void Main1()
        {
            ////1.new方式实例化一个Task，需要通过Start方法启动
            Task<string> task = new Task<string>(() =>
            {
                return $"hello, task1的ID为{Thread.CurrentThread.ManagedThreadId}";
            });
            task.Start();

            ////2.Task.Factory.StartNew(Func func)创建和启动一个Task
           Task<string> task2 =Task.Factory.StartNew<string>(() =>
            {
                return $"hello, task2的ID为{ Thread.CurrentThread.ManagedThreadId}";
            });

            ////3.Task.Run(Func func)将任务放在线程池队列，返回并启动一个Task
           Task<string> task3= Task.Run<string>(() =>
            {
                return $"hello, task3的ID为{ Thread.CurrentThread.ManagedThreadId}";
            });

            Console.WriteLine($"执行主线程！{ Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine(task.Result);
            Console.WriteLine(task2.Result);
            Console.WriteLine(task3.Result);
            Console.ReadLine();
        }
        
    }