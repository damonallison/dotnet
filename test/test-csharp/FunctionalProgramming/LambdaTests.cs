
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Xunit;

namespace DamonAllison.CSharpTests.FunctionalProgramming
{
    /// <summary>
    /// Lambdas are functions. They are advanced versions of delegates.
    /// </summary>
    public class LambdaTests
    {
        /// <summary>
        /// Declaring a delegate type. To declare a delegate type, simply follow
        /// the <c>delegate</c> keyword with the method signature.
        /// </summary>
        private delegate bool CompareFunction(int x, int y);

        /// <summary>
        /// Before lambdas, functions were passed around as delegates.
        /// </summary>
        [Fact]
        public void DelegateTest() {

            // You can create a delegate variable (instance) which points to
            // a function. This function pointer can be used wherever you need
            // a delegate instance. This is a rather obsolete syntax, as lambdas
            // are generally preferred, however if the function is rather long
            // and does not lend itself well to a lambda, it can be used.
            CompareFunction func = CompareFunc;

            Assert.True(CompareDelegate(1, 2, func));

            // Rather than specifying a dedicated method as a delegate parameter into
            // CompareDelegate, "anonymous methods" were also used. Here is an example of
            // an anonymous method. This syntax is obsolete, replaced with lambda
            // syntax.
            Assert.True(CompareDelegate(1, 2, delegate(int x, int y) {
                return x < y;
            }));
        }

        /// <summary>
        /// Lambdas are syntactic sugar around delegates. When C# compiles a lambda,
        /// the compiler emits a method that contains the body of the lambda. It then
        /// creates a delegate to the emitted method and passes the delegate to the
        /// function.
        /// </summary>
        [Fact]
        public void LambdaTest() {
            // Passes a lambda when a Func<> is required.
            Assert.True(CompareLambda(1, 2, (int x, int y) => x < y));

            // Example of using single parameter lambda (no parens needed for single argument
            // lambda.
            List<int> ints = new List<int> {1, 2, 3};
            IEnumerable<int> filtered = ints.Where(x => x > 1);
            Assert.Equal(2, filtered.Count());
        }

        /// <summary>
        /// Expression trees are data structures which describe a lambda expression.
        ///
        /// Expression trees allow for code to transform the lambda into another
        /// representation, say a SQL query. (IQueryable<>).
        ///
        /// You could build software which takes an expression tree and convert
        /// it to any query language.
        ///
        /// When lambdas are compiled, they are turned into a <c>LambdaExpression</c>.
        /// </summary>
        [Fact]
        public void ExpressionTreeTest() {
            IEnumerable<string> e = new List<string> {"damon", "allison"};

            // In this example, the lambda is compiled into an expression tree.
            // This powers LINQ -> SQL, where at execution time the lambda could
            // be converted into SQL and sent to a DB to return results.
            IQueryable<string> queryable = Queryable.Where<string>(e.AsQueryable(),
                x => x.Length <= 5);
            Assert.Equal(1, queryable.ToList().Count);

            // Here, we create an expression tree and show how we have
            // programmatic access to the expression.
            Expression<Func<int, int, bool>> exp;
            exp = (x, y) => x * y > x + y;

            // You now have access to the entire expression.
            Assert.Equal(2, exp.Parameters.Count);
            Assert.Equal(exp.ReturnType, typeof(bool));

            // Deconstruct the expression. Having access to the entire expression tree
            // allows you to manipulate the expression (optimize), turn the expression
            // into a search query (SQL), or examine the expression for another purpose.
            Assert.True(exp.Body is BinaryExpression);
            BinaryExpression b = exp.Body as BinaryExpression;

            // b.Left is the LHS == "(x * y)"
            Assert.True(b.Left.NodeType == ExpressionType.Multiply);
        }

        /// <summary>
        /// A function which matches the `CompareDelegate` delegate signature.
        /// </summary>
        private bool CompareFunc(int x, int y) {
            return x < y;
        }

        /// <summary>
        /// Example of a function accepting a delegate (function pointer) as an argument.
        /// </summary>
        private bool CompareDelegate(int x, int y, CompareFunction pred) {
            return pred.Invoke(x, y);
        }
        /// <summary>
        /// Example of a function accepting a lambda (function pointer) as an argument.
        ///
        /// Func<> and Action<> are generic delegates which generally avoid the need to create
        /// custom delegate types. In general, there is no reason to create a custom delegate
        /// type for most delegates.
        /// </summary>
        private bool CompareLambda(int x, int y, Func<int, int, bool> pred) {
            return pred.Invoke(x, y);
        }
    }
}