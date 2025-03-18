using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting comparison...\n");

        AsyncTester tester = new AsyncTester();

        tester.RunBlockingTest();
        await tester.RunAsyncTest();
        await tester.RunParallelAsyncTest();
        await tester.RunMixedTest();

        Console.WriteLine("Done!");
    }
}
