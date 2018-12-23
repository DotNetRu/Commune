using System;
using System.Linq;
using System.Threading.Tasks;
using DevActivator.Common.BL.Config;
using DevActivator.Meetups.DAL.Providers;
using DevActivator.Meetups.BL.Extensions;
using DevActivator.Meetups.BL.Interfaces;
using Xunit;

namespace DevActivator.Meetups.Tests.ProviderTests
{
    public class SpeakerProviderTests
    {
        [Fact]
        public async Task Test1()
        {
            // prepare
            var settings = new Settings {AuditRepoDirectory = "/Users/alex-mbp/repos/Audit"};
            ISpeakerProvider speakerProvider = new SpeakerProvider(null, settings);

            // test
            var speakers = await speakerProvider.GetAllSpeakersAsync();
            Assert.NotNull(speakers);
            Assert.NotEmpty(speakers);

            var speaker = speakers.First();

            var lastUpdateDate = speaker.GetLastUpdateDate(settings);
            var date = DateTime.Parse(lastUpdateDate);
            Assert.False(DateTime.MinValue == date);
        }
    }
}