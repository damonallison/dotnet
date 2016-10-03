using System;

namespace DamonAllison.CSharpTests.Objects
{
    public class Person
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
        int Age { get; set; } = 0;
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
    }
}