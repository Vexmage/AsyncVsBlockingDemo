using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting comparison..");

        // Measure blocking execution time
        Stopwatch stopwatch = Stopwatch.StartNew();
        BlockingMethod();
        stopwatch.Stop();
        Console.WriteLine($"Blocking Method Time: {stopwatch.ElapsedMilliseconds} ms");

        // Measure async execution time
        stopwatch.Restart();
        await AsyncMethod();
        stopwatch.Stop();
        Console.WriteLine($"Async Method Time: {stopwatch.ElapsedMilliseconds} ms");

        // Demonstrate parallel async execution
        stopwatch.Restart();
        Task task1 = AsyncMethod();
        Task task2 = AsyncMethod();
        await Task.WhenAll(task1, task2);
        stopwatch.Stop();
        Console.WriteLine($"Parallel Async Execution Time: {stopwatch.ElapsedMilliseconds} ms");

        // Demonstrate mixed method
        stopwatch.Restart();
        await MixedMethod();
        stopwatch.Stop();
        Console.WriteLine($"Mixed Method Time: {stopwatch.ElapsedMilliseconds} ms");

        Console.WriteLine("Done!");
    }

    static void BlockingMethod()
    {
        Console.WriteLine("Blocking method started...");
        Thread.Sleep(5000);  // Simulate long-running task (3 seconds, then try other times)
        Console.WriteLine("Blocking method finished");
    }

    static async Task AsyncMethod()
    {
        Console.WriteLine("Async method started...");
        await Task.Delay(5000); // Simulate async task (3 seconds, then try other times)
        Console.WriteLine("Async method finished");
    }

    static async Task MixedMethod()
    {
        Console.WriteLine("Mixed method started...");
        Thread.Sleep(5000);  // Blocking part
        await Task.Delay(5000); // Async part
        Console.WriteLine("Mixed method finished.");
    }


}