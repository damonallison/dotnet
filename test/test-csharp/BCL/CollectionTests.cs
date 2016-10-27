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
            // Provides the 
        }
    }
}