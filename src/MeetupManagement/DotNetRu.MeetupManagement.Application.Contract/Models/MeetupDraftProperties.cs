using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetRu.MeetupManagement.Application.Contract.Models
{
    public class MeetupDraftProperties
    {
        public MeetupDraftProperties(ICollection<string> talkDraftIds, ICollection<string> friends, string venue)
        {
            TalkDraftIds = talkDraftIds ?? throw new ArgumentNullException(nameof(talkDraftIds));
            FriendIds = friends ?? throw new ArgumentNullException(nameof(friends));
            VenueId = venue;
        }

        public ICollection<string> TalkDraftIds { get; }
        public ICollection<string> FriendIds { get; }
        public string VenueId { get; }
    }
}
