using System;
using System.Collections.Generic;
using System.Text;
using DotNetRu.MeetupManagement.Application.Contract.Models;
using DotNetRu.MeetupManagement.Application.Contract.Services;

namespace DotNetRu.MeetupManagement.Application.Services
{
    public class FriendDraftService : IFriendDraftService
    {
        public FriendDraft CreateFriendDraft(CreateFriendDraftParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void DeleteFriendDraft(string friendDraftId)
        {
            throw new NotImplementedException();
        }

        public ICollection<MeetupDraft> GetMeetups(string friendDraftId)
        {
            throw new NotImplementedException();
        }

        public void UpdateFriendDraft(FriendDraft friendDraft)
        {
            throw new NotImplementedException();
        }
    }
}
