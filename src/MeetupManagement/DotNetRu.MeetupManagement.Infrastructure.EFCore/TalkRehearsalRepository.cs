using System;
using DotNetRu.MeetupManagement.Domain;
using DotNetRu.MeetupManagement.Domain.Drafts;

namespace DotNetRu.MeetupManagement.Infrastructure.EFCore
{
    public class TalkRehearsalRepository : ITalkRehearsalRepository
    {
        public TalkRehearsal Get(TalkRehearsalKey id)
        {
            throw new NotImplementedException();
        }

        public void Update(TalkRehearsal entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(TalkRehearsalKey id)
        {
            throw new NotImplementedException();
        }

        public TalkRehearsal Add(TalkKey key, string comments, DateTimeOffset time)
        {
            throw new NotImplementedException();
        }
    }
}