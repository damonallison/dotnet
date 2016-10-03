namespace DamonAllison.CSharpTests.Objects
{
    public class Employee : Person
    {
        public string LanId { get; private set; }
        public Employee(string firstName, string lastName, string lanId) 
            : base(firstName, lastName) 
        {
            LanId = lanId;
        }
    }
}