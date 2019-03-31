using System;
using System.Collections.Generic;
using System.Linq;
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
    public class MeetupProvider :  IMeetupProvider
    {
        private readonly DotNetRuServerContext _context;

        public MeetupProvider(DotNetRuServerContext context)
        {
            _context = context;
        }

        public Task<List<Meetup>> GetAllMeetupsAsync()
            => _context.Meetups.OrderBy(x => x.Id).ToListAsync();

        public Task<Meetup> GetMeetupOrDefaultAsync(string meetupId)
            => _context.Meetups.FirstOrDefaultAsync(x => x.ExportId == meetupId);

        public async Task<Meetup> SaveMeetupAsync(Meetup meetup)
        {
            await _context.Meetups.AddAsync(meetup);
            await _context.SaveChangesAsync();
            return meetup;
        }
    }
}