using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Common.BL.Config;
using DevActivator.Common.DAL;
using DevActivator.Meetup.BL.Entities;
using DevActivator.Meetup.BL.Interfaces;
using DevActivator.Meetup.DAL.Config;
using Microsoft.Extensions.Logging;

namespace DevActivator.Meetup.DAL.Providers
{
    public class VenueProvider : BaseProvider<Venue>, IVenueProvider
    {
        public VenueProvider(ILogger<VenueProvider> l, Settings s) : base(l, s, VenueConfig.DirectoryName)
        {
        }

        public Task<List<Venue>> GetAllVenuesAsync()
            => GetAllAsync();

        public Task<Venue> GetVenueOrDefaultAsync(string venueId)
            => GetEntityByIdAsync(venueId);

        public Task<Venue> SaveVenueAsync(Venue venue)
            => SaveEntityAsync(venue);
    }
}