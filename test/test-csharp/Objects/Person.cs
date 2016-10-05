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
        public int? Age { get; set; } = null;

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
 
        private string _firstName;
        public string FirstName 
        { 
            get 
            {
                return _firstName;
            } 
            set 
            {
                if (value == null) 
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _firstName = value;
            }
        }
        public string LastName { get; set; }

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
            if (ReferenceEquals(Name, null)) 
            {
                return ReferenceEquals(other.Name, null);
            }
            return Name.Equals(other.Name);
        }

        /// <summary>
        /// GetHashCode() should not change over the life of the object. This only works 
        /// when values used within the hashCode are immutable. This object does *not* 
        /// keep a consistent HashCode over the lifetime of the object since the variables 
        /// upon which it's based (Name) is mutable.
        /// </summary>
        public override int GetHashCode()
        {
            int hashCode = base.GetHashCode();
            if (Name != null) {
                hashCode ^= Name.GetHashCode();
            }
            return hashCode;
        }

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