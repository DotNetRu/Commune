namespace DotNetRu.MeetupManagement.Infrastructure.EFCore.Models.Drafts
{
    public class SpeakerDraft
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string BlogsUrl { get; set; }
        public string ContactsUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string GitHubUrl { get; set; }
        public virtual Company Company { get; set; }

        public string CompanyId { get; set; }
    }
}
