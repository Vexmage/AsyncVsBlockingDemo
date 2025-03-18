using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Microsoft.EntityFrameworkCore;

public class AsyncTester
{
    private readonly Stopwatch _stopwatch = new Stopwatch();
    private static readonly HttpClient _httpClient = new HttpClient();
    private readonly DatabaseContext _dbContext = new DatabaseContext();

    public async Task InitializeDatabaseAsync()
    {
        Console.WriteLine("Initializing Database...");
        await _dbContext.Database.EnsureCreatedAsync();
        await _dbContext.InitializeDatabaseAsync();
        Console.WriteLine("Database Ready!\n");
    }

    // Save API Response to SQLite
    public async Task SaveApiResponseToDatabaseAsync()
    {
        Console.WriteLine("Fetching data from API and saving to database...");
        _stopwatch.Restart();

        string data = await FetchDataAsync();
        if (string.IsNullOrEmpty(data) || data.StartsWith("API Error") || data.StartsWith("🚨"))
        {
            Console.WriteLine($"API call failed, not saving to database.\n");
            return;
        }

        var response = new ApiResponse
        {
            Data = data.Substring(0, 500), // Store only first 500 chars
            RetrievedAt = DateTime.UtcNow
        };

        await _dbContext.Database.EnsureCreatedAsync();
        _dbContext.ApiResponses.Add(response);
        await _dbContext.SaveChangesAsync();

        _stopwatch.Stop();
        Console.WriteLine($"API Response Saved! (Time: {_stopwatch.ElapsedMilliseconds} ms)\n");
    }

    // Retrieve API Responses from SQLite
    public async Task RetrieveApiResponsesAsync()
    {
        Console.WriteLine("📤 Retrieving stored API responses from database...");
        _stopwatch.Restart();

        var responses = await _dbContext.ApiResponses.ToListAsync();
        _stopwatch.Stop();

        if (responses.Count == 0)
        {
            Console.WriteLine("No data found in the database.\n");
        }
        else
        {
            foreach (var response in responses)
            {
                Console.WriteLine($"📝 ID: {response.Id} | Retrieved: {response.RetrievedAt} | Data (first 100 chars): {response.Data.Substring(0, 100)}...\n");
            }
        }

        Console.WriteLine($"⏳ Database Query Time: {_stopwatch.ElapsedMilliseconds} ms\n");
    }


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


    // API Call 
    public async Task RunApiCallTest()
    {
        Console.WriteLine("Fetching data from API...");
        _stopwatch.Restart();
        try
        {
            string data = await FetchDataAsync();
            _stopwatch.Stop();
            Console.WriteLine($"API Response (First 100 chars): {data.Substring(0, 100)}...");
            Console.WriteLine($"API Call Time: {_stopwatch.ElapsedMilliseconds} ms\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"API call failed: {ex.Message}\n");
        }
    }


    // Parallel API Calls 
    public async Task RunParallelApiCalls()
    {
        Console.WriteLine("Starting parallel API calls...");
        _stopwatch.Restart();
        try
        {
            Task<string> task1 = FetchDataAsync();
            Task<string> task2 = FetchDataAsync();
            Task<string> task3 = FetchDataAsync();

            string[] results = await Task.WhenAll(task1, task2, task3);
            _stopwatch.Stop();
            Console.WriteLine($"Parallel API Calls Completed in: {_stopwatch.ElapsedMilliseconds} ms\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"One or more API calls failed: {ex.Message}\n");
        }
    }
    private async Task<string> FetchDataAsync()
    {
        string apiUrl = "https://jsonplaceholder.typicode.com/posts"; // Replace with a real API

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            // Handle unsuccessful responses
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"API returned {response.StatusCode} ({(int)response.StatusCode})");
                return $"API Error: {response.StatusCode}";
            }

            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.RequestTimeout)
        {
            return "API request timed out.";
        }
        catch (HttpRequestException ex)
        {
            return $"🚨 Network or API failure: {ex.Message}";
        }
        catch (TaskCanceledException)
        {
            return "API call was canceled (possible timeout).";
        }
        catch (Exception ex)
        {
            return $"Unexpected error: {ex.Message}";
        }
    }


    // Async Method Helper (Used in Parallel Async Execution)

    private async Task AsyncMethod()
    {
        Console.WriteLine("Async method started...");
        await Task.Delay(5000);
        Console.WriteLine("Async method finished.");
    }
}
