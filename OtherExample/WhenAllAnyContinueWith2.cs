using System;
using System.Threading.Tasks;
using System.Threading;

/*
WhenAll/WhenAny方法不会阻塞主线程
*/

public class WhenAllAnyContinueWith2
    {
        public static void Main1()
        {
            Task task1 = new Task(() => {
                Thread.Sleep(500);
                Console.WriteLine("线程1执行完毕！");
            });
            task1.Start();
            Task task2 = new Task(() => {
                Thread.Sleep(1000);
                Console.WriteLine("线程2执行完毕！");
            });
            task2.Start();
            //通过TaskFactroy实现
            Task.Factory.ContinueWhenAll(new Task[] { task1, task2 }, (t) =>
            {
                Thread.Sleep(100);
                Console.WriteLine("执行后续操作");
            });

            Console.WriteLine("主线程执行完毕！");
            Console.ReadLine();
        }
        
    }