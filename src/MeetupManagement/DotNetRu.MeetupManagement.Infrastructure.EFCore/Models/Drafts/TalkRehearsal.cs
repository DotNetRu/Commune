namespace DotNetRu.MeetupManagement.Infrastructure.EFCore.Models.Drafts
{
    using System;

    public class TalkRehearsal
    {
        public string Id { get; set; }
        public string Comment { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
