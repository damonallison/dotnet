using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DamonAllison.CSharpTests
{

    public class ValueChangedEventArgs<T> : EventArgs {

        public T OldValue { get; private set; }
        public T NewValue { get; private set; }

        public ValueChangedEventArgs(T oldValue, T newValue) {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }

    /// <summary>
    /// <c>System.Threading</c> has been made mostly obsolete by the TPL (Task Parallel Library)
    /// and Task Async Programming (TAP) programming model.
    ///
    /// <see cref="DamonAllison.CSharpTests.BCL.Multithreading.TaskParallelLibraryTests" />
    ///
    /// Obviously, threading concepts are fundamental and need to be mastered
    /// to truly understand how the C# / .NET environment has implemented threading.
    ///
    /// This class contains threading examples in .NET.
    ///
    /// General guidelines:
    /// * USE IMMUTABLE TYPES! If you don't mutate state, you don't need locking.
    /// * Lock on an internal <c>private readonly static object</c> instance. Do not lock on
    ///   <c>this</c>, a type object like <c>this.GetType() || typeof(ClassName)</c>
    ///   or a <c>String</c> object. All of these objects are able to be used as lock
    ///  objects from other object instances. <c>String</c> objects are intened, so
    ///  other classes could lock on the same string from within another class.
    /// * The synchronization must *not* be a value type. When boxing occurs on
    ///   the value type, a different instance is given to Monitor.Enter and Monitor.Exit.
    /// * The <c>volatile</c> keyword can be used to tell the compiler *not* to optimize
    ///   reads/writes to the variable. In general, avoid volatile and wrap reads/writes
    ///   to variables using <c>lock</c>.
    /// * All <c>static</c> data must be thread safe.
    /// * Instance state is *not* expected to be thread safe, unless specifically
    ///   designed to be so.
    ///
    /// <see cref="System.Threading.Mutex" /> is similar to monitor in that it locks
    /// a region of code. Mutex can synchronize system wide, preventing multiple processes
    /// from accessing a piece of code.
    ///
    /// ManualResetEvent allows you manually signal another thread from the current thread.
    /// Calling <c>.Wait()</c on a ManualResetEvent will block the thread from running until
    /// </c>.Set()</c> is called from another thread or the wait period times out. You
    /// must call <c>.Reset()</c> on the ManualResetEvent to set the event back to an
    /// unsignaled state.
    ///
    /// AutoResetEvent unblocks only one thread's <c>.Wait()</c> call. Other threads are
    /// *not* unblocked.
    ///
    /// Semaphores are similar to <c>ManualResetEvent</c> in that they restrict resources.
    /// Semaphores keep a count, restricting access when that count is 0.
    ///
    /// CountdownEvent is similar to a semaphore, but is the opposite. Where Semaphores
    /// allow access until the count reaches zero, CountdownEvent will prevent access until
    /// the count reaches zero.
    ///
    /// When dealing with COM (lucky you), decorate <c>main</c> with <c>[System.STAThreadAttribute]</c>.
    /// Invoke all COM components from the main thread. Research COM threading models if
    /// you are in the poor state of having to invoke COM components.
    /// </summary>
    public sealed class ThreadingTests
    {
        /// <summary>
        /// It is important *not* to lock on <c>this</c>, a type object (<c>this.GetType()</c>),
        /// or a <see cref="System.String" />. All of these objects can also be used as locks
        /// by other objects, which can lead to incorrect locking behavior.
        /// </summary>
        private readonly static object _sync = new object();

        event EventHandler<ValueChangedEventArgs<int>> OnValueChanged;

        /// <summary>
        /// Limiting a region of code to a single execution thread at a time is
        /// handled by a <see cref="System.Threading.Monitor" /> object.
        ///
        /// Working with <see cref="System.Threading.Monitor" /> directly is error prone. You
        /// must make sure you place the proper lock code in a try / finally block, C# introduced
        /// the <c>lock</c> keyword to encapsulate monitor logic.
        ///
        /// The <c>lock</c> statement provides a convenient, less error prone way
        /// to lock a particular region.
        /// </summary>
        [Fact]
        public void LockExample()
        {
            int count = 0;

            // Locking using Monitor.
            Parallel.For(0, 100, (x) => {
                bool lockTaken = false;
                try {
                    Monitor.Enter(_sync, ref lockTaken);
                    count++;
                }
                finally {
                    if (lockTaken) {
                        Monitor.Exit(_sync);
                    }
                }

            });
            Assert.Equal(100, count);

            // Locking using lock. Equivalent to the Monitor code above.
            Parallel.For(0, 100, (x) => {
                lock(_sync) {
                    count++;
                }
            });
            Assert.Equal(200, count);
        }

        /// <summary>
        /// Always check for null using the null-conditional operator. The
        /// operator is atomic, therefore you will not get into potential race
        /// conditions by manually checking for null, then firing the event.
        /// </summary>
        [Fact]
        public void FiringAnEvent() {

            // This contains a race condition. Value changed could be
            // turned to null after the if block, causing a NPE when
            // firing the event.
            if (this.OnValueChanged != null) {
                this.OnValueChanged(this, new ValueChangedEventArgs<int>(1, 2));
            }

            // The correct way to fire an event.
            OnValueChanged?.Invoke(this, new ValueChangedEventArgs<int>(1, 2));
        }
    }
}