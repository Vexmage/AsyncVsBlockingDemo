using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;

public class AsyncTester
{
    private readonly Stopwatch _stopwatch = new Stopwatch();
    private static readonly HttpClient _httpClient = new HttpClient();

    // Original Blocking Method test
    public void RunBlockingTest()
    {
        Console.WriteLine("Blocking method started...");
        _stopwatch.Restart();
        Thread.Sleep(5000);  // Simulate long-running task
        _stopwatch.Stop();
        Console.WriteLine("Blocking method finished.");
        Console.WriteLine($"Blocking Method Time: {_stopwatch.ElapsedMilliseconds} ms\n");
    }

    // Original Async Method test
    public async Task RunAsyncTest()
    {
        Console.WriteLine("Async method started...");
        _stopwatch.Restart();
        await Task.Delay(5000); // Simulate async task
        _stopwatch.Stop();
        Console.WriteLine("Async method finished.");
        Console.WriteLine($"Async Method Time: {_stopwatch.ElapsedMilliseconds} ms\n");
    }

    // Original Parallel Async Execution test
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

    // Original Mixed Method Execution test
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


    // API Call (New Feature)
    public async Task RunApiCallTest()
    {
        Console.WriteLine("Fetching data from API...");
        _stopwatch.Restart();
        string data = await FetchDataAsync();
        _stopwatch.Stop();
        Console.WriteLine($"API Response (First 100 chars): {data.Substring(0, 100)}...");
        Console.WriteLine($"API Call Time: {_stopwatch.ElapsedMilliseconds} ms\n");
    }


    private async Task<string> FetchDataAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }


    // Parallel API Calls (New Feature)
    public async Task RunParallelApiCalls()
    {
        Console.WriteLine("Starting parallel API calls...");
        _stopwatch.Restart();
        Task<string> task1 = FetchDataAsync();
        Task<string> task2 = FetchDataAsync();
        Task<string> task3 = FetchDataAsync();

        string[] results = await Task.WhenAll(task1, task2, task3);
        _stopwatch.Stop();
        Console.WriteLine($"Parallel API Calls Completed in: {_stopwatch.ElapsedMilliseconds} ms\n");
    }

    // Async Method Helper (Used in Parallel Async Execution)

    private async Task AsyncMethod()
    {
        Console.WriteLine("Async method started...");
        await Task.Delay(5000);
        Console.WriteLine("Async method finished.");
    }
}
