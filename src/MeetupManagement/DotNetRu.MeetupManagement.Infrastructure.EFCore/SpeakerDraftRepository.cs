using System;
using DotNetRu.MeetupManagement.Domain.Drafts;

namespace DotNetRu.MeetupManagement.Infrastructure.EFCore
{
    public class SpeakerDraftRepository : ISpeakerDraftRepository
    {
        /// <exception cref="Domain.Contract.Exceptions.SpeakerNotFoundException" />
        public SpeakerDraft Get(string id)
        {
            throw new NotImplementedException();
        }

        /// <exception cref="Domain.Contract.Exceptions.SpeakerNotFoundException" />
        public void Update(SpeakerDraft entity)
        {
            throw new NotImplementedException();
        }

        /// <exception cref="Domain.Contract.Exceptions.SpeakerNotFoundException" />
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public SpeakerDraft Add(CreateSpeakerDraftParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
