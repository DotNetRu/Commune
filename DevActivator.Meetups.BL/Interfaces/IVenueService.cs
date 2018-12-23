using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Meetups.BL.Models;

namespace DevActivator.Meetups.BL.Interfaces
{
    public interface IVenueService
    {
        Task<List<AutocompleteRow>> GetAllVenuesAsync();

        Task<VenueVm> GetVenueAsync(string venueId);

        Task<VenueVm> AddVenueAsync(VenueVm venue);

        Task<VenueVm> UpdateVenueAsync(VenueVm venue);
    }
}