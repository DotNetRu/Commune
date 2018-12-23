using System.Threading.Tasks;
using DevActivator.Common.BL.Config;
using DevActivator.Meetups.BL.Entities;
using DevActivator.Meetups.BL.Interfaces;
using DevActivator.Meetups.DAL.Providers;
using Xunit;

namespace DevActivator.Meetups.Tests.ProviderTests
{
    public class MeetupProviderTests
    {
        [Fact]
        public async Task MeetupDateDeserializationSucceed()
        {
            // prepare
            var meetup = await GetTestMeetupAsync();

            // test
            Assert.NotEmpty(meetup.Date);
        }

        [Fact]
        public async Task MeetupFriendIdsDeserializationSucceed()
        {
            // prepare
            var talk = await GetTestMeetupAsync();

            // test
            Assert.Equal(2, talk.FriendIds.Count);
        }

        [Fact]
        public async Task MeetupSessionsDeserializationSucceed()
        {
            // prepare
            var talk = await GetTestMeetupAsync();

            // test
            Assert.Equal(2, talk.Sessions.Count);
        }

        [Fact]
        public async Task MeetupTalkIdsDeserializationSucceed()
        {
            // prepare
            var talk = await GetTestMeetupAsync();

            // test
            Assert.Equal(2, talk.TalkIds.Count);
        }

        private Task<Meetup> GetTestMeetupAsync(string testMeetupId = "SpbDotNet-30")
        {
            var settings = new Settings {AuditRepoDirectory = "/Users/alex-mbp/repos/Audit"};
            IMeetupProvider talkProvider = new MeetupProvider(null, settings);
            return talkProvider.GetMeetupOrDefaultAsync(testMeetupId);
        }
    }
}