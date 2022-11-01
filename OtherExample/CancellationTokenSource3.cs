using System;
using System.Threading.Tasks;
using System.Threading;

public class CancellationTokenSource3
    {
        public static void Main1()
        {
            CancellationTokenSource source = new CancellationTokenSource();
            //注册任务取消的事件
            source.Token.Register(() =>
            {
                Console.WriteLine("任务被取消后执行xx操作！");
            });

            int index = 0;
            //开启一个task执行任务
            Task task1 = new Task(() =>
              {
                  while (!source.IsCancellationRequested)
                  {
                      Thread.Sleep(1000);
                      Console.WriteLine($"第{++index}次执行，线程运行中...");
                  }
                  Console.WriteLine("Is Stopeed");
              });
            task1.Start();
            //延时取消，效果等同于Thread.Sleep(5000);source.Cancel();
            source.CancelAfter(5000);
            Console.ReadLine();
        }
        
    }