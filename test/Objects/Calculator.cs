namespace DamonAllisoNamespace.CSharpTests.Objects
{
    /// <summary>
    /// Static classes prevent instances from being created 
    /// and prevents declaration of non-static members. 
    /// </summary>
    public static class Calculator
    {
        /// <summary>
        /// Const fields are static and cannot be changed over time. 
        /// 
        /// Version numbers, identifiers, build numbers, which can be changed
        /// over time are poor candidates for constants.
        /// 
        /// If another assembly references a public const, the value is compiled
        /// directly into their assembly. Therefore, the value of const should 
        /// truly be const and never change.
        /// </summary>
        public const float PI = 3.14F; 

        public static int Add(int x, int y) => x + y;
        public static int Subtract(int x, int y) => x - y;
    }
}