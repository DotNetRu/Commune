using System;
using DotNetRu.MeetupManagement.Domain.Drafts;

namespace DotNetRu.MeetupManagement.Infrastructure.EFCore
{
    public class VenueDraftRepository : IVenueDraftRepository
    {
        /// <exception cref="Domain.Contract.Exceptions.VenueNotFoundException" />
        public VenueDraft Get(string id)
        {
            throw new NotImplementedException();
        }

        /// <exception cref="Domain.Contract.Exceptions.VenueNotFoundException" />
        public void Update(VenueDraft entity)
        {
            throw new NotImplementedException();
        }

        /// <exception cref="Domain.Contract.Exceptions.VenueNotFoundException" />
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public VenueDraft Add(CreateVenueDraftParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
