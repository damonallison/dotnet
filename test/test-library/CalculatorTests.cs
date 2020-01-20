using Xunit;
using DamonAllison.Library;

namespace Tests
{
    public class CalculatorTests
    {
        [Fact]
        public void Add() 
        {
            Assert.Equal(4, Calculator.Add(2, 2));
        }

        /// <summary>
        /// Shows how to ignore a `Fact`. The Skip reason will be printed to console.
        /// </summary>
        [Fact(Skip = "This test is flaky")]
        public void DivideByZero() {
            Assert.Equal(0, Calculator.Divide(10, 0));
        }
    }
}
