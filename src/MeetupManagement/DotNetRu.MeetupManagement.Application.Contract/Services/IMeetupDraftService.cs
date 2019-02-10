using System.Collections.Generic;
using DotNetRu.MeetupManagement.Application.Contract.Models;

namespace DotNetRu.MeetupManagement.Application.Contract.Services
{
    public interface IMeetupDraftService
    {
        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.MeetupNotFoundException" />
        MeetupDraft GetMeetupDraft(string communityId, string meetupId);

        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        MeetupDraft CreateMeetupDraft(string communityId, string name);

        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.MeetupNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.TalkNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.FriendNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.VenueNotFoundException" />
        void UpdateMeetupDraft(
            string communityId,
            string meetupId,
            ICollection<string> talksIds,
            ICollection<string> friendIds,
            string venueId);

        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.MeetupNotFoundException" />
        void DeleteMeetupDraft(string communityId, string meetupId);

        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.MeetupNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.VenueNotFoundException" />
        void SetVenue(string communityId, string meetupId, string venueId);

        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.MeetupNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.FriendNotFoundException" />
        void AddFriend(string communityId, string meetupId, string friendId);

        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.MeetupNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.FriendNotFoundException" />
        void DeleteFriend(string communityId, string meetupId, string friendId);

        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.MeetupNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.TalkNotFoundException" />
        void AddTalkDraft(string communityId, string meetupId, string talkDraftId);

        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.MeetupNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.TalkNotFoundException" />
        void DeleteTalkDraft(string communityId, string meetupId, string talkDraftId);
    }
}