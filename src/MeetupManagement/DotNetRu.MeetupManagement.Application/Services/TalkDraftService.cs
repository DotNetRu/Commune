using System;
using System.Linq;
using DotNetRu.Common.Collections;
using DotNetRu.MeetupManagement.Application.Contract.Exceptions;
using DotNetRu.MeetupManagement.Domain;
using DotNetRu.MeetupManagement.Domain.Contract.Exceptions;
using CreateSpeakerDraftParameters = DotNetRu.MeetupManagement.Application.Contract.Models.CreateSpeakerDraftParameters;
using ITalkDraftService = DotNetRu.MeetupManagement.Application.Contract.Services.ITalkDraftService;
using SpeakerReference = DotNetRu.MeetupManagement.Application.Contract.Models.SpeakerReference;
using TalkDraft = DotNetRu.MeetupManagement.Application.Contract.Models.TalkDraft;
using TalkRehearsal = DotNetRu.MeetupManagement.Application.Contract.Models.TalkRehearsal;

namespace DotNetRu.MeetupManagement.Application.Services
{
    public class TalkDraftService : ITalkDraftService
    {
        private readonly Domain.Drafts.ITalkDraftService _draftTalkService;

        public TalkDraftService(Domain.Drafts.ITalkDraftService draftTalkService)
        {
            _draftTalkService = draftTalkService ?? throw new ArgumentNullException(nameof(draftTalkService));
        }

        public TalkDraft CreateTalkDraft(string communityId, string title, string comment)
        {
            try
            {
                var draftTalk = _draftTalkService.CreateTalkDraft(communityId, title, comment);
                return Convert(draftTalk);
            }
            catch (Exception ex)
            {
                if (ex is DomainException)
                    throw;
                throw new UnexpectedException("Create talk draft failed.", ex);
            }
        }

        public void DeleteTalkDraft(string communityId, string talkDraftId)
        {
            try
            {
                _draftTalkService.DeleteTalkDraft(new TalkKey(communityId, talkDraftId));
            }
            catch (Exception ex)
            {
                if (ex is DomainException)
                    throw;
                throw new UnexpectedException("Create talk draft failed.", ex);
            }
        }

        public void AddSpeaker(string communityId, string talkDraftId, string speakerId)
        {
            try
            {
                _draftTalkService.AddSpeaker(new TalkKey(communityId, talkDraftId), speakerId);
            }
            catch (Exception ex)
            {
                if (ex is DomainException)
                    throw;
                throw new UnexpectedException("Add speaker failed.", ex);
            }
        }

        public void RemoveSpeaker(string communityId, string talkDraftId, string speakerId)
        {
            try
            {
                var draftKey = new TalkKey(communityId, talkDraftId);
                _draftTalkService.RemoveSpeaker(draftKey, speakerId);
            }
            catch (Exception ex)
            {
                if (ex is DomainException)
                    throw;
                throw new UnexpectedException("Remove speaker failed.", ex);
            }
        }

        public TalkRehearsal AddRehearsal(string communityId, string talkDraftId, string comment)
        {
            throw new NotImplementedException();
        }

        public void UpdateRehearsal(string communityId, string talkDraftId, TalkRehearsal rehearsal)
        {
            throw new NotImplementedException();
        }

        private static TalkDraft Convert(Domain.Drafts.TalkDraft source)
        {
            var result = new TalkDraft(source.Key.Id)
            {
                CommunityId = source.Key.CommunityId,
                Title = source.Title,
                Comments = source.Comments
            };
            result.Rehearsals.Assign(source.Rehearsals.Select(Convert));
            result.Speakers.Assign(source.Speakers.Select(GetSpeakerReference));
            return result;
        }

        private static TalkRehearsal Convert(Domain.Drafts.TalkRehearsal source)
        {
            return new TalkRehearsal(source.Id) 
            {
                Comment = source.Comment
            };
        }

        private static SpeakerReference GetSpeakerReference(EntityReference source)
        {
            return new SpeakerReference(source.Id, source.Name, source.IsDraft);
        }

        private static Contract.Models.Company Convert(Company source)
        {
            return new Contract.Models.Company()
            {
                Name = source.Name,
                Url = source.Url
            };
        }

        private static Company Convert(Contract.Models.Company source)
        {
            return new Company()
            {
                Name = source.Name,
                Url = source.Url
            };
        }

        private static Domain.Drafts.CreateSpeakerDraftParameters Convert(
            CreateSpeakerDraftParameters source)
        {
            return new Domain.Drafts.CreateSpeakerDraftParameters()
            {
                BlogsUrl = source.BlogsUrl,
                Company = Convert(source.Company),
                ContactsUrl = source.ContactsUrl,
                Description = source.Description,
                FirstName = source.FirstName,
                GitHubUrl = source.GitHubUrl,
                LastName = source.LastName,
                TwitterUrl = source.TwitterUrl
            };
        }
    }
}