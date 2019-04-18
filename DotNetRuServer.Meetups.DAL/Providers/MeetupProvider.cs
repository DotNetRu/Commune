using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetRuSerber.Meetups.BL.Entities;
using DotNetRuSerber.Meetups.BL.Interfaces;
using DotNetRuServer.Meetups.DAL.Database;
using Microsoft.EntityFrameworkCore;

namespace DotNetRuServer.Meetups.DAL.Providers
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
        
        
        public Task<Meetup> GetMeetupOrDefaultExtendedAsync(string meetupId) 
         => _context.Meetups
             .Include(x => x.Venue)
             .Include(x => x.Friends).ThenInclude(x => x.Friend)
             .Include(x => x.Community)
             .Include(x => x.Sessions).ThenInclude(x => x.Talk)
             .FirstOrDefaultAsync(x => x.ExportId == meetupId);

        public async Task<Meetup> SaveMeetupAsync(Meetup meetup)
        {
            await _context.Meetups.AddAsync(meetup);
            await _context.SaveChangesAsync();
            return meetup;
        }
    }
}