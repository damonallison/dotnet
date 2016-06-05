using System;

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
            var t = new Types();
            var c = new Calculator();

            Console.WriteLine($"Hello World : {t}");
        }
    }
}
