using System;

namespace DamonAllison.CSharpTests.Objects
{
    /// <summary>
    /// Provides an abstract base for dealing with identity.
    /// </summary>
    public abstract class IdentityBase : IIdentity, ILogable
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
        static IdentityBase()
        {
            Console.WriteLine("static IdentityBase() has been called.");
        }

        public IdentityBase() 
        {
            NextId++;
        }

        public int Id { get; } = NextId;

        /// <summary>
        /// abstract requires all derived classes to provide an implementation.
        /// 
        /// abstract members are required to be overridden, so they cannot be private.
        /// 
        /// abstract members require the class to be abstract. 
        /// </summary>
        public abstract string Name { get; }

        #region ILoggable

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
        void ILogable.Log() 
        {
            Console.WriteLine($"{DateTime.UtcNow.ToString("s")} : {this}");
        }
        
        #endregion ILoggable
    }
}