using DamonAllison.CSharpTests.Objects;
using System;
using System.Collections.Generic;
using Xunit;

namespace DamonAllison.CSharpTests.Language
{
    public class ObjectTests
    {
        private class TestObject {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        [Fact]
        public void ObjectInitializerTest()
        {
            // C# 3.0 added object initializers. Object initializers
            // allow you to set field / property values on the object.
            //
            // This is strictly syntactic sugar. The IL generated by
            // object initializers is exactly the same as if you would manually
            // set properties following the creation of the object.
            TestObject t = new TestObject()
            {
                FirstName = "Damon",
                LastName = "Allison"
            };
        }

        [Fact]
        public void CollectionInitializerTest()
        {
            // C# 3.0 added collection initializers.
            // Collection initializers allow you to add elements to the collection
            // at instiantiation.
            IList<TestObject> testObjects = new List<TestObject>()
            {
                new TestObject()
                {
                    FirstName = "Damon",
                    LastName = "Allison"
                },
                new TestObject()
                {
                    FirstName = "Kari",
                    LastName = "Allison"
                }
            };
            Assert.Equal(2, testObjects.Count);
        }

        /// <summary>
        /// <code>Capitalize</code> is defined in StringExtensions.cs
        ///
        /// Extensions can be added to *all* types, even BCL types.
        /// </summary>
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
        ///
        /// Here, an explicit conversion operator (string) is defined for
        /// person.
        /// </summary>
        [Fact]
        public void ConversionOperatorTest()
        {
            Person p = new Person("Test", "User");
            string s = (string)p;
            Assert.Equal("Test User", s);
        }

        /// <summary>
        /// The class hierarchy in this example is:
        ///
        /// IIdentity
        ///   IdentityBase
        ///     Person
        ///       Employee
        /// </summary>
        [Fact]
        public void InheritanceTest()
        {
            Employee e = new Employee(1, "Damon", "Allison", 20, "allidam");
            Assert.IsAssignableFrom<Employee>(e);
            Assert.IsAssignableFrom<Person>(e);
            Assert.IsAssignableFrom<IdentityBase>(e);
            Assert.IsAssignableFrom<IIdentity>(e);

            Person p = e;
            IdentityBase ib = p;
            IIdentity i = ib;
            Assert.Equal("Allison, Damon", e.Name);
            Assert.Equal("Allison, Damon", p.Name);
            Assert.Equal("Damon Allison", (string)p);

            Assert.Equal(e.Id, p.Id);
            Assert.Equal(p.Id, ib.Id);
            Assert.Equal(ib.Id, i.Id);

            // Note that we have three references to the same object.
            Assert.True(Object.ReferenceEquals(e, p));
            Assert.True(Object.ReferenceEquals(p, i));
            Assert.True(Object.ReferenceEquals(i, ib));

            // `is` determines if an object is of a given type. Generally, you want
            // to avoid having to downcast an object. You should be able to work with
            // an object as it's declared in the current scope. If you need to downcast,
            // consider changing the declaration of the function to require the specific
            // type you are looking for.
            Assert.True(p is IIdentity);
            Assert.True(p is IdentityBase);
            Assert.NotNull(p as Employee); // Downcast. This should be avoided.

            // `as` goes a step beyond `is`. If the object is of a given type, it will
            // convert the type and return a reference to the object as the target type
            // or `null` if the cast is not valid.
            //
            // C#'s' type checker is smart enough to know that p (Person) cannot be converted
            // to a string. Therefore, we cast to `object` to fool the type checker.
            string s = (string)p;
            Assert.Equal("Damon Allison", (string)p);
            Assert.Equal("Damon Allison", (string)e);

            object obj = (object)p;
            Assert.IsAssignableFrom<Employee>(obj);
            Assert.NotNull(obj as Employee);
            Assert.NotNull(obj as Person);
            Assert.NotNull(obj as IdentityBase);
            Assert.NotNull(obj as IIdentity);
        }

        /// <summary>
        /// Person's abstract base class, <see cref="IdentityBase"/> defines
        /// a static member "NextId". When any new IdentityBase object is created,
        /// it must have a new, unique Id. This test verifies that identities are
        /// handled correctly by the base.
        /// </summary>
        [Fact]
        public void AbstractBaseTest()
        {
            // Employee and person both derive from AbstractBase, therefore
            // they both will contain unique identifiers

            // Id is not specified, therefore IdentityBase's NextId will
            // be assigned.
            Person one = new Person("Damon", "Allison");
            Assert.True(one.Id > 0);

            // Specifies an Id.
            Employee two = new Employee(1, "Damon", "Allison", 20, "allidam");
            Assert.Equal(1, two.Id);
        }

        /// <summary>
        /// Explicit interface implementation requires the caller cast an
        /// object to the interface reference before calling a member.
        ///
        /// In our object hierarchy, ILoggable is explicitly implemented
        /// by IdentityBase. Therefore, you must cast all objects to ILoggable
        /// to log them. Any member explicitly implemented is not added to the
        /// type's declaration space.
        ///
        /// Explicit interface implementation is preferred when the interface's
        /// members are not essential to the object.
        ///
        /// Favor explicit implementations when possible to reduce the type's
        /// footprint.
        /// </summary>
        [Fact]
        public void ExplicitInterfaceImplementation()
        {
            Person p = new Person();


            p.LogToDB();

            // Note that LogToDB is an extension method in ILogableExtensions.
            // Extension methods work with interfaces in the same way they work
            // with concrete types.
            ((ILogable)p).Log();
        }

        /// <summary>
        /// Well formed types provide overrides for `System.Object` virtual members
        /// .ToString()
        /// .Equals()
        /// .GetHashCode()
        /// </summary>
        [Fact]
        public void WellFormedTypes()
        {
            Person p = new Person(1, "Damon", "Allison", 20);

            // Equality.
            Person c = new Person(1, "Damon", "Allison", 20);
            Assert.True(c.GetHashCode() == p.GetHashCode());
            Assert.True(c.Equals(p));
            Assert.True(c == p);
            Assert.False(c != p);

            c = new Person(1, "Cole", "Allison", 20);
            Assert.False(c.GetHashCode() == p.GetHashCode());
            Assert.False(c.Equals(p));
            Assert.False(c == p);
            Assert.True(c != p);
        }
        [Fact]
        public void LazyInitialization() {
            Lazy<Person> lazy = new Lazy<Person>(() => {
                return new Person("Damon", "Allison");
            });
            Assert.False(lazy.IsValueCreated);
            Assert.Equal("Damon", lazy.Value.FirstName);
            Assert.True(lazy.IsValueCreated);
        }
    }
}