using System;
using System.Linq;
using DevActivator.Meetups.BL.Entities;
using DevActivator.Meetups.BL.Enums;
using DevActivator.Meetups.BL.Models;

namespace DevActivator.Meetups.BL.Extensions
{
    public static class MeetupExtensions
    {
        public static MeetupVm EnsureIsValid(this MeetupVm meetup)
        {
            // todo: implement full validation
            if (string.IsNullOrWhiteSpace(meetup.Name))
            {
                throw new FormatException(nameof(meetup.Name));
            }

            if (meetup.FriendIds == null || meetup.FriendIds.Count == 0 ||
                meetup.FriendIds.Any(x => string.IsNullOrWhiteSpace(x.FriendId)))
            {
                throw new FormatException(nameof(meetup.FriendIds));
            }

            if (string.IsNullOrWhiteSpace(meetup.VenueId))
            {
                throw new FormatException(nameof(meetup.VenueId));
            }

            if (meetup.Sessions == null || meetup.Sessions.Count == 0 ||
                meetup.Sessions.Any(x => string.IsNullOrWhiteSpace(x.TalkId)))
            {
                throw new FormatException(nameof(meetup.Sessions));
            }

            return meetup;
        }

        public static MeetupVm ToVm(this Meetup meetup)
            => new MeetupVm
            {
                Id = meetup.Id,
                Name = meetup.Name,
                CommunityId = meetup.CommunityId.GetCommunity(),
                FriendIds = meetup.FriendIds.Select(x => new FriendReference {FriendId = x}).ToList(),
                VenueId = meetup.VenueId,
                Sessions = meetup.Sessions.Select(x => new Session
                {
                    TalkId = x.TalkId,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime
                }).ToList(),
            };

        public static Meetup Extend(this Meetup original, MeetupVm meetup)
            => new Meetup
            {
                Id = original.Id,
                Name = meetup.Name,
                CommunityId = meetup.CommunityId.ToString(),
                FriendIds = meetup.FriendIds.Select(x => x.FriendId).ToList(),
                VenueId = meetup.VenueId,
                Sessions = meetup.Sessions.Select(x => new Session
                {
                    TalkId = x.TalkId,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime
                }).ToList(),
                TalkIds = meetup.Sessions.Select(x => x.TalkId).ToList(),
            };

        private static Community GetCommunity(this string id)
            => (Community) Enum.Parse(typeof(Community), id, true);
    }
}