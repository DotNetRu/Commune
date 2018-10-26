namespace DotNetRu.MeetupManagement.Infrastructure.EFCore.Models
{
#pragma warning disable CA1716 // Identifiers should not match keywords
    public class Friend
#pragma warning restore CA1716 // Identifiers should not match keywords
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
    }
}
