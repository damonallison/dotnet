using Xunit;

namespace DamonAllison.CSharpTests
{
    public class StringTests 
    {
        [Fact]
        public void StringImmutability() {
            string s = "damon";
            string s2 = "damon";

            // Both strings point to the same reference. 
            // They are "interned" within the .NET runtime.
            
            Assert.True(string.ReferenceEquals(s, s2));
        }

        [Fact]
        public void StringComparison() 
        {
            char lowerD = 'd';
            char upperD = 'D';

            // upperD comes before lowerD in UTF-16.
            Assert.True(upperD < lowerD);

            // String comparison determines how the 2nd parameter compares against the first.
            // -1 == decreasing. The second parameter less than (sorted before) the first.
            //  0 == identical.
            //  1 == increasing. The second value is more than (sorted after) the first.
            Assert.Equal(1, string.Compare(upperD.ToString(), lowerD.ToString()));
            Assert.True(string.Compare(upperD.ToString(), lowerD.ToString(), true) == 0);
        }
    }
}