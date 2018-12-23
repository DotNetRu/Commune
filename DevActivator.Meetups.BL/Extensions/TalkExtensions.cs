using System;
using System.Linq;
using DevActivator.Meetups.BL.Entities;
using DevActivator.Meetups.BL.Models;

namespace DevActivator.Meetups.BL.Extensions
{
    public static class TalkExtensions
    {
        public static TalkVm EnsureIsValid(this TalkVm talk)
        {
            // todo: implement full validation
            if (string.IsNullOrWhiteSpace(talk.Title))
            {
                throw new FormatException(nameof(talk.Title));
            }

            if (string.IsNullOrWhiteSpace(talk.Description))
            {
                throw new FormatException(nameof(talk.Description));
            }

            if (talk.SpeakerIds == null || talk.SpeakerIds.Count == 0 ||
                talk.SpeakerIds.Any(x => string.IsNullOrWhiteSpace(x.SpeakerId)))
            {
                throw new FormatException(nameof(talk.SpeakerIds));
            }

            return talk;
        }

        public static TalkVm ToVm(this Talk talk)
            => new TalkVm
            {
                Id = talk.Id,
                SpeakerIds = talk.SpeakerIds.Select(x => new SpeakerReference {SpeakerId = x}).ToList(),
                Title = talk.Title,
                Description = talk.Description,
                CodeUrl = talk.CodeUrl,
                SlidesUrl = talk.SlidesUrl,
                VideoUrl = talk.VideoUrl
            };

        public static Talk Extend(this Talk original, TalkVm talk)
            => new Talk
            {
                Id = original.Id,
                SpeakerIds = talk.SpeakerIds.Select(x => x.SpeakerId).ToList(),
                Title = talk.Title,
                Description = talk.Description,
                CodeUrl = talk.CodeUrl,
                SlidesUrl = talk.SlidesUrl,
                VideoUrl = talk.VideoUrl
            };
    }
}