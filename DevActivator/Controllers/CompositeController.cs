using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevActivator.Meetups.BL.Entities;
using DevActivator.Meetups.BL.Extensions;
using DevActivator.Meetups.BL.Interfaces;
using DevActivator.Meetups.BL.Models;
using DevActivator.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevActivator.Controllers
{
    [Route("api/[controller]")]
    public class CompositeController : Controller
    {
        private readonly IMeetupService _meetupService;
        private readonly ITalkService _talkService;
        private readonly ISpeakerService _speakerService;
        private readonly IFriendService _friendService;
        private readonly IVenueService _venueService;

        public CompositeController(IMeetupService ms, ITalkService ts, ISpeakerService ss, IFriendService fs, IVenueService vs)
        {
            _meetupService = ms;
            _talkService = ts;
            _speakerService = ss;
            _friendService = fs;
            _venueService = vs;
        }

        [HttpPost("[action]/{meetupId?}")]
        public async Task<CompositeModel> GetMeetup([FromRoute] string meetupId, [FromBody] RandomConcatModel descriptor = null)
        {
            var meetup = await _meetupService.GetMeetupAsync(meetupId).ConfigureAwait(true);

            descriptor = descriptor ?? new RandomConcatModel();
            descriptor.VenueId = descriptor.VenueId;
            descriptor.Sessions = descriptor.Sessions ?? new List<SessionVm>();
            descriptor.TalkIds = descriptor.TalkIds ?? new List<string>();
            descriptor.SpeakerIds = descriptor.SpeakerIds ?? new List<string>();
            descriptor.FriendIds = descriptor.FriendIds ?? new List<string>();

            if (meetup != null && descriptor.Sessions.Count == 0)
            {
                descriptor.Sessions = meetup.Sessions;
            }

            descriptor.TalkIds.AddRange(descriptor.Sessions.Select(x => x.TalkId).Where(x => !string.IsNullOrWhiteSpace(x)));

            // talks
            var talks = new Dictionary<string, TalkVm>();
            foreach (var talkId in descriptor.TalkIds.Distinct())
            {
                var talk = await _talkService.GetTalkAsync(talkId).ConfigureAwait(true);
                talks.Add(talkId, talk);
            }

            // speakers
            descriptor.SpeakerIds.AddRange(
                talks.Select(x => x.Value).SelectMany(x => x.SpeakerIds.Select(s => s.SpeakerId))
            );
            var speakers = new Dictionary<string, SpeakerVm>();
            foreach (var speakerId in descriptor.SpeakerIds.Distinct())
            {
                var speaker = await _speakerService.GetSpeakerAsync(speakerId).ConfigureAwait(true);
                speakers.Add(speakerId, speaker);
            }

            // friends
            if (meetup != null && descriptor.FriendIds.Count == 0)
            {
                descriptor.FriendIds.AddRange(meetup.FriendIds.Select(x => x.FriendId));
            }

            var friends = new List<Friend>();
            foreach (var friendId in descriptor.FriendIds.Distinct())
            {
                var friend = await _friendService.GetFriendAsync(friendId).ConfigureAwait(true);
                friends.Add(friend);
            }

            // name
            if (string.IsNullOrWhiteSpace(descriptor.Name))
            {
                descriptor.Name = meetup?.Name;
            }

            // venue
            descriptor.VenueId = descriptor.VenueId ?? meetup?.VenueId;
            VenueVm venue = null;
            if (!string.IsNullOrWhiteSpace(descriptor.VenueId))
            {
                venue = await _venueService.GetVenueAsync(descriptor.VenueId).ConfigureAwait(true);
            }

            // community
            if (string.IsNullOrWhiteSpace(descriptor.CommunityId))
            {
                descriptor.CommunityId = meetup?.CommunityId.ToString();
            }

            return new CompositeModel
            {
                Id = meetup?.Id,
                Name = descriptor.Name,
                CommunityId = descriptor.CommunityId.GetCommunity(),
                Venue = venue,
                Sessions = descriptor.Sessions,
                Talks = talks,
                Speakers = speakers,
                Friends = friends
            };
        }

        [HttpPost("[action]/{meetupId?}")]
        public async Task<CompositeModel> SaveMeetup([FromRoute] string meetupId, [FromBody] RandomConcatModel descriptor = null)
        {
            var oldMeetup = await _meetupService.GetMeetupAsync(meetupId).ConfigureAwait(true);

            if (oldMeetup != null)
            {
                Extend(oldMeetup, descriptor);
                var savedMeetup = await _meetupService.UpdateMeetupAsync(oldMeetup).ConfigureAwait(true);
                meetupId = savedMeetup.Id;
            }
            else
            {
                var newMeetup = new MeetupVm {Id = meetupId};
                Extend(newMeetup, descriptor);

                var savedMeetup = await _meetupService.AddMeetupAsync(newMeetup).ConfigureAwait(true);
                meetupId = savedMeetup.Id;
            }

            return await GetMeetup(meetupId).ConfigureAwait(true);
        }

        private static void Extend(MeetupVm meetup, RandomConcatModel descriptor)
        {
            if (descriptor != null)
            {
                if (!string.IsNullOrWhiteSpace(descriptor.Name))
                {
                    meetup.Name = descriptor.Name;
                }

                if (!string.IsNullOrWhiteSpace(descriptor.CommunityId))
                {
                    meetup.CommunityId = descriptor.CommunityId.GetCommunity();
                }

                if (descriptor.FriendIds != null && descriptor.FriendIds.Count != 0)
                {
                    meetup.FriendIds = descriptor.FriendIds.Select(x => new FriendReference {FriendId = x}).ToList();
                }

                if (!string.IsNullOrWhiteSpace(descriptor.VenueId))
                {
                    meetup.VenueId = descriptor.VenueId;
                }

                if (descriptor.Sessions != null && descriptor.Sessions.Count != 0)
                {
                    meetup.Sessions = descriptor.Sessions;
                }
            }
        }
    }
}