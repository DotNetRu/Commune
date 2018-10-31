using System;
using System.Collections.Generic;
using System.Linq;
using DotNetRu.Common.Collections;
using DotNetRu.MeetupManagement.Application.Contract.Exceptions;
using DotNetRu.MeetupManagement.Application.Contract.Models;
using DotNetRu.MeetupManagement.Application.Contract.Services;
using DotNetRu.MeetupManagement.Domain;
using DotNetRu.MeetupManagement.Domain.Contract.Exceptions;

namespace DotNetRu.MeetupManagement.Application.Services
{
    public class MeetupDraftService : IMeetupDraftService
    {
        private readonly Domain.Drafts.IMeetupDraftService _meetupDraftService;

        public MeetupDraftService(Domain.Drafts.IMeetupDraftService meetupDraftService)
        {
            _meetupDraftService = meetupDraftService ?? throw new ArgumentNullException(nameof(meetupDraftService));
        }

        public MeetupDraft GetMeetupDraft(string communityId, string meetupId)
        {
            var meetupDraft = _meetupDraftService.GetMeetupDraft(new MeetupKey(communityId, meetupId));
            return MapMeetUpDraft(meetupDraft);
        }

        public MeetupDraft CreateMeetupDraft(string communityId, string name)
        {
            try
            {
                // ToDo: description here is missed
                var meetup = _meetupDraftService.CreateMeetupDraft(communityId, name, string.Empty);
                return MapMeetUpDraft(meetup);
            }
            catch (Exception ex)
            {
                if (ex is DomainException)
                    throw;
                throw new UnexpectedException("Create talk draft failed.", ex);
            }
        }

        public void UpdateMeetupDraft(string communityId, string meetupId, ICollection<string> talksIds, ICollection<string> speakerIds, string venueId)
        {
            // ToDo: add update method to domain service or get this meetup and check one by one all the parameters changes?
            // _meetupDraftService.UpdateMeetupDraft(meetupId);
            throw new NotImplementedException();
        }

        public void DeleteMeetupDraft(string communityId, string meetupId)
        {
            // ToDo: add delete method to doamin service
            // _meetupDraftService.RemoveMeetupDraft(meetupId);
        }

        public void SetVenue(string communityId, string meetupId, string venueId)
        {
            _meetupDraftService.SetVenueReference(new MeetupKey(communityId, meetupId), venueId);
        }

        public void AddFriend(string communityId, string meetupId, string friendId)
        {
            _meetupDraftService.AddFriendReference(new MeetupKey(communityId, meetupId), friendId);
        }

        public void DeleteFriend(string communityId, string meetupId, string friendId)
        {
            _meetupDraftService.RemoveFriendReference(new MeetupKey(communityId, meetupId), friendId);
        }

        public void AddTalkDraft(string communityId, string meetupId, string talkDraftId)
        {
            _meetupDraftService.AddTalkDraft(new MeetupKey(communityId, meetupId), talkDraftId);
        }

        public void DeleteTalkDraft(string communityId, string meetupId, string talkDraftId)
        {
            _meetupDraftService.RemoveTalkDraft(new MeetupKey(communityId, meetupId), talkDraftId);
        }

        private MeetupDraft MapMeetUpDraft(Domain.Drafts.MeetupDraft domainMeetUp)
        {
            var mapped = new MeetupDraft(domainMeetUp.Key.Id, domainMeetUp.Key.CommunityId);
            mapped.Friends.Assign(domainMeetUp.Friends.Select(GetFriendReference));

            // ToDo: map talks references to talks
            // mapped.Talks.Assign(domainMeetUp.Talks);
            return mapped;
        }

        private FriendReference GetFriendReference(EntityReference source)
        {
            return new FriendReference(source.Id, source.Name, source.IsDraft);
        }
    }
}