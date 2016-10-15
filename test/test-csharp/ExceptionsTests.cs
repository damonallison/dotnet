using System;
using System.Collections.Generic;
using Xunit;

namespace DamonAllison.CSharpTests 
{
    /// <summary>
    /// An example of a custom exception. In general, a custom exception allows you to:
    /// 
    /// 1. Associate state with an exception.
    ///  
    /// TODO: 
    /// * Exception conditionals.
    /// * 
    /// 
    /// Exception guidelines:
    /// * Only catch (swallow) exceptions you can handle.
    /// * Allow unexpected exceptions to the top of the stack. Don't handle them (or log them) too low.
    /// * Prefer `throw` to `throw ex` since `throw` will keep the initial call stack in tact.
    ///  
    /// ////// </summary>
    internal class ArgumentTooShortException : System.ArgumentException
    {
        public int Length { get; private set; }

        /// <summary>
        /// Since constructors are not automatically inherited when subclassing, you must 
        /// define your own constructors. When dealing with exceptions, you typically want 
        /// to override all base exception constructors. Obviously you could only 
        /// override a particular subset of constructors if you want. 
        /// 
        /// This exception does *not* override all constructors since we have 
        /// required state to set on the exception (Length).
        /// 
        /// Just like regular objects, custom exceptions should be immutable.
        /// 
        /// Note on <c>InnerException</c>. <c>InnerException</c> is used to store the original 
        /// exception that was raised. This is valuable information to have when raising 
        /// a different exception than what you catch. It allows the user to have the original 
        /// exception which caused the final exception.
        /// 
        /// A function should only throw exceptions that make sense to the caller. For example, if you 
        /// define a function such as:
        /// 
        /// <code>
        /// public int TotalValue(int accountNumber)
        /// {
        ///     CheckingAccount acct = new CheckingAccount(System.IO.File($"{accountNumber}.txt");
        ///     return acct.Value;
        /// }    
        /// </code>
        /// 
        /// The above function may throw an <see cref="System.IO.IOException" />. The caller has 
        /// no idea that disk activity was taking place, therefore this function should not 
        /// throw an <see cref="System.IO.IOException" />, rather it should throw an exception
        /// that would make sense to the caller, like <see cref="AccountDoesNotExistException" />.
        /// 
        /// Exceptions are a critical part of your interface. Therefore, pick type names
        /// which are appropriate to the exception.
        /// 
        /// Exceptions should reflect exceptional conditions only! An API should allow the caller
        /// to check if an exception will occur rather than making the caller call the function 
        /// and handling an exception to see if a condition is valid. For example, Enum.TryParse() 
        /// is more useful than Enum.Parse() since you don't have to handle an exception to determine
        /// if a value is valid.
        /// </summary>
        public ArgumentTooShortException(string paramName, int length) 
            : this(null, paramName, length) 
        {
        } 

        public ArgumentTooShortException(string message, string paramName, int length) 
            : this(message, paramName, length, null) 
        {
        }

        public ArgumentTooShortException(string message, string paramName, int length, Exception innerException) 
            : base(message, paramName, innerException)
        {
            this.Length = length;
        }
    }

    public class ExceptionsTests
    {

        /// <summary>
        ///  When catching exceptions, the most specific catch block should proceed
        /// more general catch blocks. The first matching block will be invoked.
        /// </summary>
        [Fact]
        public void TestExceptionHandling() 
        {
            try
            {
                throw new System.ArgumentNullException("test");
            }
            // An exception conditional block allows you to specify a condition that must be 
            // met for the catch block to be invoked. This block should *not* throw an exception.
            // Likewise, what it returns should be consistent (otherwise it may be difficult to 
            // reason about). 
            catch (System.ArgumentNullException nullEx) when (nullEx.ParamName == "test")
            {
                // Say this exception is recoverable or expected. 
                // In this case, we would catch it and carry on. 
            }
            catch (System.ArgumentNullException nullEx)
            {
                Assert.True(false, "The exception should have been caught by the first catch block");
                // re-throwing an exception instance will replace the exceptions stack trace 
                // with this function as the root. Typically, you should just `throw`, which leaves
                // the original call stack unmodified.
                throw nullEx; 
            }
            catch
            {
                Assert.True(false, "The exception should have been caught");
                throw;  // rethrows the caught exception with the original stack trace.  
            }            
        }

        [Fact]
        public void TestCatchMostSpecific()
        {
            try 
            {
                throw new ArgumentTooShortException("test", 10);
            }
            catch (ArgumentTooShortException) 
            {
                // The C# compiler will enforce you catch the most derived exception
                // prior to a more general exception. If you attempted to catch a more 
                // generic exception prior to a more specific exception, you will receive
                // a compiler error.
            }
            catch (ArgumentException) 
            {
                Assert.True(false, "This will never occur. ArgumentTooShortException is more derived and will always catch the exception");
            }
        }

        /// <summary>
        /// C# 5.0 added a class that allows you to re-throw an exception without losing the 
        /// original stack trace. This is particularly helpful in the case of `AggregateException`,
        /// where you want to throw the `InnerException` and not lose the original stack trace.
        /// </summary>
        [Fact]
        public void TestRethrowingExceptionWithoutLosingCallstack() 
        {
            try 
            {
                throw new AggregateException(new List<Exception> {
                    new NullReferenceException("test")
                });
            }
            catch (AggregateException aggEx)
            {
                Exception ex = Assert.Throws<NullReferenceException>(() => {
                    // This call will throw the given exception, but keep the original stack 
                    // trace in tact.
                    System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(aggEx.InnerException).Throw();
                });
                Assert.IsType<NullReferenceException>(ex);
            }
        }
    }

}