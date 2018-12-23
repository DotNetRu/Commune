using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Common.BL.Config;
using DevActivator.Common.DAL;
using DevActivator.Meetups.BL.Entities;
using DevActivator.Meetups.BL.Interfaces;
using DevActivator.Meetups.DAL.Config;
using Microsoft.Extensions.Logging;

namespace DevActivator.Meetups.DAL.Providers
{
    public class MeetupProvider : BaseProvider<Meetup>, IMeetupProvider
    {
        public MeetupProvider(ILogger<MeetupProvider> l, Settings s) : base(l, s, MeetupConfig.DirectoryName)
        {
        }

        public Task<List<Meetup>> GetAllMeetupsAsync()
            => GetAllAsync();

        public Task<Meetup> GetMeetupOrDefaultAsync(string meetupId)
            => GetEntityByIdAsync(meetupId);

        public Task<Meetup> SaveMeetupAsync(Meetup meetup)
            => SaveEntityAsync(meetup);
    }
}