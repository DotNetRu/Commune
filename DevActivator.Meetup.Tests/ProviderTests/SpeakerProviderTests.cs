using System;
using System.Linq;
using System.Threading.Tasks;
using DevActivator.Common.BL.Config;
using DevActivator.Meetup.BL.Extensions;
using DevActivator.Meetup.BL.Interfaces;
using DevActivator.Meetup.DAL.Providers;
using Xunit;

namespace DevActivator.Meetup.Tests.ProviderTests
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