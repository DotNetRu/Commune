using System;
using DotNetRu.MeetupManagement.Domain;
using DotNetRu.MeetupManagement.Domain.Drafts;

namespace DotNetRu.MeetupManagement.Infrastructure.EFCore
{
    public class TalkRehearsalRepository : ITalkRehearsalRepository
    {
        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.TalkNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.SpeakerNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.TalkRehearsalNotFoundException" />
        public TalkRehearsal GetEntity(TalkRehearsalKey id)
        {
            throw new NotImplementedException();
        }

        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.TalkNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.SpeakerNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.TalkRehearsalNotFoundException" />
        public void Update(TalkRehearsal entity)
        {
            throw new NotImplementedException();
        }

        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.TalkNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.SpeakerNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.TalkRehearsalNotFoundException" />
        public void Delete(TalkRehearsalKey id)
        {
            throw new NotImplementedException();
        }

        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.TalkNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.SpeakerNotFoundException" />
        public TalkRehearsal Add(TalkKey key, string comments, DateTimeOffset time)
        {
            throw new NotImplementedException();
        }
    }
}