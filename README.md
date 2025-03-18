# Async vs Blocking in C#

## Overview
This project demonstrates the difference between **blocking (`Thread.Sleep`)** and **non-blocking (`async/await`)** execution in C#. It provides a side-by-side comparison of:
- A **blocking method** that halts execution (`Thread.Sleep`).
- An **async method** that runs non-blocking tasks (`Task.Delay`).
- A **parallel async execution** that utilizes `Task.WhenAll()`.
- A **mixed method** that combines both blocking and async behavior.

## Why This Matters
Understanding **async programming** is crucial for building **efficient, responsive applications** in .NET. This project helps visualize:
- How **blocking code delays execution**.
- How **async code improves responsiveness**.
- Why **mixing blocking and async code** can create performance issues.

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
Blocking Method Time: 5000 ms

Async method started...
Async method finished.
Async Method Time: 5000 ms

Async method started...
Async method started...
Async method finished.
Async method finished.
Parallel Async Execution Time: 5000 ms

Mixed method started...
Mixed method finished.
Mixed Method Time: 10000 ms
