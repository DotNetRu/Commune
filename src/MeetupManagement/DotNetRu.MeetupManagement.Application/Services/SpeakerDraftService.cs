using System;
using DotNetRu.MeetupManagement.Application.Contract.Models;
using DotNetRu.MeetupManagement.Application.Contract.Services;

namespace DotNetRu.MeetupManagement.Application.Services
{
    public class SpeakerDraftService : ISpeakerDraftService
    {
        private readonly Domain.Drafts.ISpeakerDraftService _speakerDraftService;

        public SpeakerDraftService(Domain.Drafts.ISpeakerDraftService speakerDraftService)
        {
            _speakerDraftService = speakerDraftService ?? throw new ArgumentNullException(nameof(speakerDraftService));
        }

        public SpeakerDraft CreteSpeakerDraft(string communityId, string talkDraftId, CreateSpeakerDraftParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void UpdateSpeakerDraft(SpeakerDraft draft)
        {
            throw new NotImplementedException();
        }

        public void DeleteSpeakerDraft(string speakerId)
        {
            throw new NotImplementedException();
        }
    }
}
