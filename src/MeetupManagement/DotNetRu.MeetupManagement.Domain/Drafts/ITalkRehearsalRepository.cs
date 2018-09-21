using System;
using DotNetRu.MeetupManagement.Domain.Common;

namespace DotNetRu.MeetupManagement.Domain.Drafts
{
    public interface ITalkRehearsalRepository : IRepository<TalkRehearsal, TalkRehearsalKey>
    {
        /// <summary>
        /// Store new talk rehearsal
        /// </summary>
        /// <exception cref="Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Contract.Exceptions.TalkNotFoundException" />
        TalkRehearsal Add(TalkKey key, string comments, DateTimeOffset time);
    }
}