using DamonAllison.CSharpTests.Objects;
using Newtonsoft.Json;
using System;
using System.Reflection;
using Xunit;

/// <summary>
/// Reflection allows you to:
///
/// 1. Introspect the type system.
/// 2. Add additional metadata to the typesystem via reflection.
/// 3. Dynamic member invocation. Reflection even allows you to circumvent
///    accessibility rules (private | protected) as long as the "Code Access Security (CAS)"
///    rules allow it.
///
/// "Code Access Security (CAS)" are rules which apply to assemblies loaded locally or
/// remotely. CAS is not a factor if all your assemblies are local. Microsoft should drop CAS
/// from .NET entirely since loading remote assemblies is just a bad idea.
///
/// <c>System.Type</c> provides information about a type.
///
/// Questions for you to think about:
/// 1. Types are data structures. How are types similar or different than other data structures?
/// 2. How do weakly typed languages (javascript) handle reflection? Do they need reflection?
/// 3. If your code uses a lot of reflection, are you using the right programming language?
///    Reflection is essentially working around the type system by invoking things
///    dynamically.
///
/// Serialization in early versions of .NET (binary and soap serialization) used attributes
/// <c>[Serializable]</c> to mark a type as serializable. .NET serialization is deprecated
/// and should be avoided. Use Newtonsoft.Json to serialize / deserialize objects.
/// </summary>
namespace DamonAllison.CSharpTests.BCL.Reflection
{

    /// <summary>
    /// An attribute which allows you to provide "human readable names" to each
    /// property. Such an attribute could be used for translating typed arguments
    /// into a property name or field name. For example if the user types "-t" we
    /// could set the "Test" property to <c>true</c>.
    ///
    /// <c>AttributeUsage</c> constricts which type members an attribute applies to.
    /// This prevents a user from applying an attribute to an incorrect type member.
    /// You should always constrain the targets to which a custom attribute applies.
    ///
    /// Attributes allow for named parameters. Named parameters allow you to set
    /// public properties as part of the attribute definition. Assigning named properties
    /// must occur as the last portion of the constructor call, following explicitly
    /// declared constructor arguments.
    ///
    /// Named properties provide you flexability to set individual properties without
    /// having to create a dedicated constructor for each parameter combination.
    ///
    /// <c>AttributeUsage</c> will cause the compiler to validate usages of this
    /// attribute and fail the build if used incorrectly. <c>AttributeUsage</c> is special
    /// in this regard as your attributes cannot influence the compiler.
    ///
    /// <c>ObsoleteAttribute</c> will also alter the compiler's behavior by injecting
    /// a compile time warning (optionally an error) when the member is invoked.
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Property | AttributeTargets.Field,
        AllowMultiple = true)]
    internal class ReadableNameAttribute : Attribute
    {
        internal string ReadableName { get; private set; }
        internal ReadableNameAttribute(string name) {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            ReadableName = name;
        }

    }

    public class ReflectionTests
    {
        [ReadableNameAttribute("t")]
        [ReadableNameAttribute("test")]
        public string TestProperty { get; private set; }

        /// <summary>
        /// There are two ways to get <c>System.Type</c> information.
        /// 1. Calling <c>.GetType()</c> on an object instance.
        /// 2. Using the <c>typeof()</c> operator (useful for static classes).
        /// </summary>
        [Fact]
        public void GetTypeInformation() {
            Type t = typeof(Person);
            Person p = new Person();
            Assert.True(t.Equals(p.GetType()));
            Assert.False(t.Equals(typeof(IdentityBase)));

            // nameof allows you to retrieve a string version of an identifier.
            // * nameof() allows you to refactor identifiers without needing to manually
            //   update string values to match.
            // * nameof() provides compiler checking to it's argument, ensuring the
            //   parameter is valid.
            Assert.Equal("p", nameof(p));
        }

        /// <summary>
        /// Retrieves custom attributes on <c>TestProperty</c>.
        /// </summary>
        [Fact]
        public void CustomAttributeTest() {
            PropertyInfo property = GetType().GetProperty(nameof(TestProperty));
            Attribute[] attributes = (Attribute[])property
                .GetCustomAttributes(typeof(ReadableNameAttribute), false);

            foreach(Attribute attribute in attributes) {
                Assert.True(attribute is ReadableNameAttribute);
                ReadableNameAttribute rna = (ReadableNameAttribute)attribute;
                Assert.True(rna.ReadableName == "test" || rna.ReadableName == "t");
            }
        }

        /// <summary>
        /// Dynamic objects (C# 4.0). With dynamic objects, the runtime resolves and
        /// dispatches calls at runtime. Calls to dynamic objects are *not* verified
        /// by the compiler.
        ///
        /// Dynamic objects were introduced to work with runtime environments which
        /// do not have a compile time defined structure.
        ///
        /// Dynamic is completely different than <c>var</c>.
        /// * <c>var</c> variables are strongly typed. var is simply a convenience
        ///   to prevent having to explicitly define a type. The variable is strongly
        ///   typed and will be type checked by the compiler.
        /// * <c>dynamic</c> variables are weakly typed.
        ///   No type checking is done at compile time.
        ///
        /// Why use dynamic typying?
        ///
        /// * Dealing with incoming Json (or other) data structures where you don't
        ///   want to parse the entire incoming structure into a strongly typed object.
        ///   i.e., The incoming data is "dynamic".
        ///
        /// * Whenever you find yourself using weakly typed data, like using a hardcoded
        ///   string to index a data structure, consider dynamic typing.
        ///
        /// </summary>
        [Fact]
        public void DynamicObjectsTest() {

            dynamic data = "Damon Allison";

            // Tools will *not* provide intellisense with dynamic objects.
            Assert.Equal(13, data.Length);
            if (false) {
                // Because dynamic objects are *not* compiler checked, this will compile.
                Console.WriteLine(data.WhateverPropertyIWantThisStillCompiles());
            }
        }
    }
}

