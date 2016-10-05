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
        /// itself, rather than an instance of a class. The runtime will 
        /// invoke the static constructor once prior to any use of the class.
        /// 
        /// Important : Do *not* throw an exception from a static constructor. 
        /// The type will be unusable for the remainder of the application's 
        /// lifetime.
        /// 
        /// The order of operations for class instance initialization:
        /// 
        /// 0. Static properties are set to their default values.
        /// 1. Static constructor is invoked.
        /// 2. Instance properties are set to their default values.
        /// 3. Instance constructure is invoked.
        /// </summary>
        static IdentityBase()
        {
            Console.WriteLine("static IdentityBase() has been called.");
        }

        /// <summary>
        /// Creates a new instance with an ID that is beased off our internal static 
        /// "NextId" value. NextId will be unique within the execution of the program.
        /// 
        /// Using a technique like this - a static variable to store the next identifier 
        /// is not a good solution. Objects created in different instances of the program 
        /// will produce identical Id values. (Each instance will assign 1 to the first created
        /// object). 
        /// 
        /// A better solution for identity would be to use a random GUID value for new instances.
        /// The reason this class uses "NextId" is strictly to show the use of static variables.
        /// </summary>
        public IdentityBase() 
        {
            NextId++;
        }

        public IdentityBase(int id) 
        {
            this.Id = id;
        }

        /// <summary>
        /// By default, each new instance will be assigned the current NextId value. 
        /// </summary>
        public int Id { get; private set; } = NextId;

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

        #region Object

        public override string ToString() 
        {
            return $"[{this.GetType().FullName}] : {Id} : {Name}";
        }

        /// <summary>
        /// Guidelines for overriding Equals:
        /// 
        /// * If the objects are <code>Equals()</code>, they *must* 
        ///   have the same GetHashCode().
        /// 
        /// * GetHashCode()'s return value should be consistent for the life
        ///   of the object, even if it's state changes. You should cache 
        ///   the method return to enforce this constraint.
        /// 
        /// * GetHashCode() should include base.GetHashCode as part 
        ///   of it's calculation.
        /// </summary>
        public override bool Equals (object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return this.Id == ((IdentityBase)obj).Id;
        }
        
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <summary>
        /// Overriding == and != is a convenience for the programmer, since calling
        /// .Equals() and == is guaranteed to return the same value. If the operators
        /// are not overridden, .Equals() will perform our custom value equality, where 
        /// == could return a different value.
        /// 
        /// In general, operator overloading is a bad idea. It's not immediately intuitive that 
        /// comparing two objects via an operator (a > b) does what you expect it to do. Rather,
        /// writing a dedicated function requires (a.IsGreaterThan(b)) the programmer to see
        /// what method is being invoked.
        /// 
        /// Operator overloading can be confusing. Stay away unless you really know what you are
        /// doing.
        /// </summary>
        public static bool operator ==(IdentityBase lhs, IdentityBase rhs) 
        {
            if (ReferenceEquals(lhs, null)) // Don't call == or you'll get into infinite recursion.
            {
                // Return true if both sides are null.
                return ReferenceEquals(rhs, null);
            }
            return lhs.Equals(rhs);
        }

        public static bool operator !=(IdentityBase lhs, IdentityBase rhs)
        {
            return !(lhs == rhs);
        }

        #endregion Object 
    }
}