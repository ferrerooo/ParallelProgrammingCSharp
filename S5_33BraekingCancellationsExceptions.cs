
using System;
using System.CodeDom;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelLoops
{
  class BreakingAndStoppingDemo
  {
    public static void Demo()
    {
      var cts = new CancellationTokenSource();
      Console.WriteLine("--here1--");     
      var po = new ParallelOptions {CancellationToken = cts.Token};
      ParallelLoopResult result = Parallel.For(0, 20, po, (int x, ParallelLoopState state) =>
      {
        Console.Write($"{x}[{Task.CurrentId}]\t");
        if (x == 10)
        {
          //cts.Cancel(); // throw OperationCanceledException at Demo() level
          //throw new Exception(); // execution stops on exception
          //state.Stop(); // stop execution as soon as possible
          state.Break(); // request that loop stop execution of iterations beyond current iteration asap
        }
        if (state.IsExceptional)
          Console.Write($"EX[{Task.CurrentId}]\t");
        else
          Console.Write($"NoEx\t");
        // state.LowestBreakIteration, ShouldExitCurrentIteration
      });

      Console.WriteLine("--here2--");
      Console.WriteLine($"Was loop completed? {result.IsCompleted}"); // uncomment break
      if (result.LowestBreakIteration.HasValue)
        Console.WriteLine($"Lowest break iteration: {result.LowestBreakIteration}");
    }

    static void Main(string[] args)
    {
      try
      {
        Demo();
      }
      catch (OperationCanceledException) 
      {
        Console.WriteLine("OperationCanceledException");
      }
      catch (AggregateException ae)
      {
        ae.Handle(e =>
        {
          Console.WriteLine(e.Message);
          return true;
        });
      }
    }
  }
}
