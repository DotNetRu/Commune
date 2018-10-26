namespace DotNetRu.MeetupManagement.Infrastructure.EFCore.Models.Common
{
    public class Person
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Summary { get; set; }
        public string Phone { get; set; }
    }
}
