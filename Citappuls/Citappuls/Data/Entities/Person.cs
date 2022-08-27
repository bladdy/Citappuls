using Citappuls.Enums;

namespace Citappuls.Data.Entities
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public DateTime DateofBirth { get; set; }
        //public int Age { get; set; }
        public SexType SexType { get; set; }
        public MaritalStatusType MaritalStatus { get; set; }
        public string Nationality { get; set; }
        public City City { get; set; }
        public string Address { get; set; }

    }
}
