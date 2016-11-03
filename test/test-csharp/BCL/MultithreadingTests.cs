using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace DamonAllison.CSharpTests.BCL.Multithreading
{
    /// <summary>
    /// .NET 4.0 introduced the "Parallel Extensions" library in .NET 4.0.
    /// Parallel Extensions includes:
    ///
    /// * TPL : Task Parallel Library
    /// * PLINQ : Parallel LINQ
    /// * TAP : Task based asynchronous programming (TAP) (async/await)
    ///
    /// TPL
    /// ---
    /// Tasks are run via a TaskScheduler. The scheduler abstracts the underlying
    /// thread pool and task execution, allowing it to optimize thread use.
    ///
    /// TAP : Task-based Asynchronous Programming
    /// -----------------------------------------
    /// TAP provides library infrastructure and language support for asynchronous
    /// programming.
    ///
    /// TAP includes:
    /// * Task : an object representing an asynchronous operation.
    /// * async / await : C# language extensions which streamlines the use
    ///   of asynchronous functions. async / await allows you to program
    ///   using asynchronous methods as if they were synchronous. Handling
    ///   results, thread context switching, and exception handling are all
    ///   handled by the compiler and TAP framekwork.
    ///
    /// TAP was created to address key problems with asynchronous programming:
    /// * Allow long running activities to happen without blocking the main thread.
    /// * Optimize the use of threads (TaskScheduler).
    /// * Handle thread synchronization seamlessly.
    /// * Provide language support to make asynchronous programming feel synchronous
    ///   and easy to use.
    ///
    /// Synchronization Context
    /// -----------------------
    /// Tasks are ran on a TaskScheduler. The TaskScheduler is created with a SynchronizationContext,
    /// which guides how work is synchronized with the caller. The SynchronizationContext is
    /// associated to a thread. For example, if a task is executed from a Windows UI thread,
    /// the task's continuation work - the work which happens after 'await' - will
    /// execute on the main thread.
    ///
    /// You generally do not need to create custom TaskSchedulers or set custom
    /// SynchronizationContext(s) on threads, simply understand the TaskScheduler
    /// and SynchronizationContext you are using.
    /// </summary>
    public class MultithreadingTests
    {
        /// <summary>
        /// Shows how to run tasks with the default scheduler.
        /// </summary>
        [Fact]
        public void TaskBasics() {

            // Note that in general, tasks should *not* mutate
            // collection state. Rather, mutate the collection
            // on the main thread with results from tasks.
            // Here, we are adding to the ConcurrentBag<int>
            // for illustration purposes only as some tasks
            // are thunks and do not return a value.
            ConcurrentBag<int> bag = new ConcurrentBag<int>();

            DateTime start = DateTime.Now;
            // Run a thunk.
            Task mainTask = Task.Run(async () => {
                await Task.Delay(100); // TPL version of Thread.Sleep()
                bag.Add(1);
            });
            mainTask.Wait();
            Assert.True(mainTask.IsCompleted && !mainTask.IsFaulted);
            Assert.True(bag.ToList().Contains(1));
            Assert.Equal(1, bag.Count);

            // Proves the task really took 100 ms.
            Assert.True(DateTime.Now.AddMilliseconds(-100).Ticks > start.Ticks);

            // Run a func.
            Task<int> result = Task.Run<int>(async () => {
                await Task.Delay(100);
                return 2;
            });

            bag.Add(result.Result); // .Result blocks and waits for completion.
            Assert.True(bag.ToList().Contains(2));
            Assert.Equal(2, bag.Count);

            // Task Continuation
            Task continuation = mainTask.ContinueWith((previous) => {
                bag.Add(bag.Count);
            });
            continuation.Wait();
            Assert.Equal(3, bag.Count);
        }

        /// <summary>
        /// Exceptions thrown in tasks will be caught and associated with the task
        /// by the task scheduler.
        ///
        /// When attempting to "complete" the task - by calling <c>.Result</c>
        /// or <c>.Wait</c> any exception stored in the task will be wrapped within
        /// an <c>AggregateException</c> and thrown.
        /// </summary>
        [Fact]
        public void TaskExceptions() {

            // Exceptions thrown from tasks which are not handled by
            // observing the completion of the task (.Wait or .Result)
            // or by accessing the .Exception property are considered
            // unobserved. When the task is .Dispose()d, an an unobserved
            // exception remains, the UnobservedTaskException event is
            // called.
            TaskScheduler.UnobservedTaskException += (s, e) => {
                Console.WriteLine($"Caught unobserved exception {e.Exception}");
            };
            Func<int> f = () => {
                throw new InvalidOperationException();
            };
            Task<int> t = Task.Run<int>(f);
            try {
                Assert.Equal(1, t.Result);
            }
            catch (AggregateException aggEx) {
                // Tasks will *always* throw an Aggregate exception.
                // To determine what exception(s) were thrown in the
                // task, you must examine the .InnerExceptions property.
                Assert.Equal(1, aggEx.InnerExceptions.Count);
                Assert.Equal(aggEx.InnerExceptions.ToList()[0].GetType(), typeof(InvalidOperationException));
            }

            // Adding an "OnFaulted" continuation handler will "handle"
            // the exception.
            bool faulted = false;
            Task tt = Task.Run<int>(f);
            Task tt2 = tt.ContinueWith(task => {
                faulted = task.IsFaulted;
            }, TaskContinuationOptions.OnlyOnFaulted);

            tt2.Wait();

            Assert.True(tt.IsFaulted);
            Assert.True(faulted); // Verifies the Continuation task executed.

            // You can also determine if the task threw an exception
            // without accessing .Wait or .Result and causing the
            // exception to be rethrown.
            Assert.NotNull(tt.Exception);
            Assert.Null(tt2.Exception);   // tt2 did *not* throw an exception
        }

        /// <summary>
        /// Tasks use "cooperative cancellation" by monitoring the state of
        /// a cancellation token. Tasks which respect cancellation tokens
        /// will abort themselves when cancellation is requested on the token.
        /// </summary>
        [Fact]
        public void TaskCancellationVoid() {

            int iterations = 0;
            // Create a cancellation token.
            CancellationTokenSource ts = new CancellationTokenSource();
            Task t = Task.Run(() => {
                // infinite loop, only cancelled via token cancellation
                while (!ts.IsCancellationRequested) {
                    iterations++;
                    Task.Delay(10);
                }
            },
            ts.Token);

            bool tokenCancelled = false;


            // Tokens can be monitored for cancellation. This would be nice
            ts.Token.Register(() => {
                tokenCancelled = true;
            });

            Thread.Sleep(200);

            ts.Cancel();
            t.Wait();

            Assert.True(iterations > 0);
            Assert.True(tokenCancelled);
        }

        /// <summary>
        /// Dealing with tasks for asynchronous operations ends up in
        /// "callback hell" - where tasks are chained together. (think javascript).
        ///
        /// C# adds "async / await" to the language to simplify the async pattern.
        /// </summary>
        [Fact]
        public void AsyncAwait() {
            Task<int> t = DoLongRunningThingAsync(10);
            Assert.Equal(10, t.Result);
        }

        /// <summary>
        /// Lambdas can be made async as well.
        ///
        /// async lambdas allow your lambdas to participate in TAP as
        /// concrete methods would. Code can await on their result
        /// rather than having to call .Wait() or .Result().
        /// </summary>
        [Fact]
        public void AsychronousLambdas() {

            Func<int, Task<int>> f = async (int x) => {
                await Task.Delay(x);
                return x;
            };
            Parallel.For(0, 100, (x) => {
                Assert.Equal(x, f(x).Result);
            });
        }

        /// <summary>
        /// The result of an "await" call can be used as a normal return value.
        ///
        /// There are restrictions on when you can use an await's return value.
        /// The type being returned must support a "GetAwaiter". Task<int>,
        /// used below, supports .GetAwaiter(), allowing you to use the return
        /// value from 'async'.
        ///
        /// Note that there is
        /// </summary>
        [Fact]
        public async void AwatingForAReturnValue() {
            Func<int, Task<int>> f = async (int x) => {
                await Task.Delay(x);
                return x;
            };

            Assert.Equal(100, await f(100));

            // An example of setting an 'await result' to a variable.
            int y = await f(200);
            Assert.Equal(200, y);
        }

        /// <summary>
        /// For fun, write an async version of Fibonacci.
        /// </summary>
        [Fact]
        public void AsynchronousFibonacci() {

            // Asynchronous fibonacci
            Func<int, Task<int>> fib = null;
            fib = new Func<int, Task<int>>(async x => {
                if (x <= 2) {
                    return await Task.Run(() => 1);
                }
                return await fib(x - 2) + await fib(x - 1);
            });

            // Prove the logic.
            Assert.Equal(1, fib(1).Result);
            Assert.Equal(1, fib(2).Result);
            Assert.Equal(2, fib(3).Result);
            Assert.Equal(3, fib(4).Result);
            Assert.Equal(5, fib(5).Result);
            Assert.Equal(8, fib(6).Result);

            // Prove asynchronicity
            Task<int> fib10 = fib(10);
            Assert.False(fib10.IsCompleted); // it's running async!
            fib10.Wait();
            Assert.True(fib10.Result > 5);
        }

        [Fact]
        public void ParallelLoops() {
            int result = 0;
            Parallel.For(0, 5, x => {
                // Generally, do *not* mutate data structures from
                // concurrent code!
                Interlocked.Add(ref result, x);
            });
            Assert.Equal(10, result);
        }
        /// <summary>
        /// The async / await pattern allows you to avoid "callback hell" by
        /// providing language level support for asynchronous operations.
        ///
        /// await transforms the rest of your method into a continuation
        /// which executes after the awaited task completes.
        ///
        /// await allows you to handle exceptions without having to deal
        /// with AggregateException. Remember Tasks always throw "AggregateExceptions",
        /// wrapping all "actual" exceptions into it's "InnerExceptions" property.
        ///
        /// await will "unpack" and throw the first InnerException contained
        /// within the Tasks's AggregateException, allowing you to handle
        /// the exception synchronously as you would any other exception.
        ///
        /// Note that if you need to handle multiple exceptions from a task,
        /// you cannot use 'await'. Instead, call the task as you wouuld without
        /// await, .Wait() on the result, and catch the AggregateException.
        ///
        /// A method decorated with "async" must return void, Task, or Task<T>.
        /// It is idomatic to add the "Async" suffix to the function name,
        /// indicating asychronicity to the caller.
        ///
        /// Threading
        /// When an await completes, the remainder of the method (the await's continuation)
        /// may execute on a different thread
        /// </summary>
        private async Task<int> DoLongRunningThingAsync(int x) {
            try {
                await Task.Delay(x);
                // Illustrates exception handling with await.
                // The "AggregateException" is handled internally by await,
                // throwing the first InnerException to the caller.
                await Task.Run(() => {
                    throw new InvalidOperationException();
                });
            }
            catch(InvalidOperationException) {
                // Notice the compiler unwraps the first InnerException from the
                // AggregateException, allowing you to handle the exception
                // directly, without the AggregateException wrapper.
            }
            catch(Exception) {
                Assert.True(false, "We should have caught an InvalidOperationException");
            }
            return x;
        }
    }
}