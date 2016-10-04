using System;

namespace DamonAllison.CSharpTests.Objects
{
    public class Person : IdentityBase
    {
        public Person() : this("First", "Last")
        {
        }

        // Constructor chaining.
        public Person(string firstName, string lastName)
            : this(firstName, lastName, 0) 
        {
        }
        public Person(string firstName, string lastName, int age)  
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
 
        /// <summary>
        /// "Auto-implemented" properties implement a private backing 
        /// field to store the property value. 
        ///    
        /// C# 6.0 added the ability to initialize properites to a 
        /// default value with initializer syntax.
        /// 
        /// Without an access modifier, members are private.
        /// </summary>
        int? Age { get; set; } = null;

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
            return $"Person[{Id}]: \"{FirstName} {LastName}\"";
        }

        #endregion Object Overrides
    }
}