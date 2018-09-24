using System;

namespace DotNetRu.MeetupManagement.Application.Contract.Models
{
    public class TalkRehearsal
    {
        public TalkRehearsal(string id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }

        public string Id { get; }
        public string Comment { get; set; }
    }
}
