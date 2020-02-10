using System;
using DamonAllison.CSharpTests.Objects;

namespace DamonAllison.CSharpTests.ValueTypes
{
    /// <summary>
    /// An example of a value type (struct).
    /// 
    /// Value types should be small since they copied around on the stack.
    /// 
    /// Value types should be immutable. The programmer may *think* they are
    /// working with a reference type (via boxing) and expect reference type semantics. By
    /// forcing immutability, a programmer cannot make mistakes when using 
    /// the value type. 
    /// 
    /// System.ValueType overrides `object`s virtual members, but does not provide
    /// efficient or smart implementations. Always override GetHashCode(), Equals(), 
    /// !=, ==, and ToString() in your value types. Also consider implementing 
    /// IEquatable&lt;T&gt;
    /// 
    /// TODO: ^^
    /// 
    /// </summary>
    internal struct Square : ILogable
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

        #region ILogable

        public void Log()
        {
            Console.WriteLine($"Square ({Width}x{Height})");
        }
        #endregion
    }
}