using System.Threading.Tasks;
using DevActivator.Common.BL.Config;
using DevActivator.Meetup.BL.Interfaces;
using DevActivator.Meetup.DAL.Providers;
using Xunit;

namespace DevActivator.Meetup.Tests.ProviderTests
{
    public class TalkProviderTests
    {
        [Fact]
        public async Task TalkSpeakerIdsDeserializationSucceed()
        {
            // prepare
            var settings = new Settings {AuditRepoDirectory = "/Users/alex-mbp/repos/Audit"};
            ITalkProvider talkProvider = new TalkProvider(null, settings);
            var testTalkId = "Round-table-Talk-about-Performance";
            
            // test
            var talk = await talkProvider.GetTalkOrDefaultAsync(testTalkId);
            Assert.Equal(6, talk.SpeakerIds.Count);
        }
    }
}