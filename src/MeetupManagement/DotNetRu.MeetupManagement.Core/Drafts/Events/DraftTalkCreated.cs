using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetRu.MeetupManagement.Core.Drafts.Events
{
    public class DraftTalkCreated
    {
        public long DraftTalkId { get; set; }
        public string Description { get; set; }
    }
}
