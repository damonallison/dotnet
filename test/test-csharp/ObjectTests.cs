using Xunit;
using DamonAllison.CSharpTests.Objects;
using System.Collections.Generic;

namespace DamonAllison.CSharpTests
{
    public class ObjectTests 
    {

        [Fact]
        public void ObjectInitializerTest()  
        {
            // C# 3.0 added object initializers. Object initializers
            // allow you to set field / property values on the object.
            // 
            // This is strictly syntactic sugar. The IL generated by 
            // object initializers is exactly the same as if you would manually
            // set properties following the creation of the object.
            Person p = new Person() 
            {
                FirstName = "Damon", 
                LastName = "Allison"
            };
            Assert.True(p.Id > 0);
            Assert.Equal(Person.NextId, p.Id + 1);
        }

        [Fact]
        public void CollectionInitializerTest() 
        {
            // C# 3.0 added collection initializers. 
            // Collection initializers allow you to add elements to the collection 
            // at instiantiation.
            IList<Person> family = new List<Person>() 
            {
                new Person() 
                {
                    FirstName = "Damon", 
                    LastName = "Allison"
                },
                new Person() 
                {
                    FirstName = "Kari", 
                    LastName = "Allison"
                } 
            };
            Assert.Equal(2, family.Count);
        }

        [Fact]
        public void ExtensionMethodTest()
        {
            string damon = "damon";
            Assert.Equal("Damon", damon.Capitalize());
        }

        /// <summary>
        /// Conversion operators (implicit or explicit) allow you to convert
        /// a type (A) to another type (B) where the two types are not part 
        /// of an inheritance hierarchy.
        /// </summary>
        [Fact]
        public void ConversionOperatorTest() 
        {
            Person p = new Person("Test", "User");
            string s = (string)p;
            Assert.Equal("Test User", s);
        }

        [Fact]
        public void InheritanceTest()
        {
            Employee e = new Employee("Damon", "Allison", "allidam");
            Assert.IsAssignableFrom<Employee>(e);
            Assert.IsAssignableFrom<Person>(e);
        }



    }
}