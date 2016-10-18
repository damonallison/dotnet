using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DamonAllison.CSharpTests
{

    public class ContactMethod {
        public int Id { get; set; }
    }
    public class EmailContact : ContactMethod {
        public string EmailAddress { get; set; }
    }
    public class PhoneContact : ContactMethod {
        public string PhoneNumber { get; set; }
    }

    /// <summary>
    /// "out T" allows the type parameter to be covariant. It allows you to return a base type
    /// (say, List<object> when the type parameter T is derived from object (say, string)).
    /// 
    /// The compiler allows you to return base types for a type param only when "out" is specified. 
    /// Out signals to the compiler that T is only used for return values, never as input. This 
    /// means the type conversion is always safe.
    ///  
    /// Covariance allows you to return the base class for a generic type parameter.
    /// For example, you could return List<Object> from a covariant function where 
    /// the actual value is List<string>.
    /// </summary>
    public interface IReadOnlyPair<out T> {
        T First { get; }
        T Second { get; }
    }
    public class Pair<T> : IReadOnlyPair<T> {
        public T First { get; set; }
        public T Second { get; set; }
    }
    
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
        /// Covariant types are two types where each type can be converted into the other
        /// without the loss of type safety.
        /// 
        /// If types X and Y are covariant, you can convert one into the other without loss
        /// of type safety.
        /// 
        /// Generic collections are not covariant. For example, <c>List<string></c> is not
        /// covariant with <c>String<object></c>, even tho string derives from object. 
        /// 
        /// Why are these two types not covariant? You can't cast List<object> into List<string>
        /// safely. You could insert an int into List<object>, which would cause the cast to fail.
        /// 
        /// You can enable covariance with the `out` type parameter modifier. If an object only
        /// comes "out" (returned) from a type and never going "into" 
        /// </summary>
        [Fact]
        public void CovariantTest() {

            Pair<EmailContact> contacts = new Pair<EmailContact> {
                First = new EmailContact { Id = 1, EmailAddress = "a@a.com" },
                Second = new EmailContact { Id = 2, EmailAddress = "b@b.com" }
            };

            // This assignment would fail if not for the covariant (out T) type parameter.
            // By default, IReadOnlyPair<Email> and Pair<EmailContact> are not convertable.
            IReadOnlyPair<ContactMethod> baseContacts = contacts;

            Assert.Equal(1, baseContacts.First.Id);
            Assert.Equal(2, baseContacts.Second.Id);
        }

        /// <summary>
        /// Tuple is a great example of the use of generics. 
        /// There are 9 different Typle classes defined, 
        /// each with a differing number of type parameters. 
        /// 
        /// One override of tuples allows you to pass in another tuple. 
        /// This allows you to create essentially variable length tuples.
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