﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Dotnet.Debugging;

//
// TODO: How to link libraries?
//
// using DamonAllison.Library;

/// <summary>
/// TODO
/// * Logging framework (time, thread, function name, severity) - like Cocoa Lumberjack.
/// * Practice task creation. Why can you simply `return x + y` and not return a task object?
/// * Parallel for.
/// * Exception handling (write a task which throws an exception).
/// * async / await / promises
/// </summary>
namespace Dotnet
{
    /// <summary>
    /// Main entry point into the application.
    /// <seealso cref="Calculator"/>
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            // Console.WriteLine($"2 + 2 = {Calculator.Add(2, 2)}");
            // return;

            EnvironmentDebugger.PrintEnvironment();
            EnvironmentDebugger.PrintFundamentalTypes();

            var t = new Tasks();

            Debug.WriteLine("Add task starting");
            Task<decimal> addTask = t.AddWithDelayAsync(2M, 2M, 2000);
            addTask.Wait();

            Debug.Assert(addTask.IsCompleted);
            Debug.Assert(!addTask.IsFaulted);

            Console.WriteLine($"Hello World : {addTask.Result}");
        }

        /// <summary>
        /// <c>true</c>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static int Test(int x, int y) {
            return x + y;
        }

    }
}
