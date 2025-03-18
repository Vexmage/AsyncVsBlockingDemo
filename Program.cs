﻿using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting comparison...\n");

        AsyncTester tester = new AsyncTester();

        // Original tests
        tester.RunBlockingTest();
        await tester.RunAsyncTest();
        await tester.RunParallelAsyncTest();
        await tester.RunMixedTest();

        // New API Call tests
        await tester.RunApiCallTest();
        await tester.RunParallelApiCalls();

        // Save API Response to Database
        await tester.SaveApiResponseToDatabaseAsync();

        // Retrieve API Responses from Database
        await tester.RetrieveApiResponsesAsync();


        Console.WriteLine("Done!");
    }
}
