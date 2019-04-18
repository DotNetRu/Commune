using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetRuSerber.Meetups.BL.Models;

namespace DotNetRuSerber.Meetups.BL.Interfaces
{
    public interface IVenueService
    {
        Task<List<AutocompleteRow>> GetAllVenuesAsync();

        Task<VenueVm> GetVenueAsync(string venueId);

        Task<VenueVm> AddVenueAsync(VenueVm venue);

        Task<VenueVm> UpdateVenueAsync(VenueVm venue);
    }
}