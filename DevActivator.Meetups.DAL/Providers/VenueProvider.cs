using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Common.BL.Config;
using DevActivator.Common.DAL;
using DevActivator.Meetups.BL.Entities;
using DevActivator.Meetups.BL.Interfaces;
using DevActivator.Meetups.DAL.Config;
using Microsoft.Extensions.Logging;

namespace DevActivator.Meetups.DAL.Providers
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