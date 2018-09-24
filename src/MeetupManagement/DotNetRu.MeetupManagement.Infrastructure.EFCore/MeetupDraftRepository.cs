using System;
using DotNetRu.MeetupManagement.Domain;
using DotNetRu.MeetupManagement.Domain.Drafts;

namespace DotNetRu.MeetupManagement.Infrastructure.EFCore
{
    public class MeetupDraftRepository : IMeetupDraftRepository
    {
        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.MeetupNotFoundException" />
        public MeetupDraft GetEntity(MeetupKey id)
        {
            throw new NotImplementedException();
        }

        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.MeetupNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.TalkNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.TalkRehearsalNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.SpeakerNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.VenueNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.FriendNotFoundException" />
        public void Update(MeetupDraft entity)
        {
            throw new NotImplementedException();
        }

        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.MeetupNotFoundException" />
        public void Delete(MeetupKey id)
        {
            throw new NotImplementedException();
        }

        public MeetupDraft Add(string name)
        {
            throw new NotImplementedException();
        }
    }
}
