using System;

namespace DamonAllison.CSharpTests.Objects
{
    public class Employee : Person
    {
        public string LanId { get; private set; }

        /// <summary>
        /// Constructors are not overridable. If there is not a default
        /// constructor on the base class, the derived class must specify
        /// which base constructor to call.
        /// </summary>
        public Employee(int id, string firstName, string lastName, int? age, string lanId)
            : base(id, firstName, lastName, age)
        {
            LanId = lanId;
        }

        /// <summary>
        /// The `sealed` modifier prevents further overriding of a virtual member.
        ///
        /// Note that any member decorated `override` is inherently `virtual`
        /// </summary>
        public override sealed string Name
        {
            get
            {
                return $"{LastName}, {FirstName}";
            }
        }


        #region Object Overrides

        public override string ToString() {
            return $"{base.ToString()} LanId : {LanId}";
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

            Employee other = (Employee)obj;

            if (!base.Equals(other))
            {
                return false;
            }
            if (ReferenceEquals(LanId, null))
            {
                return ReferenceEquals(other.LanId, null);
            }
            return LanId.Equals(other.LanId);
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
            int prime = 31;

            hashCode = hashCode * prime + LanId == null ? 0 : LanId.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Employee lhs, Employee rhs)
        {
            if (ReferenceEquals(lhs, null)) // Don't call == or you'll get into infinite recursion.
            {
                // Return true if both sides are null.
                return ReferenceEquals(rhs, null);
            }
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Employee lhs, Employee rhs)
        {
            return !(lhs == rhs);
        }

        #endregion Object Overrides
    }
}