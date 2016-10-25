using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

using DamonAllison.CSharpTests.Objects;

namespace DamonAllison.CSharpTests {

    /// <summary>
    /// These tests show the foundations for working with .NET
    /// collection classes, like <c>IEnumerable<T></c>.
    ///
    /// In .NET, there are two versions of collection classes : generic
    /// and non-generic. Only use the non-generic collection classes when
    /// dealing with legacy code.
    /// </summary>
    public class CollectionTests {

        /// <summary>
        /// Collection initializers are syntactic shortcuts which allow you
        /// to populate a collection during initialization by providing
        /// a set of values.
        /// </summary>
        [Fact]
        public void CollectionInitializerTests() {

            string firstName = "test";

            // Note the constructor parens are optional (omitted here)
            // when using a default constructor.
            // We could have written this as new List<string>() { "damon" ... };
            List<string> names = new List<string> {
                "damon",
                "kari",
                "grace",
                "lily",
                "cole",
                firstName
            };
            Assert.True(names.Any(x => x.Equals("damon")));

            // Dictionary initializer syntax
            var d = new Dictionary<string, Type> {
                ["e"] = typeof(System.Exception),
                ["t"] = typeof(System.Type)
            };
            Assert.Equal(d["t"], typeof(System.Type));
        }

        /// <summary>
        /// `IEnumerable` is the minimum interface required to iterate a collection.
        ///
        /// What is `IEnumerable`? IEnumerable is an interface which supports iterating
        /// over a collection. Different collections support iteration in
        /// different ways. When looping over an array, the .Length property
        /// can be used, as well as the indexer [] property.
        ///
        /// When looping over a stack or queue, where you don't know the length or
        /// have access to the indexer[] property, you need a different mechanism
        /// to access elements in the collection.
        ///
        /// IEnumerable is a general approach for iterating a collection using the
        /// iterator pattern. <c>IEnumerator<T></c> provides a generic interface
        /// for keeping track of the current position, accessing the current element,
        /// and moving between elements.
        ///
        /// Note that .Reset() on an IEnumerator usually throws a
        /// `NotImplementedException` and should not be called. If you need a
        /// new enumerator, create a fresh enumerator with <c>GetEnumerator</c>.
        ///
        /// Collections implement <c>IEnumerable<T></c> rather than
        /// <c>IEnumerator<T></c> in order to provide a factory for creating
        /// <c>IEnumerator<T></c> instances. This allows a collection to have
        /// multiple instances of an enumerator enumerating independenty of
        /// each other.
        /// </summary>
        [Fact]
        public void IEnumerableTest() {

            System.Collections.Generic.Stack<int> s =
                new System.Collections.Generic.Stack<int>();
            s.Push(1);
            s.Push(2);

            List<int> items = new List<int>();

            // Uses IEnumerable.GetEnumerator() to return an enumerator instance
            // to use with `foreach`.
            foreach(int i in s) {
                items.Add(i);
            }

            // The above code compiles into...

            // using (System.Collections.Generic.Stack<int>.Enumerator enumerator = s.GetEnumerator()) {
            //     while (enumerator.MoveNext())
            //     {
            //         items.Add(enumerator.Current);
            //     }
            // }

            Assert.Equal(new List<int> { 2, 1 }, items);
        }

        /// <summary>
        /// System.Linq provides extension methods for working with IEnumerable
        /// instances. These extension methods are called "standard query operators"
        /// since they provide querying capability over an IEnumerable.
        ///
        /// This example shows a few of the more common
        /// </summary>
        [Fact]
        public void StandardQueryOperators() {
            IList<Employee> employees = new List<Employee> {
                new Employee(1, "Damon", "Allison", null, "DAMON"),
                new Employee(2, "Cole", "Allison", null, "COLE"),
                new Employee(3, "Grace", "Allison", null, "GRACE"),
            };

            // Filter. Note that "Where" lazily evaluates the collection.
            // Only when .Count() is called is the list evaluated.
            Assert.Equal(1, employees.Where(e =>
                e.FirstName.ToLowerInvariant() == "cole").Count());

            // Map. (.ToList() forces evaluation here).
            var expected = new List<string> { "Damon", "Cole", "Grace" };
            var actual = employees.Select(e => e.FirstName).ToList();
            Assert.True(expected.SequenceEqual(actual));

            // We could use anonymous types to project the list into a custom
            // type.
            var names = employees.Select(e => new { e.FirstName, e.LastName });
            Assert.True(expected.SequenceEqual(names.Select(n => n.FirstName)));

            // Any() All()
            Assert.True(employees.Any(e => e.FirstName == "Cole"));
            Assert.True(employees.All(e => !string.IsNullOrWhiteSpace(e.FirstName)));

            // Parallelization
            actual = employees.AsParallel().Select(e => e.FirstName).ToList();
            Assert.True(expected.SequenceEqual(actual));

            // Sorting
            expected = new List<string> { "Cole", "Damon", "Grace" };
            actual = employees.OrderBy(e => e.FirstName).Select(e => e.FirstName).ToList();
            Assert.True(expected.SequenceEqual(actual));


        }
    }
}