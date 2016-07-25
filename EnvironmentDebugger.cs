using System;
using System.Collections;
using System.Linq;

namespace Dotnet.Debugging
{
    /// <summary>
    /// Provides functions for inspecting the runtime environment.
    /// </summary>
    public static class EnvironmentDebugger
    {
        public static void PrintEnvironment()
        {

            PrintInDelimiters(() => {
                PrintMachineInformation();
            });

            PrintInDelimiters(() => {
                PrintArgs();
            });

            PrintInDelimiters(() => {
                PrintEnvironmentVariables();
            });
        }

        private static void PrintMachineInformation() 
        {
            Console.WriteLine($"MachineName:{Environment.MachineName}");
            Console.WriteLine($"ProcessorCount:{Environment.ProcessorCount}");
            Console.WriteLine($"CurrentManagedThreadId:{Environment.CurrentManagedThreadId}");
        }

        private static void PrintArgs()
        {
            string[] args = Environment.GetCommandLineArgs();
            Console.WriteLine($"Command Line Arguments ({args.Count()}):");
            for (int i = 0; i < args.Count(); i++)
            {
                Console.WriteLine($"[{i}]:{args[i]}");
            }
        }

        private static void PrintEnvironmentVariables()
        {
            IDictionary vars = Environment.GetEnvironmentVariables();
            Console.WriteLine($"Environment Variables ({vars.Count}):");
            foreach(DictionaryEntry entry in vars)
            {
                Console.WriteLine($"[{entry.Key}] = \"{entry.Value}\"");
            }
        }

        public static void PrintFundamentalTypes() 
        {
            // sbyte = 8 bits (signed)
            Console.WriteLine($"sbyte = {sbyte.MinValue - sbyte.MaxValue}");
            // byte = 8 bits
            Console.WriteLine($"byte = {byte.MinValue - byte.MaxValue}");
        }

        private static void PrintInDelimiters(Action action) 
        {
            PrintDelimiter();
            action();
            PrintDelimiter();
        }

        private static void PrintDelimiter(int count = 80) 
        {
            count = Math.Min(40, count);
            for (int i = 0; i < 5; i++) {
                Console.Write("-");
            }
            for (int i = 0; i < 80; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }
    }
}