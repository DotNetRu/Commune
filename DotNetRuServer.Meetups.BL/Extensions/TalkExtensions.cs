using System;
using System.Linq;
using DotNetRuServer.Meetups.BL.Entities;
using DotNetRuServer.Meetups.BL.Models;

namespace DotNetRuServer.Meetups.BL.Extensions
{
    public static class TalkExtensions
    {
        public static TalkVm EnsureIsValid(this TalkVm talk)
        {
            // todo: implement full validation
            if (string.IsNullOrWhiteSpace(talk.Title)) throw new FormatException(nameof(talk.Title));

            if (string.IsNullOrWhiteSpace(talk.Description)) throw new FormatException(nameof(talk.Description));

            if (talk.SpeakerIds == null || talk.SpeakerIds.Count == 0 ||
                talk.SpeakerIds.Any(x => string.IsNullOrWhiteSpace(x.SpeakerId)))
                throw new FormatException(nameof(talk.SpeakerIds));

            return talk;
        }

        public static TalkVm ToVm(this Talk talk)
        {
            return new TalkVm
            {
                Id = talk.ExportId,
                SpeakerIds = talk.Speakers.Select(x => new SpeakerReference {SpeakerId = x.Speaker.ExportId}).ToList(),
                Title = talk.Title,
                Description = talk.Description,
                CodeUrl = talk.CodeUrl,
                SlidesUrl = talk.SlidesUrl,
                VideoUrl = talk.VideoUrl
            };
        }

        public static TalkExportVm ToExportVm(this Talk talk)
        {
            return new TalkExportVm
            {
                Id = talk.ExportId,
                SpeakerIds = talk.Speakers.Select(x => x.Speaker.ExportId).ToList(),
                Title = talk.Title,
                Description = talk.Description,
                CodeUrl = talk.CodeUrl,
                SlidesUrl = talk.SlidesUrl,
                VideoUrl = talk.VideoUrl
            };
        }

        public static Talk Extend(this Talk original, TalkVm talk)
        {
            return new Talk
            {
                Id = original.Id,
                ExportId = talk.Id,
                Title = talk.Title,
                Description = talk.Description,
                CodeUrl = talk.CodeUrl,
                SlidesUrl = talk.SlidesUrl,
                VideoUrl = talk.VideoUrl,
                Speakers = original.Speakers,
                SeeAlsoTalks = original.SeeAlsoTalks
            };
        }
    }
}
