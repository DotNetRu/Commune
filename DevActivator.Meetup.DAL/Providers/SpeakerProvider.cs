using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Common.BL.Config;
using DevActivator.Common.DAL;
using DevActivator.Meetup.BL.Entities;
using DevActivator.Meetup.BL.Interfaces;
using DevActivator.Meetup.DAL.Config;
using Microsoft.Extensions.Logging;

namespace DevActivator.Meetup.DAL.Providers
{
    public class SpeakerProvider : BaseProvider<Speaker>, ISpeakerProvider
    {
        public SpeakerProvider(ILogger<SpeakerProvider> l, Settings s) : base(l, s, SpeakerConfig.DirectoryName)
        {
        }

        public Task<List<Speaker>> GetAllSpeakersAsync()
            => GetAllAsync();

        public Task<Speaker> GetSpeakerOrDefaultAsync(string speakerId)
            => GetEntityByIdAsync(speakerId);

        public Task<Speaker> SaveSpeakerAsync(Speaker speaker)
            => SaveEntityAsync(speaker);
    }
}