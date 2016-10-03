using Xunit;

namespace DamonAllison.CSharpTests.FunctionalProgramming
{

    /// <summary>
    /// Anonymous types are data structures created on the fly rather
    /// than a class declaration. 
    /// 
    /// Anonymous types are useful for creating ephemeral data structures
    /// for use within a function or when dealing with lambdas.
    /// </summary>
    public class AnonymousTypesTests
    {
        [Fact]
        public void AnonymousTypes() 
        {
            var p1 = new 
            {
                Name = "Damon", 
                Anything = 123
            };
            Assert.Equal("Damon", p1.Name);
        }
    }
} 
