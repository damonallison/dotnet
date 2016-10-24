using Xunit;

namespace DamonAllison.CSharpTests.FunctionalProgramming
{

    /// <summary>
    /// Anonymous types are data structures created on the fly rather
    /// than a class declaration.
    ///
    /// Anonymous types are useful for creating ephemeral data structures
    /// for use within a function or when dealing with lambdas.
    ///
    /// While it is possible to delcare everything as <c>var</c> and
    /// let the static analyzer handle type assignment, it does not make
    /// the code more clear. Specifically typing each variable makes the
    /// code more readable and is preferred to using <c>var</c> for all
    /// variable delcarations.
    ///
    /// In cases where the type is obvious, it is OK to use <c>var</c>.
    ///
    /// <code>
    /// // Here, it is obvious what type `d` is. Using var is OK.
    /// var d = new Dictionary<string, List<Account>>();
    ///
    /// // Here, it is *not* obvious what type `d` is, do not use var.
    /// Dictionary<string, List<Account>> d = GetAccounts();
    /// </code>
    ///
    /// Var does *not* mean weakly typed (like void* in C). `var` means
    /// the type will be assigned by the compiler. The variable is strongly
    /// typed, is type checked at runtime, and the type cannot change.
    ///
    /// Where as with `void *`, the variable can obviously change type at
    /// runtime, you cannot type check at compile time, and a whole host of
    /// other fun things that dynamic types allow.
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

            // Anonymous types are immutable.
            // p1.Name = "Allison";

        }
    }
}
