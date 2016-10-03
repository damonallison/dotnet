using Xunit;

namespace DamonAllison.CSharpTests
{
    public class MethodTests 
    {

        [Fact]
        public void RefParamTest() 
        {
            string f = "damon";
            string l = "allison";
            Swap(ref f, ref l);
            Assert.Equal(f, "allison");
            Assert.Equal(l, "damon");
        }
        [Fact]
        public void OutParamTest() 
        {
            string firstName, lastName;
            ReturnMultiple("Damon Allison", out firstName, out lastName);
            Assert.Equal("Damon", firstName);
            Assert.Equal("Allison", lastName);
        }
        [Fact]
        public void ParamsTest() 
        {
            Assert.Equal(0, Params());
            Assert.Equal(1, Params("damon"));
            Assert.Equal(2, Params("Damon", "Allison"));
        }

        [Fact]
        public void OptionalParametersTest() 
        {
            Assert.Equal("FirstName LastName", OptionalName());
            Assert.Equal("Damon LastName", OptionalName(firstName : "Damon"));

            // C# 4.0 introduced "named arguments" - allowing the caller to specify 
            // each parameter by name. It's important to note that if named arguments
            // are used, the argument names become part of the object's contract and 
            // cannot be changed without breaking compatibility.
            Assert.Equal("Damon Allison", OptionalName(lastName: "Allison", firstName: "Damon"));
        }

        private void Swap(ref string firstName, ref string lastName) 
        {
            string temp = firstName;
            firstName = lastName;
            lastName = temp;
        }
        /// <summary>
        /// Out parameters allow you to return multiple values from a function.
        /// 
        /// The compiler will guarantee the out parameters are assigned before 
        /// each return statement. (They do *not* need to be set if you are 
        /// throwing an exception)
        /// 
        /// TryParse() and TryValidate() are good examples of when to use <code>out</code>
        /// parameters.
        /// </summary>
        /// <param name="name">The incoming full name.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        private void ReturnMultiple(string name, out string firstName, out string lastName)
        {
            string[] names = name.Split(' ');
            firstName = names[0];
            lastName = names.Length > 1 ? names[1] : null;           
        }
        
        private int Params(params string[] parameters)
        {
            return parameters.Length;
        }

        /// <summary>
        /// Parameters can be associated with a constant value, which makes them optional.
        ///  
        /// Optional parameters must come after all required parameters.
        /// </summary>
        private string OptionalName(string firstName = "FirstName", string lastName = "LastName") => $"{firstName} {lastName}";
    }
}