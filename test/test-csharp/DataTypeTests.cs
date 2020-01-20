using Xunit;

namespace DamonAllison.CSharpTests
{
    public class DataTypeTests 
    {
        /// <summary>
        /// Integer types in .NET:
        /// 
        /// byte / sbyte : 8 bit
        /// short / ushort : 16 bit
        /// int / uint : 32 bit
        /// long / ulong: 64 bit
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
        /// F == float
        /// </summary>
        [Fact]
        public void LiteralSuffix() {

            ulong ul = 123UL;
            long l = 123L;
            Assert.Equal((long)ul, l);

            float f = 123.123F;
            double d = 123.123D;

            Assert.Equal(123.123F, f);
            Assert.Equal(123.123D, d);

            decimal dec = 123.123M;
            Assert.Equal(123.123M, dec);
        }

        /// <summary>
        /// Floating point types in .NET:
        /// 
        /// float : 32 bit (binary floating point)
        /// double : 64 bit (binary floating point)
        /// decimal : 128 bit (not binary, maintains exact accuracy. useful for financial calculations)
        /// 
        /// Floating point numbers can only represent exactly if they are a fraction with a power of 2 
        /// as the denominator. (1/4, 1/8). If not, they are not precise.
        /// 
        /// Do *not* compare binary numbers for equality.
        /// 
        /// TODO: Understand in what cases floating point numbers are equal / inequal.
        /// </summary>
        public void FloatingPointTypes() {

            double d = 23.13D;
            float f = 23.13F;

            Assert.NotEqual(f, (float)d);
        }

        /// <summary>
        /// char is a 16 bit type whose values are drawn from the Unicode UTF-16 encoding.
        /// 
        /// Not all unicode chars can fit into a single char. Surrogate pairs are required for some 
        /// characters. 
        /// </summary>
        [Fact]
        public void CharType() {
            char a = 'a';
            char c = 'c';

            // internally, chars are `ushort`s, so they can be used in arithmetic calculations.
            Assert.Equal(a + 2, c);
        }

    }
}