using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Meetups.BL.Entities;
using DevActivator.Meetups.BL.Interfaces;
using DevActivator.Meetups.DAL.Database;
using Microsoft.EntityFrameworkCore;

namespace DevActivator.Meetups.DAL.Providers
{
    public class TalkProvider : ITalkProvider
    {
        private readonly DotNetRuServerContext _context;

        public TalkProvider(DotNetRuServerContext context)
        {
            _context = context;
        }

        public Task<List<Talk>> GetAllTalksAsync()
            => _context.Talks.ToListAsync();

        public Task<Talk> GetTalkOrDefaultAsync(string talkId)
            => _context.Talks.FirstOrDefaultAsync(x => x.ExportId == talkId);

        public Task<Talk> GetTalkOrDefaultExtendedAsync(string talkId)
            => _context.Talks
                .Include(x => x.Speakers).ThenInclude(x => x.Speaker)
                .Include(x => x.SeeAlsoTalks)
                .FirstOrDefaultAsync(x => x.ExportId == talkId);

        public async Task<Talk> SaveTalkAsync(Talk talk)
        {
            await _context.Talks.AddAsync(talk);
            await _context.SaveChangesAsync();
            return talk;
        }
    }
}