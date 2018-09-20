using System;

namespace DotNetRu.MeetupManagement.Domain
{
    public class Speaker
    {
        public Speaker(string id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }
        public string Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string BlogsUrl { get; set; }
        public string ContactsUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string GitHubUrl { get; set; }
        public Company Company { get; set; }
        //public string PersonId { get; set; }
    }
}
