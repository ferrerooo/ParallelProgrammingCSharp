  using System;
  using System.Threading.Tasks;
  using System.Threading;

namespace IntroducingTasks
{
  public class IntroducingTasks
  {
    public static void Main1()
    {
      //CreateAndStartSimpleTasks();

      //TasksWithState();

      TasksWithReturnValues();

      Console.WriteLine("Main program done, press any line.");
      Console.ReadLine();
    }

    public static void CreateAndStartSimpleTasks()
    {
      Console.WriteLine("== CreateAndStartSimpleTasks() ==");

      Task.Factory.StartNew(() =>
      {
        Write('-');
      });

      Task t = new Task(() => Write('?'));
      t.Start();

      Write('.');
    }

    public static void TasksWithState()
    {
      Console.WriteLine("== TasksWithState() ==");
      // clumsy 'object' approach
      Task t = new Task(Write, "foo");
      t.Start();
      Task.Factory.StartNew(Write, "bar");
    }

    private static void TasksWithReturnValues()
    {
      Console.WriteLine("== TasksWithReturnValues() ==");

      string text1 = "testing", text2 = "this";
      var task1 = new Task<int>(TextLength, text1);
      task1.Start();
      var task2 = Task.Factory.StartNew(TextLength, text2);

      // getting the result is a blocking operation!
      Console.WriteLine($"Length of '{text1}' is {task1.Result}.");
      Console.WriteLine($"Length of '{text2}' is {task2.Result}.");
    }

    private static void Write(char c)
    {
      int i = 1000;
      while (i-- > 0)
      {
        Console.Write(c);
      }
    }

    private static void Write(object s)
    {
      int i = 100;
      while (i-- > 0)
      {
        Console.Write(s.ToString());
      }
    }

    private static int TextLength(object o)
    {
      Console.WriteLine($"\nTask with id {Task.CurrentId} processing object '{o}'...");
      return o.ToString().Length;
    }

    // Summary:

    // 1. Two ways of using tasks
    //    Task.Factory.StartNew() creates and starts a Task
    //    new Task(() => { ... }) creates a task; use Start() to fire it
    // 2. Tasks take an optional 'object' argument
    //    Task.Factory.StartNew(x => { foo(x) }, arg);
    // 3. To return values, use Task<T> instead of Task
    //    To get the return value. use t.Result (this waits until task is complete)
    // 4. Use Task.CurrentId to identify individual tasks.


    // to run the code, use: dotnet run Program.cs 
  }
}
