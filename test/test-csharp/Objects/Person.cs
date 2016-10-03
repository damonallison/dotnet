namespace DamonAllison.CSharpTests.Objects
{
    public class Person
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Name 
        { 
            get 
            { 
                return $"{FirstName} {LastName}"; 
            } 
        }
    }
    
}