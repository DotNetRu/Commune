using DotNetRu.MeetupManagement.Application.Contract.Models;

namespace DotNetRu.MeetupManagement.Application.Contract.Services
{
    public interface ISpeakerDraftService
    {
        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.TalkNotFoundException" />
        SpeakerDraft CreteSpeakerDraft(string comminityId, string talkDraftId, CreateSpeakerDraftParameters parameters);

        /// <exception cref="Domain.Contract.Exceptions.SpeakerNotFoundException" />
        void UpdateSpeakerDraft(SpeakerDraft draft);

        /// <exception cref="Domain.Contract.Exceptions.SpeakerNotFoundException" />
        void DeleteSpeakerDraft(string speakerId);
    }
}
