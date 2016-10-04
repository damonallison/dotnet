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
        public Employee(string firstName, string lastName, string lanId) 
            : base(firstName, lastName) 
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
    }
}