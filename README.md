# Async vs Blocking in C#

## Overview
This project demonstrates the difference between **blocking (`Thread.Sleep`)** and **non-blocking (`async/await`)** execution in C#. It provides a side-by-side comparison of:
- A **blocking method** that halts execution (`Thread.Sleep`).
- An **async method** that runs non-blocking tasks (`Task.Delay`).
- A **parallel async execution** that utilizes `Task.WhenAll()`.
- A **mixed method** that combines both blocking and async behavior.
- A **real-world API call** that demonstrates async HTTP requests.
- **Parallel API calls** to show how multiple requests can be handled concurrently.


## Why This Matters
Understanding **async programming** is crucial for building **efficient, responsive applications** in .NET. This project helps visualize:
- How **blocking code** delays execution.
- How **async code** improves responsiveness.
- Why **mixing blocking and async code** can create performance issues.
- How **real-world async tasks (API calls)** behave in .NET.

## Running the Project
### **Prerequisites**
- .NET 6.0 or later
- Visual Studio 2022 (or any C# IDE)

### **Setup & Run**
1. Clone the repository:
   ```sh
   git clone https://github.com/yourusername/AsyncVsBlockingDemo.git
   cd AsyncVsBlockingDemo

2. Open in Visual Studio and run the project

### Expected Output:
Starting comparison...

Blocking method started...
Blocking method finished.
Blocking Method Time: 5010 ms

Async method started...
Async method finished.
Async Method Time: 5000 ms

Starting parallel async execution...
Async method started...
Async method started...
Async method finished.
Async method finished.
Parallel Async Execution Time: 5013 ms

Mixed method started...
Mixed method finished.
Mixed Method Time: 10019 ms

Fetching data from API...
API Response (First 100 chars): [
  {
    "userId": 1,
    "id": 1,
    "title": "sunt aut facere repellat provident occaecati excepturi optio reprehenderit"...
API Call Time: 410 ms

Starting parallel API calls...
Parallel API Calls Completed in: 173 ms

Done!


## How It Works
- Blocking Method() -- Uses Thread.Sleep(5000), pausing execution.
- AsyncMethod() -- Uses Task.Delay(5000), allowing non-blocking execution.
- Task.WhenAll(AsyncMethod, AsyncMethod) -- Runs multiple async tasks in parallel, completing in the same time as one.
- MixedMethod() -- Uses both Thread.Sleep and Task.Delay, leading to inefficient execution.
- RunApiCallTest() -- Uses HttpClient.GetAsync() to fetch real API data asynchronously.
- RunParallelApiCalls() -- Runs multiple API calls in parallel, showcasing async network performance.

## Key Takeaways
- Avoid Thread.Sleep in real-world applications – it completely blocks execution.
- Use Task.Delay for async, non-blocking operations to keep apps responsive.
- Run async tasks in parallel (Task.WhenAll) to improve performance.
- Mixing blocking & async negates async benefits – be mindful when combining them.
- Async API calls can greatly reduce execution time when parallelized.

### Contributing
Feel free to fork the project and submit a pull request with improvements! 
