using System;

namespace DotNetRu.MeetupManagement.Application.Contract.Models
{
    public class CreateFriendDraftParameters
    {
        public CreateFriendDraftParameters(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
    }
}
