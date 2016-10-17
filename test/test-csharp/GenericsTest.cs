using System;
using System.Collections.Generic;
using Xunit;

namespace DamonAllison.CSharpTests
{
    /// <summary>
    /// Interfaces can be declared with generic type parameters.
    /// 
    /// This example shows the use of type constraints. 
    /// 
    /// Covariant: 
    /// Types X and Y are considered "covariant" if X can be converted into Y.
    /// In C#, List<string> and List<object> are not covariant. Why? 
    /// If you downcasted List<string> into List<object>, you could legally
    /// add any type to the list (since T is now more generic), violating
    /// type safety.
    /// 
    /// Therefore, you cannot pass a List<object> instance into a function
    /// which accepts List<string>.  Likewise, you cannot cast a List<string>
    /// into List<object>.
    /// 
    /// Read the Func<>, Action<>, and Tuple<> classes for good exampes of generics,
    /// covariance, contravariance.
    /// </summary>
    internal interface IStack<T> 
        where T: System.IComparable<T>, System.IEquatable<T> {
        void Push(T obj);
        T Pop();
        int Length { get; }
    }

    /// <summary>
    /// Generic classes are especially useful for container classes (i.e. collections).
    /// 
    /// Notice in this example, the generic type parameter `T` must be constrained 
    /// at a minimum to the types that are defined on IStack&lt;T&gt;. Type parameters
    /// are not inherited and must be re-declared to meet the constraints imposed by 
    /// the base class. The compiler will enforce this.
    /// </summary>
    internal class Stack<T> : IStack<T> 
        where T: System.IComparable<T>, System.IEquatable<T> {
        private List<T> internalStack = new List<T>();
        public Stack() {}

        public void Push(T obj)
        {
            internalStack.Add(obj);
        }
        public T Pop() 
        {
            if (internalStack.Count == 0) 
            {
                throw new InvalidOperationException();
            }
            T temp = internalStack[internalStack.Count - 1];
            internalStack.RemoveAt(internalStack.Count - 1);
            return temp;
        }
        public int Length => internalStack.Count;
    }

    internal class StackPrinter
    {
        /// <summary>
        /// Methods can also declare type parameters.
        ///  
        /// Like types, it is legal to have two methods which differ
        /// only in the number of generic parameters they take. For example, we could 
        /// also declare PrintStack&lt;T, U&gt;
        ///   
        /// Specifying type parameters when calling generic methods is not required. Notice 
        /// in the example below, we do not need to specify the type parameter when calling 
        /// PrintStack. Type inference will infer the type parameter based on the argument.
        ///  
        /// <code>
        /// IStack<int> st = new Stack<int>();
        /// st.Push(1);
        /// st.Push(2);
        /// 
        /// StackPrinter.PrintStack(st); 
        /// </code>
        /// </summary>
        internal static void PrintStack<T>(IStack<T> stack) 
            where T: System.IComparable<T>, System.IEquatable<T> 
        {
            while(stack.Length > 0) {
                System.Console.WriteLine($"Stack item : {stack.Pop()}");
            }
        }
    }

    public class GenericsTests
    {
        [Fact]
        public void TestGenerics() {
            IStack<int> s = new Stack<int>();

            Assert.IsAssignableFrom<Stack<int>>(s);
            Assert.IsAssignableFrom<IStack<int>>(s);
            Assert.IsAssignableFrom<object>(s);

            s.Push(1);
            s.Push(2);
            Assert.Equal(s.Pop(), 2);
            Assert.Equal(s.Pop(), 1);
            try {
                s.Pop();
            }
            catch(InvalidOperationException) {
                return;
            }
            Assert.True(false, "Stack<T> should should have thrown an InvalidOperationException when trying to pop from an empty stack");
        }

        /// <summary>
        /// Tuple is a great example of the use of generics. There are 9 different Typle classes defined, 
        /// each with a differing number of type parameters. This allows you to create essentially variable
        /// length tuples.
        /// </summary>
        [Fact]
        public void TupleTest() {
            // Tuple types are created via type inference. The factory method `Tuple.Create` 
            // infers it's type parameters based on it's arguments.
            Assert.IsAssignableFrom<Tuple<string, int>>(Tuple.Create("Damon", 40));
            
            // Create an instance without using type inference.
            Tuple<string, int> t = new Tuple<String, int>("Damon", 40);

            // Compare. 
            Assert.Equal(t, Tuple.Create("Damon", 40));
        }   
    }
}