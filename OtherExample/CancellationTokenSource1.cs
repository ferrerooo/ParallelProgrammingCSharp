using System;
using System.Threading.Tasks;
using System.Threading;

public class CancellationTokenSource1
    {
        public static void Main1()
        {
            bool isStop = false;
            int index = 0;
            //开启一个线程执行任务
            Thread th1 = new Thread(() =>
              {
                  while (!isStop)
                  {
                      Thread.Sleep(1000);
                      Console.WriteLine($"第{++index}次执行，线程运行中...");
                  }
                  Console.WriteLine($"Is Stopeed...");
              });
            th1.Start();
            //五秒后取消任务执行
            Thread.Sleep(5000);
            isStop = true;
            Console.ReadLine();
        }
        
    }