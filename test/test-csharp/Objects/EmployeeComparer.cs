using System;
using System.Collections.Generic;

namespace DamonAllison.CSharpTests.Objects {

    public class EmployeeComparer : IComparer<Employee>
    {
        /// <summary>
        /// A custom comparer determines how two values are compared.
        /// Comparison is used to determine sort order.
        ///
        /// Compare() must produce consistent ordering (total order?) for
        /// any possible pair of items. If you don't, you'll end up crashing,
        /// getting into an infinite loop, or other very bad things®.
        /// </summary>
        /// <returns>
        /// * -1 if y < x (think "decreasing")
        /// * 0 if objects are equal.
        /// * 1 if y > x (think "increasing")
        /// </returns>
        public int Compare(Employee x, Employee y)
        {
            // objects are *always* equal to themselves.
            // This also handles both values being null.
            if (Object.ReferenceEquals(x, y))
            {
                return 0;
            }
            if (x == null)
            {
                return 1;
            }
            if (y == null)
            {
                return -1;
            }
            int result = x.LastName.CompareTo(y.LastName);
            if (result != 0)
            {
                return result;
            }
            return 0;
        }
    }
}