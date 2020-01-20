using System;

namespace DamonAllison.CSharpTests.Objects
{
    public class Person : IdentityBase
    {
        /// <summary>
        /// "Auto-implemented" properties implement a private backing
        /// field to store the property value.
        ///
        /// C# 6.0 added the ability to initialize properites to a
        /// default value with initializer syntax.
        ///
        /// Without an access modifier, members are private.
        /// </summary>
        public int? Age { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public Person() : this("First", "Last")
        {
        }

        // Constructor chaining.
        public Person(string firstName, string lastName)
            : this(firstName, lastName, null)
        {
        }

        public Person(string firstName, string lastName, int? age) : base()
        {
            // Id is assigned by base()
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public Person(int id, string firstName, string lastName, int? age)
            : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        #region

        /// <summary>
        /// Implicit and explicit conversion operators allow you to convert
        /// types which are not part of an inheritance relationship.
        ///
        /// This conversion operator allows you to convert a person to a string.
        ///
        /// Use implicit operators if you are certain the cast will succeed. For
        /// example, converting an int to a long will always succeed.
        ///
        /// If there is any chance of conversion failing, use an explicit operator.
        ///
        /// The compiler will not allow an invalid type cast, therefore "implicit" is
        /// strictly a convenience. When in doubt, use explicit and force the
        /// consumer to provide an explicit cast. It's easy to read and understand
        /// that the conversion is taking place. It's tricky to remember that a type
        /// converstion is taking place.
        ///
        /// Note that operators are *static* and thus resolved at compile time. Therefore,
        /// they cannot be overridden.
        /// </summary>
        /// <example>
        /// Person p = new Person("Test", "User");
        /// string s = (string)p;
        /// Assert.Equal("Test User", s);
        /// </example>
        /// <param name="person"></param>
        public static explicit operator string(Person person)
        {
            return $"{person.FirstName} {person.LastName}";
        }

        #endregion

        /// <summary>
        /// Virtual allows derived classes to override the property / method.
        ///
        /// Be careful of the fragile base class problem when exposing virtual
        /// methods. The fragile base class problem is where derived classes
        /// do not act as expected when changes to the base class are made.
        ///
        /// For example, if this property was changed to invoke an overridden
        /// method, infinite recursion could occur if the derived class didn't
        /// expect the overridden method to be invoked.
        /// </summary>
        public override string Name
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        #region Object Overrides

        public override string ToString() {
            return $"{base.ToString()} Age:{Age}";
        }

        public override bool Equals (object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (this.GetHashCode() != obj.GetHashCode())
            {
                return false;
            }

            Person other = (Person)obj;

            if (!base.Equals(other))
            {
                return false;
            }

            if (ReferenceEquals(FirstName, null))
            {
                return ReferenceEquals(other.FirstName, null);
            }
            if (!FirstName.Equals(other.FirstName))
            {
                return false;
            }
            if (ReferenceEquals(LastName, null))
            {
                return ReferenceEquals(other.LastName, null);
            }
            if (!LastName.Equals(other.LastName))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// GetHashCode() should not change over the life of the object. This only works
        /// when values used within the hashCode are immutable. This object does *not*
        /// keep a consistent HashCode over the lifetime of the object since the variables
        /// upon which it's based (Name) are mutable.
        /// </summary>
        public override int GetHashCode()
        {
            int hashCode = base.GetHashCode();
            int prime = 31;

            hashCode = hashCode * prime + (FirstName == null ? 0 : FirstName.GetHashCode());
            hashCode = hashCode * prime + (LastName == null ? 0 : LastName.GetHashCode());
            return hashCode;
        }

        /// <summary>
        /// Operator overloading allows you to treat a reference type as a primitive
        /// type.
        ///
        /// Operator overloading is simply syntactic sugar for calling a method
        /// on a type (like Equals()). It is more explicit to call a method
        /// rather than rely on using operators. Operators are not implemented
        /// consistenly across all types, therefore it is better to be safe
        /// and define concrete methods on types rather than relying on
        /// operators.
        /// </summary>
        public static bool operator ==(Person lhs, Person rhs)
        {
            if (ReferenceEquals(lhs, null)) // Don't call == or you'll get into infinite recursion.
            {
                // Return true if both sides are null.
                return ReferenceEquals(rhs, null);
            }
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Person lhs, Person rhs)
        {
            return !(lhs == rhs);
        }


        #endregion Object Overrides
    }
}