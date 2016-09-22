using System.Globalization;
using Xunit;

namespace DamonAllison.CSharpTests
{
    public class DataTypeTests 
    {
        /// <summary>
        /// Integer types in 
        /// </summary>
        [Fact]
        public void IntegerTypes() 
        {
            // Determining the max / min value of a primitive type.
            Assert.Equal(0, byte.MinValue);
            Assert.Equal(255, byte.MaxValue);
            
            // Parsing an integer type from a string representation.
            Assert.Equal(123, int.Parse("123"));

            // implicit casting is allowed when expanding the value.
            int i = 123;
            long l = i; // implicit cast
            Assert.Equal(i, l);

            // explicit cast is required when collapsing the value.
            long l2 = 123L;
            Assert.IsType(typeof(int), (int)l2);
        }
        /// <summary>
        /// The following literal suffixes are used to specify what type
        /// a literal value should assume. 
        /// 
        /// Literal suffixes can be specified in both upper or lower case, 
        /// however uppercase to avoid mistaking a literal ("l") with a 
        /// number ("1").
        /// 
        /// U == unsigned
        /// L == long (64 bits)
        /// </summary>
        [Fact]
        public void LiteralSuffix() {

            ulong ul = 123UL;
            
            Assert.Equals(ul)


        }

    }
}