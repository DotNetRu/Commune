using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Meetup.BL.Entities;

namespace DevActivator.Meetup.BL.Interfaces
{
    public interface IVenueProvider
    {
        Task<List<Venue>> GetAllVenuesAsync();

        Task<Venue> GetVenueOrDefaultAsync(string venueId);

        Task<Venue> SaveVenueAsync(Venue venue);
    }
}