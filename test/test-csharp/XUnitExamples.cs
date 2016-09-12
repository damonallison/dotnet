using System;
using Xunit;

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
    /// </summary>
    public class XUnitExamples
    {
        /// <summary>
        /// This will currently write to the output window *only*
        /// when debugging the test. Need to determine how to send
        /// output to console when running `dotnet test`.
        /// </summary>
        [Fact]
        public void WriteToConsole()
        {
            System.Diagnostics.Debug.WriteLine("Hello, world");
        }
    }
}
