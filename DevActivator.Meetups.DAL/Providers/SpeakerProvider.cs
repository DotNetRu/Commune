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
    public class SpeakerProvider :  ISpeakerProvider
    {
//        public SpeakerProvider(ILogger<SpeakerProvider> l, Settings s) : base(l, s, SpeakerConfig.DirectoryName)
//        {
//        }

        public Task<List<Speaker>> GetAllSpeakersAsync()
            => throw new NotImplementedException(); // GetAllAsync();

        public Task<Speaker> GetSpeakerOrDefaultAsync(string speakerId)
            => throw new NotImplementedException(); // GetEntityByIdAsync(speakerId);

        public Task<Speaker> SaveSpeakerAsync(Speaker speaker)
            => throw new NotImplementedException(); // SaveEntityAsync(speaker);
    }
}