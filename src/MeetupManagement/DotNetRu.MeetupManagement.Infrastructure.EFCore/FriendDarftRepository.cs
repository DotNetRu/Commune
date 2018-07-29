using System;
using DotNetRu.MeetupManagement.Domain.Drafts;

namespace DotNetRu.MeetupManagement.Infrastructure.EFCore
{
    public class FriendDarftRepository : IFriendDraftRepository
    {
        /// <exception cref="Domain.Contract.Exceptions.FriendNotFoundException" />
        public FriendDraft Get(string id)
        {
            throw new NotImplementedException();
        }

        /// <exception cref="Domain.Contract.Exceptions.FriendNotFoundException" />
        public void Update(FriendDraft entity)
        {
            throw new NotImplementedException();
        }

        /// <exception cref="Domain.Contract.Exceptions.FriendNotFoundException" />
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public FriendDraft Add(CreateFriendDraftParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
