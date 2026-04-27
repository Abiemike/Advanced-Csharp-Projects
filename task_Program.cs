using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace TasksBasicsDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            #region TaskBasics

            //void TaskAction() => Console.WriteLine("I am running in a Task!");

            //var task = new Task(TaskAction);
            //Console.WriteLine($"Task status is: {task.Status}");
            //task.Start();
            //Console.WriteLine($"Task status is: {task.Status}");
            //Thread.Sleep(100);
            //Console.WriteLine($"Task status is: {task.Status}");

            #endregion

            #region UsingConstructor

            //var task = new Task(() =>
            //{
            //    Console.WriteLine("Task started");
            //    Thread.Sleep(10000);
            //    Console.WriteLine("Task completed");
            //});
            //task.Start();

            ////Thread.Sleep(12000);
            //task.Wait(); // <-- Block the main thread until task is complete - try removing this to see what happens!

            #endregion

            #region TaskFactory

            //var name = "World";
            //var greeter = Task.Factory.StartNew((name) =>
            //{
            //    Console.WriteLine($"Hello");
            //    Thread.Sleep(500);
            //    Console.WriteLine($".");
            //    Thread.Sleep(500);
            //    Console.WriteLine($".");
            //    Thread.Sleep(500);
            //    Console.WriteLine($".");
            //    Thread.Sleep(500);
            //    Console.WriteLine($"{name}!");
            //}, name);
            //greeter.Wait(); // <-- Block the main thread until task is complete


            #endregion

            #region TaskRun

            //var outsideVariable = "Earth";
            //var infoTask = Task.Run(() =>

            //    Console.WriteLine(
            //        $"I am task #{Task.CurrentId} with thread #{Thread.CurrentThread.ManagedThreadId}" +
            //        $"and I can see {outsideVariable}"
            //    )
            //);

            //infoTask.Wait(); //Blocking the main thread

            #endregion

            #region TaskRunSynchronously

            //Console.WriteLine($"Main thread's ID is: {Thread.CurrentThread.ManagedThreadId}");
            //var syncTask = new Task(() => Console.WriteLine($"Sync task's thread ID is: " +
            //                                                $"{Thread.CurrentThread.ManagedThreadId}"));

            //syncTask.RunSynchronously();
            //syncTask.Wait();

            #endregion

            #region ObtainingResultsFromTasks

            //var myTask = Task<int>.Run(() =>
            //{
            //    // Just loop.
            //    int max = 1000000;
            //    int ctr = 0;
            //    for (ctr = 0; ctr <= max; ctr++)
            //    {
            //        if (ctr == max / 2 && DateTime.Now.Hour <= 12)
            //        {
            //            ctr++;
            //            break;
            //        }
            //    }
            //    return ctr;
            //});
            //Console.WriteLine("Finished {0:N0} iterations.", myTask.Result);

            //Using task factory
            //var t = Task<int>.Factory.StartNew(() =>
            //{
            //    // Just loop.
            //    int max = 1000000;
            //    int ctr = 0;
            //    for (ctr = 0; ctr <= max; ctr++)
            //    {
            //        if (ctr == max / 2 && DateTime.Now.Hour <= 12)
            //        {
            //            ctr++;
            //            break;
            //        }
            //    }
            //    return ctr;
            //});
            //Console.WriteLine("Finished {0:N0} iterations.", t.Result);

            #endregion

            #region TaskContinuation

            //Func<Guid> uploader = () =>
            //{
            //    Console.WriteLine("   Uploader is uploading the document to a blob storage...");
            //    Thread.Sleep(3000);
            //    Console.WriteLine($"Current thread's ID is: {Thread.CurrentThread.ManagedThreadId}");
            //    return Guid.NewGuid();
            //};

            //Func<Task<Guid>, string> converter = (antecedent) =>
            //{
            //    Console.WriteLine($"Current thread's ID is: {Thread.CurrentThread.ManagedThreadId}");
            //    Console.WriteLine($"   Converter is converting document {antecedent.Result} to text...");
            //    Thread.Sleep(3000);
            //    return "Some very long text that...";
            //};

            //Action<Task<string>> indexer = (antecedent) =>
            //{
            //    Console.WriteLine($"Current thread's ID is: {Thread.CurrentThread.ManagedThreadId}");
            //    Console.WriteLine($"   Indexing text: {antecedent.Result}...");
            //    Thread.Sleep(3000);
            //};

            //Console.WriteLine("Main thread: processing a new document...");
            //Console.WriteLine($"Main thread's ID is: {Thread.CurrentThread.ManagedThreadId}");

            //Task.Factory
            //    .StartNew(uploader)
            //    .ContinueWith(converter)
            //    .ContinueWith(indexer)
            //    .Wait();
            //Console.WriteLine("Main thread: completed processing.");

            #endregion

            #region TaskCancellation

            //var tokenSource = new CancellationTokenSource();
            //var token = tokenSource.Token;
            //Action<int, CancellationToken> counterAction = (max, ct) =>
            //{
            //    for (var i = 1; i <= max; i++)
            //    {
            //        if (ct.IsCancellationRequested)
            //        {
            //            Console.WriteLine("   Cancellation requested. Returning");
            //            return;
            //        }
            //        Console.WriteLine($"   {i}");
            //        Thread.Sleep(1500);
            //    }
            //};
            //Console.WriteLine("Calling counter to count to 10");
            //var counterTask = Task.Run(() => counterAction(10, token), token);
            //Thread.Sleep(8000);
            //tokenSource.Cancel();
            //counterTask.Wait();
            //Console.WriteLine("Done!");

            #endregion

            #region ParallelFor

            //Console.WriteLine("Spawning 10 tasks in parallel:");
            //ParallelLoopResult result = Parallel.For(0, 10000, iteration =>
            //    Console.WriteLine(
            //        $"   Iteration: {iteration}, task #{Task.CurrentId}, thread #{Thread.CurrentThread.ManagedThreadId} DateTime {DateTime.Now}"
            //    )
            //);
            //Console.WriteLine($"Is complete: {result.IsCompleted}");
            //Console.WriteLine("Done!");

            //Console.WriteLine("Spawning 30 tasks in parallel:");
            //ParallelLoopResult result2 = Parallel.For(10, 10000000, (int i, ParallelLoopState pls) =>
            //{
            //    Console.WriteLine(
            //        $"   Iteration: {i}, task #{Task.CurrentId}, thread #{Thread.CurrentThread.ManagedThreadId}"
            //    );
            //    Task.Delay(5000);
            //    if (i > 15)
            //    {
            //        Console.WriteLine($"   Breaking on iteration {i} for thread {Thread.CurrentThread.ManagedThreadId}");
            //        pls.Break();
            //    }
            //});
            ////Console.WriteLine("Is completed: {0}", result.IsCompleted);
            //Console.WriteLine($"Is complete: {result2.IsCompleted}");
            //Console.WriteLine("Done!");

            #endregion

            #region ParellelForEach

            //Console.WriteLine("Running 13 parallel tasks to display numbers from zero to twelve:");
            //var data = new[]
            //{
            //    "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve"
            //};

            //var result = Parallel.ForEach<string>(data, s =>
            //{
            //    Console.WriteLine($"   {s}");
            //});
            //Console.WriteLine($"Is complete: {result.IsCompleted}");
            //Console.WriteLine("Done!");

            #endregion

            #region ParellelInvoke

            Action action1 = () =>
            {
                Console.WriteLine($"Thread Id: {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine($"   Start of first task");
                Thread.Sleep(10000);
                Console.WriteLine($"   End of first task");
            };

            Action action2 = () =>
            {
                Console.WriteLine($"Thread Id: {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine($"   Start of second task");
                Thread.Sleep(4000);
                Console.WriteLine($"   End of second task");
            };

            Action action3 = () =>
            {
                Console.WriteLine($"Thread Id: {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine($"   Start of third task");
                Thread.Sleep(2000);
                Console.WriteLine($"   End of third task");
            };

            Action action4 = () =>
            {
                Console.WriteLine($"Thread Id: {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine($"   Start of fourth task");
                Thread.Sleep(8000);
                Console.WriteLine($"   End of fourth task");
            };

            Parallel.Invoke(action1, action4, action2, action3);

            #endregion
        }
    }
}