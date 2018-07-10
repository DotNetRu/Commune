
namespace DotNetRu.MeetupManagement.Domain.Drafts
{
    public interface IMeetupDraftService
    {
        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        MeetupDraft CreateMeetupDraft(string comminityId, string name);

        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.MeetupNotFoundException" />
        MeetupDraft GetMeetupDraft(MeetupKey key);

        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.MeetupNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.TalkNotFoundException" />
        void AddTalkDraft(MeetupKey meetup, string talkDraftId);

        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.MeetupNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.TalkNotFoundException" />
        void RemoveTalkDraft(MeetupKey meetup, string talkDraftId);

        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.MeetupNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.FriendNotFoundException" />
        void AddFriendReference(MeetupKey meetup, string freindId);

        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.MeetupNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.FriendNotFoundException" />
        void RemoveFriendReference(MeetupKey meetup, string freindId);

        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.MeetupNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.VenueNotFoundException" />
        void SetVenueReference(MeetupKey meetup, string venueId);
    }
}
