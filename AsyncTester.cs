using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

public class AsyncTester
{
    private readonly Stopwatch _stopwatch = new Stopwatch();

    public void RunBlockingTest()
    {
        Console.WriteLine("Blocking method started...");
        _stopwatch.Restart();
        Thread.Sleep(5000);  // Simulate long-running task
        _stopwatch.Stop();
        Console.WriteLine("Blocking method finished.");
        Console.WriteLine($"Blocking Method Time: {_stopwatch.ElapsedMilliseconds} ms\n");
    }

    public async Task RunAsyncTest()
    {
        Console.WriteLine("Async method started...");
        _stopwatch.Restart();
        await Task.Delay(5000); // Simulate async task
        _stopwatch.Stop();
        Console.WriteLine("Async method finished.");
        Console.WriteLine($"Async Method Time: {_stopwatch.ElapsedMilliseconds} ms\n");
    }

    public async Task RunParallelAsyncTest()
    {
        Console.WriteLine("Starting parallel async execution...");
        _stopwatch.Restart();
        Task task1 = AsyncMethod();
        Task task2 = AsyncMethod();
        await Task.WhenAll(task1, task2);
        _stopwatch.Stop();
        Console.WriteLine($"Parallel Async Execution Time: {_stopwatch.ElapsedMilliseconds} ms\n");
    }

    public async Task RunMixedTest()
    {
        Console.WriteLine("Mixed method started...");
        _stopwatch.Restart();
        Thread.Sleep(5000);  // Blocking part
        await Task.Delay(5000); // Async part
        _stopwatch.Stop();
        Console.WriteLine("Mixed method finished.");
        Console.WriteLine($"Mixed Method Time: {_stopwatch.ElapsedMilliseconds} ms\n");
    }

    private async Task AsyncMethod()
    {
        Console.WriteLine("Async method started...");
        await Task.Delay(5000);
        Console.WriteLine("Async method finished.");
    }
}
