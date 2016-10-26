using System;
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
    public class LinqTests {

        private class Engineer {
            public string FirstName { get; set; }
            public int DepartmentId { get; set; }
        }
        private class Department {
            public int Id { get; set; }
            public string Name { get; set; }
        }

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
        /// This example shows a few of the more common extension methods.
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

            // Sorting (.OrderBy() and .ThenBy())
            //
            // .OrderBy() only allows you to sort by a single value.
            // .ThenB() allows you to sort by additional values.
            //
            // Calling .OrderBy() multiple times will not sort multiple columns.
            // Each additional .OrderBy() will undo the work of the previous OrderBy()
            // since it re-orders the values.
            expected = new List<string> { "Cole", "Damon", "Grace" };
            actual = employees.OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .Select(e => e.FirstName)
                .ToList();
            Assert.True(expected.SequenceEqual(actual));
        }

        [Fact]
        public void TestJoins() {

            var engineers = new List<Engineer> {
                new Engineer { FirstName = "Damon", DepartmentId = 1 },
                new Engineer { FirstName = "Cole", DepartmentId = 2 },
                new Engineer { FirstName = "Lily", DepartmentId = 1 },
                new Engineer { FirstName = "Grace", DepartmentId = 2 }
            };

            var departments = new List<Department> {
                new Department { Id = 1, Name = "Marketplace" },
                new Department { Id = 2, Name = "Research and Development" },
                new Department { Id = 3, Name = "Sanitation" }
            };

            var engDepts = engineers.Join(departments,
                engineer => engineer.DepartmentId,
                department => department.Id,
                (employee, department) => new {
                    FirstName = employee.FirstName,
                    DepartmentId = department.Id,
                    DepartmentName = department.Name
                });
            Assert.True(engDepts.Any(ed => ed.FirstName == "Damon" && ed.DepartmentName == "Marketplace"));

            // IGrouping<key, val> itself is an IEnumerable which contains all <Engineer> elements in the group.
            IEnumerable<IGrouping<int, Engineer>> deptEngineers = engineers.GroupBy(e => e.DepartmentId);
            IGrouping<int, Engineer> marketplaceEngineers = deptEngineers.First(de => de.Key == 1);
            Assert.True(marketplaceEngineers.Any(me => me.FirstName == "Damon"));
            Assert.True(marketplaceEngineers.Any(me => me.FirstName == "Lily"));

            // Group Join performs the join and provides the group to the resulting lambda.
            var deptGroups = departments.GroupJoin(
                engineers,
                department => department.Id,
                engineer => engineer.DepartmentId,
                (department, deptEngs) => new {
                    DepartmentId = department.Id,
                    DepartmentEngineers = deptEngs
                }
            );

            Assert.True(deptGroups.First(dg => dg.DepartmentId == 1).DepartmentEngineers.Any(e => e.FirstName == "Damon"));


            // An outer join is accomplished using .SelectMany(), which is like .FlatMap() in Rx terms.
            var engsByDept = departments.GroupJoin(
                engineers,
                department => department.Id,
                engineer => engineer.DepartmentId,
                (department, departmentEngineers) => new {
                    DepartmentId = department.Id,
                    Engineers = departmentEngineers
                }).SelectMany(
                    departmentRecord => departmentRecord.Engineers.DefaultIfEmpty(),
                    (departmentRecord, departmentRecordEngineers) => departmentRecord);

            Assert.True(engsByDept.Any(ed => ed.DepartmentId == 1 && ed.Engineers.Any(e => e.FirstName == "Damon")));

            // The .DefaultIfEmpty() on the SelectMany() provides the outer join semantics.
            // Here, we have no engineers in the "Sanitation" department (Id == 3).
            Assert.True(engsByDept.Any(ed => ed.DepartmentId == 3 && ed.Engineers.Count() == 0));
        }

        [Fact]
        public void SetLinqOperators() {

            IEnumerable<object> objects = new object[] { "damon", 5, 4, 3, 2, 1 };
            IEnumerable<int> evens = new int[] { 0, 2, 4, 6, 8 };

            // Filter based on type.
            IEnumerable<int> numbers = objects.OfType<int>();

            // Sets
            IEnumerable<int> allNumbers = evens.Union(numbers); // set-based union, no duplicates
            Assert.True(allNumbers.Count() == 8);

            var odds = allNumbers.Except(evens);
            Assert.True(odds.Contains(1));
            Assert.True(odds.Count() == 3);

            var both = numbers.Intersect(evens);
            Assert.True(both.Contains(2) && both.Contains(4) && both.Count() == 2);

            // Sorting
            Assert.True(numbers.SequenceEqual(new int[] { 5, 4, 3, 2, 1 }));
            Assert.True(numbers.OrderBy(e => e).SequenceEqual(new int[] { 1, 2, 3, 4, 5 }));
        }

        /// <summary>
        /// Using lambda based query expressions can get rather complicated. The C# team
        /// decided to bake query expressions directly into the language to "simplify"
        /// complex queries. Rather than dealing with "complex" nested lambdas, you
        /// can write a "simple" SQL-like query expression and all complexity is handled
        /// for you.
        ///
        /// Query expressions are all syntactic sugar around IEnumerable extension methods.
        /// The C# compiler translates the query expressions into corresponding IEnumerable
        /// method calls.
        ///
        /// Query expressions are a subset of IEnumerable extensions. For example, there is
        /// no query expression equivalent for TakeWhile().
        ///
        /// While the SQL-like syntax can be convenient, I think it makes more sense to avoid
        /// the layer of abstraction introduced by the new syntax.
        /// </summary>
        [Fact]
        public void LinqWithQueryExpressions() {

            var engineers = new List<Engineer> {
                new Engineer { FirstName = "Damon", DepartmentId = 1 },
                new Engineer { FirstName = "Cole", DepartmentId = 2 },
                new Engineer { FirstName = "Lily", DepartmentId = 1 },
                new Engineer { FirstName = "Grace", DepartmentId = 2 }
            };

            var departments = new List<Department> {
                new Department { Id = 1, Name = "Marketplace" },
                new Department { Id = 2, Name = "Research and Development" },
                new Department { Id = 3, Name = "Sanitation" }
            };

            // C# Query Expressions.

            // * "engineer" is a "range variable", it represents each item in the collection,
            //   much like foreach loop variable.
            IEnumerable<Engineer> marketplaceEngineers =
                from engineer in engineers where engineer.DepartmentId == 1 select engineer;

            Assert.True(marketplaceEngineers.Count() == 2);
            Assert.True(marketplaceEngineers.Select(e => e.FirstName).OrderBy(n => n).SequenceEqual(new string[] { "Damon", "Lily"}));

            // Selecting into an anonymous type.
            var marketplaceEngineerNames =
                from engineer in engineers
                let name = engineer.FirstName // introduce a new range variable which can be used further down in the statement
                where engineer.DepartmentId == 1
                orderby name descending
                select name;

            Assert.True(marketplaceEngineerNames
                .SequenceEqual(new string[] { "Lily", "Damon" }));
        }

        /// <summary>
        /// One of the examples shown in Essential C# was how to do a cartesian product using
        /// query expressions.
        /// <code>
        ///  Using Query Expressions
        /// IEnumerable<KeyValuePair<int, int>> actualViaExpression =
        ///      from one in p1
        ///      from two in p2
        ///      select new KeyValuePair<int, int>(one, two);
        /// </code>
        ///
        /// This example shows how to perform a cartesian product using standard Linq IEnumerable
        /// extensions. The trick is to use "FlatMap" (a.k.a., .SelectMany() to flatten multiple lists.
        /// </summary>
        [Fact]
        public void CartesianProduct() {
            IEnumerable<int> p1 = new int[] {0, 1};
            IEnumerable<int> p2 = new int[] {1, 2};

            // The call to .SelectMany() flattens out the results from each returned IEnumerable.
            // Here, the returned IEnumerable is a list of KeyValuePairs with the key being the current
            // p1 variable and the value being the current p2 value.
            IEnumerable<KeyValuePair<int, int>> actual = p1.SelectMany(one => p2.Select(two => new KeyValuePair<int, int>(one, two)));

            IList<KeyValuePair<int, int>> expected = new List<KeyValuePair<int, int>> {
                new KeyValuePair<int, int>(0, 1),
                new KeyValuePair<int, int>(0, 2),
                new KeyValuePair<int, int>(1, 1),
                new KeyValuePair<int, int>(1, 2),
            };

            Assert.True(expected.SequenceEqual(actual));


            // Using Query Expressions
            IEnumerable<KeyValuePair<int, int>> actualViaExpression =
                from one in p1
                from two in p2
                select new KeyValuePair<int, int>(one, two);

            Assert.True(expected.SequenceEqual(actualViaExpression));
        }


    }
}