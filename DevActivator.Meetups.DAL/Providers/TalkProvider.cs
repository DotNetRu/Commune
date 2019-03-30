using System;
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
    public class TalkProvider : ITalkProvider
    {
//        public TalkProvider(ILogger<TalkProvider> l, Settings s) : base(l, s, TalkConfig.DirectoryName)
//        {
//        }

        public Task<List<Talk>> GetAllTalksAsync()
            => throw new NotImplementedException(); //GetAllAsync();

        public Task<Talk> GetTalkOrDefaultAsync(string talkId)
            => throw new NotImplementedException(); //GetEntityByIdAsync(talkId);

        public Task<Talk> SaveTalkAsync(Talk talk)
            => throw new NotImplementedException(); //SaveEntityAsync(talk);
    }
}