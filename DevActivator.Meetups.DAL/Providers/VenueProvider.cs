using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Common.BL.Config;
using DevActivator.Common.DAL;
using DevActivator.Meetups.BL.Entities;
using DevActivator.Meetups.BL.Interfaces;
using DevActivator.Meetups.DAL.Config;
using DevActivator.Meetups.DAL.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DevActivator.Meetups.DAL.Providers
{
    public class VenueProvider :  IVenueProvider
    {
        private readonly DotNetRuServerContext _context;

        public VenueProvider(DotNetRuServerContext context)
        {
            _context = context;
        }

        public Task<List<Venue>> GetAllVenuesAsync()
            => _context.Venues.ToListAsync();

        public Task<Venue> GetVenueOrDefaultAsync(string venueId)
            => _context.Venues.FirstOrDefaultAsync(x => x.ExportId == venueId);

        public async Task<Venue> SaveVenueAsync(Venue venue)
        {
            await _context.Venues.AddAsync(venue);
            await _context.SaveChangesAsync();
            return venue;
        }
    }
}