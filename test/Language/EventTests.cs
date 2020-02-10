using System;
using Xunit;

namespace DamonAllison.CSharpTests.Language
{
    /// <summary>
    /// Events are based on MulticastDelegate.
    ///
    /// * When adding a new subscriber, the MulticastDelegate is assigned
    ///   to a new instance of the delegate type. This allows you to add and
    ///   remove listeners to a MulticastDelegate in a multi-threaded scenario.
    ///   Any existing references to the MulticastDelegate will continue to
    ///   reference the same delegate list it had previously.
    ///
    /// * Do *not* throw an exception from a delegate listener. Any remaining
    ///   non-invoked listeners will not execute.
    ///
    /// What are events? Events are syntactic wrappers around MulticastDelegate which
    /// prevent common sources of bugs that occur.
    ///
    /// 1. Inadvertant assignment. With MulticastDelegate, it is possible to do
    ///    straight assignment to the delegate, which would remove existing
    ///    subscribers. Events prevent inadvertant assignment by forcing you to
    ///    use `+=` and `-=`.
    ///
    /// <code>
    /// Action<int> OnTemperatureChanged;
    ///
    /// // Potential bug here! Any other subscribers to OnTemperatureChanged
    /// // will be removed!
    /// OnTemperatureChanged = x => Console.WriteLine($"Temp changed to {x}");
    /// </code>
    ///
    /// 2. Encapsulating the publication. Events ensure that only the
    ///    containing class can invoke the delegate. This prevents any
    ///    outside class from inadvertantly (or purposely) invoking the delegate.
    ///
    /// </summary>
    public class EventTests
    {
        /// <summary>
        /// Whenever an event has a payload, define an (preferably immutable)         /// EventArgs class with the necessary event data.
        ///
        /// When an event is raised, the sender argument is passed along. While
        /// the receiver *could* retrieve state from the sender object, there is
        /// no guarantee the state on `sender` will be identical to the state of
        /// `sender` when the event is raised. Another thread could have mutated
        /// `sender` during event delivery. Therefore, it is always best to send
        /// all relevant state in an <c>EventArgs</c> instance.
        /// </summary>
        public class TemperatureChangedEventArgs : EventArgs {
            public float OldTemperature { get; private set;}
            public float NewTemperature { get; private set; }

            public TemperatureChangedEventArgs(float oldTemperature, float newTemperature) {
                OldTemperature = oldTemperature;
                NewTemperature = newTemperature;
            }
        }
        public class Thermostat
        {
            private float _currentTemp;
            public float CurrentTemp {
                get {
                    return _currentTemp;
                }
                private set {
                    if (value == _currentTemp) {
                        return; // no change.
                    }
                    float previousTemp = _currentTemp;
                    _currentTemp = value;

                    // Even tho the event can never be null, it is still good form
                    // to check for null prior to invocation.
                    OnTemperatureChanged?.Invoke(this, new TemperatureChangedEventArgs(previousTemp, _currentTemp));
                }
            }

            public Thermostat(float currentTemp) {
                CurrentTemp = currentTemp;
            }
            public void AdjustTemp(float degrees) {
                CurrentTemp += degrees;
            }

            /// <summary>
            /// Declaring the event. Assigning the event to an empty delegate
            /// which represents 0 listeners allows us to fire the event
            /// without having to first check if the event is null.
            ///
            /// It is still smart to do a null check prior to invoking the event.
            ///
            /// The generic <c>EventHandler</c> delegate is a convention used in .NET
            /// to prevent the need to create custom delegate types for each event.
            /// While you *could* create a custom delegate type for each event, doing
            /// so would be poor form. Stick to using <c>EventHandler</c> as the
            /// delegate type.
            /// </summary>
            public event EventHandler<TemperatureChangedEventArgs> OnTemperatureChanged = delegate {};
        }


        [Fact]
        public void TestEvents() {
            float? changedTo = null;
            Thermostat t = new Thermostat(32.0F);
            t.OnTemperatureChanged += (s, e) => {
                changedTo = e.NewTemperature;
            };
            t.AdjustTemp(10.0F);
            Assert.Equal(42.0F, t.CurrentTemp, 1);
        }
    }
}