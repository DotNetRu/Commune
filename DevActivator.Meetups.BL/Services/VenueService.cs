using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevActivator.Meetups.BL.Entities;
using DevActivator.Meetups.BL.Extensions;
using DevActivator.Meetups.BL.Interfaces;
using DevActivator.Meetups.BL.Models;

namespace DevActivator.Meetups.BL.Services
{
    public class VenueService : IVenueService
    {
        private readonly IVenueProvider _venueProvider;
        private readonly IUnitOfWork _unitOfWork;

        public VenueService(IVenueProvider venueProvider, IUnitOfWork unitOfWork)
        {
            _venueProvider = venueProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<AutocompleteRow>> GetAllVenuesAsync()
        {
            var venues = await _venueProvider.GetAllVenuesAsync().ConfigureAwait(false);
            return venues
                .Select(x => new AutocompleteRow {Id = x.ExportId, Name = x.Name})
                .ToList();
        }

        public async Task<VenueVm> GetVenueAsync(string venueId)
        {
            var venue = await _venueProvider.GetVenueOrDefaultAsync(venueId).ConfigureAwait(false);
            return venue.ToVm();
        }

        public async Task<VenueVm> AddVenueAsync(VenueVm venue)
        {
            venue.EnsureIsValid();
            
            var original = await _venueProvider.GetVenueOrDefaultAsync(venue.Id).ConfigureAwait(false);
            if (original != null)
            {
                throw new FormatException($"Данный {nameof(venue.Id)} \"{venue.Id}\" уже занят");
            }

            var entity = new Venue {ExportId = venue.Id}.Extend(venue);
            var res = await _venueProvider.SaveVenueAsync(entity).ConfigureAwait(false);
            return res.ToVm();
        }

        public async Task<VenueVm> UpdateVenueAsync(VenueVm venue)
        {
            venue.EnsureIsValid();
            
            var original = await _venueProvider.GetVenueOrDefaultAsync(venue.Id).ConfigureAwait(false);
            original. ExportId = venue.Id;
            original.Name = venue.Name;
            original.City = venue.City;
            original.Address = venue.Address;
            original.MapUrl = venue.MapUrl;
            await _unitOfWork.SaveChangesAsync();
            
            return original.ToVm();
        }
    }
}