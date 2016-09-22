using System;
using Xunit;
using Xunit.Abstractions;

namespace DamonAllison.CSharp
{
    /// <summary>
    /// This class gives examples and provides documentation on how to use xUnit. 
    /// 
    /// <list type="bullet">
    /// <listheader>
    ///     <description>Helpful xUnit related links</description>
    /// </listheader>
    /// <item>
    ///     <description><see href="https://github.com/xunit/xunit">xUnit</see></description>
    /// </item>
    /// </list>
    /// 
    /// <code>Fact</code>s are tests which are always true. They test invariant conditions.
    /// <code>Theory</code>ies are tests which only return true for a particular set of data.
    /// 
    /// TODO:
    /// * Generate documentation comments from source code.
    /// * 
    /// * How to print the name of each test start / execution / end so any `Console.WriteLine` statements are part of the output. 
    /// </summary>
    public class XUnitExamples
    {
        private readonly ITestOutputHelper output;

        public XUnitExamples(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// This test shows how to write output in a test. 
        /// 
        /// Writing output to Console or Debug is not appropriate with xUnit 2, 
        /// which runs tests in parallel. 
        /// 
        /// The ITestOutputHelper class will gather output by test case.
        /// Tooling will display the output as part of the test's execution results.
        /// 
        /// See:
        /// https://xunit.github.io/docs/capturing-output.html
        /// </summary>
        [Fact]
        public void WriteToConsole()
        {
            ExecuteInColor(ConsoleColor.Yellow, () => {
                Console.WriteLine("Hello, colorful world");
            });
            Console.ForegroundColor = ConsoleColor.Yellow;
            
            output.WriteLine($"Hello, from an xUnit test");
        }

        public void ExecuteInColor(ConsoleColor color, Action thunk) {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            thunk();
            Console.ForegroundColor = originalColor;
        }
    }
}
