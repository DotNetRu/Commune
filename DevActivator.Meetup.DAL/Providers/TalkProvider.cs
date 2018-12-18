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
    public class TalkProvider : BaseProvider<Talk>, ITalkProvider
    {
        public TalkProvider(ILogger<TalkProvider> l, Settings s) : base(l, s, TalkConfig.DirectoryName)
        {
        }

        public Task<List<Talk>> GetAllTalksAsync()
            => GetAllAsync();

        public Task<Talk> GetTalkOrDefaultAsync(string talkId)
            => GetEntityByIdAsync(talkId);

        public Task<Talk> SaveTalkAsync(Talk talk)
            => SaveEntityAsync(talk);
    }
}