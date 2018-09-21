namespace DotNetRu.MeetupManagement.Domain.Common
{
    public class Person
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Summary { get; set; }
        public string Phone { get; set; }

        // public string OtherContacts { get; set; }
        // public byte[] Photo { get; set; } // lazy load
    }
}
