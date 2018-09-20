using DotNetRu.MeetupManagement.Application.Contract.Models;

namespace DotNetRu.MeetupManagement.Application.Contract.Services
{
    public interface ITalkDraftService
    {
        /// <summary>  
        ///  Create new talk draft.
        /// </summary>
        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" /> 
        TalkDraft CreateTalkDraft(string communityId, string title,string comment);

        /// <summary>  
        ///  Delete talk draft.
        /// </summary>
        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.TalkNotFoundException" />
        void DeleteTalkDraft(string communityId, string talkDraftId);

        /// <summary>  
        ///  Add new speaker to talk draft.
        /// </summary>
        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.TalkNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.SpeakerNotFoundException" />
        void AddSpeaker(string communityId, string talkDraftId, string speakerId);

        /// <summary>  
        ///  Remove speaker from talk draft.
        /// </summary>
        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.TalkNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.SpeakerNotFoundException" />
        void RemoveSpeaker(string communityId, string talkDraftId, string speakerId);

        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.TalkNotFoundException" /> 
        TalkRehearsal AddRehearsal(string communityId, string talkDraftId, string comment);

        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.TalkNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.TalkRehearsalNotFoundException" /> 
        void UpdateRehearsal(string communityId, string talkDraftId, TalkRehearsal rehearsal);
    }
}