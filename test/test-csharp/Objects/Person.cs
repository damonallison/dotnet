using System;

namespace DamonAllison.CSharpTests.Objects
{
    public class Person
    {
        
    
        public static int NextId { get; private set; } = 1;
        /// <summary>
        /// Static constructors provide a means to initialize the class
        /// itself, rather than an instance of a class.
        /// 
        /// Important : Do *not* throw an exception from a static initializer. 
        /// The type will be unusable for the remainder of the application's 
        /// lifetime.
        /// 
        /// The order of operations for class instance initialization:
        /// 
        /// 0. Static properties are set to their default values.
        /// 1. Static constructor is invoked.
        /// 2. Instance properties are set to their default values.
        /// 3. Instance constructure is invoked.d
        /// </summary>
        static Person()
        {
            Console.WriteLine("static Person() has been called.");
        }
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
            NextId++;
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
        public int Id { get; } = NextId;

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
        public string Name 
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