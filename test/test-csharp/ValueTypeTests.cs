using System;
using Xunit;

namespace DamonAllison.CSharpTests
{

    /// <summary>
    /// An example of a value type (struct).
    /// 
    /// Value types should be small since they copied around on the stack.
    /// 
    /// Value types should be immutable. The programmer may *think* they are
    /// working with a reference type and expect reference type semantics. By
    /// forcing immutability, a programmer cannot make mistakes when using 
    /// the value type. 
    /// 
    /// </summary>
    internal struct Square
    {
        /// <summary>
        /// Each value type constructor must initialize all fields.
        /// </summary>
        public Square(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; }
        public int Height { get; }

        public int Area 
        {
            get
            {
                return Width * Height;
            }
        }
    }

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
    }
}