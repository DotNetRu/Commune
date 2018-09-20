
namespace DotNetRu.MeetupManagement.Domain.Drafts
{
    public interface ISpeakerDraftService
    {
        /// <exception cref="Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Contract.Exceptions.TalkNotFoundException" />
        SpeakerDraft CreateSpeaker(TalkKey key, CreateSpeakerDraftParameters parameters);

        /// <exception cref="Contract.Exceptions.SpeakerNotFoundException" />
        void UpdateSpeakerDraft(SpeakerDraft draft);

        /// <exception cref="Contract.Exceptions.SpeakerNotFoundException" />
        void DeleteSpeakerDraft(string speakerDraftId);
    }
}
