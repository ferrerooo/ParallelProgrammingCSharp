using System;
using System.Threading.Tasks;
using System.Threading;

public class CancellationTokenSource2
    {
        public static void Main1()
        {
            CancellationTokenSource source = new CancellationTokenSource();
            int index = 0;
            //开启一个task执行任务
            Task task1 = new Task(() =>
              {
                  while (!source.IsCancellationRequested)
                  {
                      Thread.Sleep(1000);
                      Console.WriteLine($"第{++index}次执行，线程运行中...");
                  }
                  Console.WriteLine($"Is Stopped");
              });
            task1.Start();
            //五秒后取消任务执行
            Thread.Sleep(5000);
            //source.Cancel()方法请求取消任务，IsCancellationRequested会变成true
            source.Cancel();
            Console.ReadLine();
        }
        
    }