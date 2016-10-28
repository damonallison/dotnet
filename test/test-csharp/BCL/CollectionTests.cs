using DamonAllison.CSharpTests.Objects;
using System.Collections.Generic;
using Xunit;

namespace DamonAllison.CSharpTests.BCL {

    /// <summary>
    /// These tests highlight working with pre-built collections
    /// and interfaces in the BCL.
    ///
    /// An exmaple is shown which defines a custom collection using iterators,
    /// and thus supports IEnumerable<T>.
    ///
    /// Note that there are two forms of most all BCL collection types:
    /// non-generic and generic. The non-generic collections existed prior
    /// to generics and thus are considered legacy. Only use generic collections.
    ///
    /// Empty collections : return null or an empty collection?
    ///
    /// It is preferred to return an empty collection (use Enumerable.Empty<T>)
    /// rather than null to prevent the caller from having to specifically
    /// check for null. If you want to distinguish between null and an empty
    /// collection for some reason (like null == invalid parameter), you can return
    /// null. If you do run into that case, consider throwing an exception.
    /// </summary>
    public class CollectionTests {

        /// <summary>
        /// The collection interface hierarchy looks like:
        ///
        /// IEnumerable<T>
        ///   ICollection<T>
        ///     IList<T>
        ///     IDictionary<T>
        /// </summary>
        [Fact]
        public void CommonInterfaces() {

            // IList - ordinal based collection. Adds ordinal based operations
            // to ICollection (i.e., [], .IndexOf(elt), .Remove(index)
            IList<string> lst = new List<string> { "Damon", "Allison" };

            lst.Insert(1, "Ryan"); // insert by ordinal.
            Assert.Equal("Ryan", lst[1]); // retrieval by ordinal

            // Very primitive operations which all collection types support
            // .Add, .Remove, .Contains.
            //
            // The functions on ICollection do not assume rank, order, or
            // anything about how the collection is internally structured.
            ICollection<string> coll = lst;


            // IDictionary - key based collection. Adds key based operations
            // to ICollection (i.e., .Keys, .ContainsKey(key), .RemoveKey(key)
            IDictionary<string, string> dict = new Dictionary<string, string> {
                ["damon"] = "damon allison",
                ["cole"] = "cole allison"
            };

            dict.Add("lily", "lily allison"); // insert by key
            Assert.Equal(dict["damon"], "damon allison"); // retrieval by key
        }

        [Fact]
        public void CustomComparer() {

            Person p = new Person(1, "Damon", "Allison", 20);
            IDictionary<Person, bool> people = new Dictionary<Person, bool> {
                [p] = true
            };

            Assert.True(people.ContainsKey(p));
            // Dictionary uses the object's "hashCode" to determine if an object
            // is part of a collection.
            Assert.True(people.ContainsKey(new Person(1, "Damon", "Allison", 20)));
            Assert.False(people.ContainsKey(new Person(1, "Cole", "Allison", 20)));

            // You can give dictionary a custom comparer if you want to use a
            // different comparison metric (i.e., you only want to compare based
            // on Id).
            IDictionary<Person, bool> idPeople = new Dictionary<Person, bool>(new IdentityEqualityComparer());
            idPeople.Add(p, true);

            Assert.True(idPeople.ContainsKey(new Person(1, "Cole", "Allison", 20)));
        }

        [Fact]
        public void SortedCollections() {

            // Dictionaries are sorted by key. You can provide a custom IComparer to
            // determine your sort order.
            IDictionary<string, string> sd = new SortedDictionary<string, string>() {
                ["two"] = "2",
                ["one"] = "1"
            };
            IList<string> keys = new List<string>(sd.Keys);
            Assert.Equal(keys[0], "one");

        }


        // A custom iterator allows the caller to user "foreach" and interate a collection
        // without needing to know the internals of how the collection is implemented.

        private class Pair<T> : IEnumerable<T> {
            public T First { get; set; }
            public T Second { get; set; }

            public IEnumerator<T> GetEnumerator() {
                yield return First;
                yield return Second;
            }
            /// <summary>
            /// Required for non-generic collections.
            /// </summary>
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
                return GetEnumerator();
            }
        }

        [Fact]
        public void TestEnumerator() {
            Pair<string> p = new Pair<string>();
            p.First = "damon";
            p.Second = "allison";

            // Manually iterate the enumerator.
            IEnumerator<string> e = p.GetEnumerator();
            Assert.True(e.MoveNext());
            Assert.Equal(e.Current, "damon");
            Assert.True(e.MoveNext());
            Assert.Equal(e.Current, "allison");
            Assert.False(e.MoveNext());

            // using foreach
            int i = 0;
            foreach(string s in p) {
                if (i == 0) {
                    Assert.Equal(s, "damon");
                }
                else if (i == 1) {
                    Assert.Equal(s, "allison");
                }
                else {
                    Assert.True(false, "The iterator should have returned only two objects");
                }
                i++;
            }
        }
    }
}