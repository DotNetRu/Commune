using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevActivator.Meetups.BL.Entities;
using DevActivator.Meetups.BL.Interfaces;
using DevActivator.Meetups.DAL.Database;
using Microsoft.EntityFrameworkCore;

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

        public Task<List<Speaker>> GetSpeakersByIdsAsync(List<string> ids)
            => _context.Speakers.Where(x => ids.Contains(x.ExportId)).ToListAsync();

        public async Task<Speaker> SaveSpeakerAsync(Speaker speaker)
        {
            await _context.Speakers.AddAsync(speaker);
            await _context.SaveChangesAsync();
            return speaker;
        }
    }
}