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
    public class SpeakerProvider :  ISpeakerProvider
    {
        private readonly DotNetRuServerContext _context;

        public SpeakerProvider(DotNetRuServerContext context)
        {
            _context = context;
        }

        public Task<List<Speaker>> GetAllSpeakersAsync()
            => _context.Speakers.ToListAsync();

        public Task<Speaker> GetSpeakerOrDefaultAsync(string speakerId)
            => _context.Speakers.FirstOrDefaultAsync(x => x.ExportId == speakerId);

        public async Task<Speaker> SaveSpeakerAsync(Speaker speaker)
        {
            await _context.Speakers.AddAsync(speaker);
            await _context.SaveChangesAsync();
            return speaker;
        }
    }
}