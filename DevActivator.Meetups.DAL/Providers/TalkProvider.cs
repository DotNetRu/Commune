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

        public async Task<Talk> SaveTalkAsync(Talk talk)
        {
            await _context.Talks.AddAsync(talk);
            await _context.SaveChangesAsync();
            return talk;
        }
    }
}