using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetRu.MeetupManagement.Domain.Drafts
{
    public class CreateFriendDraftParameters
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
    }
}
