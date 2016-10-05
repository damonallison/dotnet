using Xunit;
using DamonAllison.CSharpTests.ValueTypes;
using DamonAllison.CSharpTests.Enums;

using System;
using System.Collections.Generic;

namespace DamonAllison.CSharpTests
{
    /// <summary>
    /// Value types are stack stored data structures. 
    /// 
    /// Value types directly store their values, where reference types 
    /// are stored on the heap and instances are pointers to the heap.
    /// 
    /// When value types are passed to a function, copies of their values
    /// are passed to the function. 
    /// 
    /// Value types should be small (since frequent copies will occur).
    /// The recommended guidance is 16 bytes or less.
    /// There are two categories of value types : enums and structs.
    /// 
    /// Important : Immutability
    /// 
    /// Value types should be immutable since boxing and unboxing will 
    /// occur on value types. It is not immediately obvious in many cases
    /// if you are working with a boxed or unboxed value. To avoid any 
    /// ambiguity (and have a good, functional data structure), make 
    /// all value types immutable.
    /// </summary> 
    public class ValueTypeTests
    {
        [Theory]
        [InlineData(5, 5)]
        [InlineData(10, 10)]
        [InlineData(100, 100)]
        public void TestValueTypes(int width, int height)
        {
           Square s = new Square(width, height);
           Assert.Equal(width * height, s.Area);
        }

        [Fact]
        public void ValueTypeEqualityTest()
        {
            // Value types are copied.
            Square x = new Square(10, 10);
            Square y = x;
            Assert.NotSame(x, y);

            // default(ValueType) will implicitly initialize an instance of the value type.
            // Implicit initialization of value types results in a zeroed-out memory block.
            // Therefore, it is not practical to use the default() operator on immutable 
            // value types such as Square. (And all your value types are immutable, right?)
            Square z = default(Square);
            Assert.Equal(0, z.Height);
        }

        [Fact]
        public void BoxingTest() 
        {
            Square x = new Square(10, 10);
            List<Square> lst = new List<Square>();
            lst.Add(x);
        }

        [Fact]
        public void EnumTest()
        {
            ConnectionState state = default(ConnectionState);
            Assert.Equal(ConnectionState.Disconnected, state);

            // The default behavior of System.Enum.ToString() is to use the 
            // enum's identifier.
            Assert.Equal("Disconnected", ConnectionState.Disconnected.ToString());

            // Converting from a string -> enum
            Assert.True(Enum.TryParse("Connected", out state));
            Assert.Equal(ConnectionState.Connected, state);

            // Obtaining the underlying enum value requires a type cast.
            Assert.Equal(2, (int)state);
       }
    }
}