namespace DamonAllison.CSharpTests.Objects
{
    public static class StringExtensions
    {
        /// <summary>
        /// Extension methods allow you to add methods (extend) another type.
        /// 
        /// * The first parameter of an extension method corresponds to the type
        ///   which is being extended. 
        /// * Extension methods are prefixed with `this`.
        /// 
        /// Extension methods are not a good substitute for inheritance. If you
        /// need to add functionality to a type, extend it. 
        /// </summary>
        public static string Capitalize(this string value) 
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            return $"{value.Substring(0, 1).ToUpper()}{value.Substring(1)}";
        }
    }
}