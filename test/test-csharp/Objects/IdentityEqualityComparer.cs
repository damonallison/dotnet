using System.Collections.Generic;

namespace DamonAllison.CSharpTests.Objects
{
    public class IdentityEqualityComparer : IEqualityComparer<IdentityBase>
    {
        public bool Equals(IdentityBase x, IdentityBase y)
        {
            if ((x == null) != (y == null)) {
                return false;
            }
            return x.Id == y.Id;
        }

        public int GetHashCode(IdentityBase obj)
        {
            return 31 * obj.Id.GetHashCode();
        }
    }
}
