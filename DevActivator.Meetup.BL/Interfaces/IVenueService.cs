using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Meetup.BL.Models;

namespace DevActivator.Meetup.BL.Interfaces
{
    public interface IVenueService
    {
        Task<List<AutocompleteRow>> GetAllVenuesAsync();

        Task<VenueVm> GetVenueAsync(string venueId);

        Task<VenueVm> AddVenueAsync(VenueVm venue);

        Task<VenueVm> UpdateVenueAsync(VenueVm venue);
    }
}