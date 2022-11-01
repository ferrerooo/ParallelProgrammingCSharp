using System;
using System.Threading.Tasks;
using System.Threading;

/*
WhenAll/WhenAny方法不会阻塞主线程
*/

public class WhenAllAnyContinueWith
    {
        public static void Main1()
        {
            Task task1 = new Task(() => {
                Thread.Sleep(500);
                Console.WriteLine($"线程1执行完毕！{Thread.CurrentThread.ManagedThreadId}");
            });
            task1.Start();
            Task task2 = new Task(() => {
                Thread.Sleep(1000);
                Console.WriteLine($"线程2执行完毕！{Thread.CurrentThread.ManagedThreadId}");
            });
            task2.Start();
            
            //task1，task2执行完了后执行后续操作
            var t = Task.WhenAll(task1, task2);
            
            t.ContinueWith((t) => {
                Thread.Sleep(100);
                Console.WriteLine($"执行后续操作完毕！{Thread.CurrentThread.ManagedThreadId}");
            });

            Console.WriteLine($"主线程执行完毕！{Thread.CurrentThread.ManagedThreadId}");
            Console.ReadLine();
        }
        
    }