using System;
using DotNetRuServer.Comon.BL.Config;
using DotNetRuServer.Comon.BL.Extensions;
using DotNetRuServer.Meetups.BL.Entities;
using DotNetRuServer.Meetups.BL.Helpers;
using DotNetRuServer.Meetups.BL.Models;

namespace DotNetRuServer.Meetups.BL.Extensions
{
    public static class SpeakerExtensions
    {
        public static SpeakerVm EnsureIsValid(this SpeakerVm speaker)
        {
            // todo: implement full validation
            if (string.IsNullOrWhiteSpace(speaker.Name))
            {
                throw new FormatException(nameof(speaker.Name));
            }

            if (string.IsNullOrWhiteSpace(speaker.Description))
            {
                throw new FormatException(nameof(speaker.Description));
            }

            return speaker;
        }

        public static Speaker Extend(this Speaker original, SpeakerVm speaker)
            // todo: allow empty fields (when possible)
            => new Speaker
            {
                ExportId = speaker.Id,
                Name = string.IsNullOrWhiteSpace(speaker.Name)
                    ? original.Name
                    : speaker.Name,
                CompanyName = string.IsNullOrWhiteSpace(speaker.CompanyName)
                    ? original.CompanyName
                    : speaker.CompanyName,
                CompanyUrl = string.IsNullOrWhiteSpace(speaker.CompanyUrl)
                    ? original.CompanyUrl
                    : speaker.CompanyUrl,
                Description = string.IsNullOrWhiteSpace(speaker.Description)
                    ? original.Description
                    : speaker.Description,
                BlogUrl = string.IsNullOrWhiteSpace(speaker.BlogUrl)
                    ? original.BlogUrl
                    : speaker.BlogUrl,
                ContactsUrl = string.IsNullOrWhiteSpace(speaker.ContactsUrl)
                    ? original.ContactsUrl
                    : speaker.ContactsUrl,
                TwitterUrl = string.IsNullOrWhiteSpace(speaker.TwitterUrl)
                    ? original.TwitterUrl
                    : speaker.TwitterUrl,
                HabrUrl = string.IsNullOrWhiteSpace(speaker.HabrUrl)
                    ? original.HabrUrl
                    : speaker.HabrUrl,
                GitHubUrl = string.IsNullOrWhiteSpace(speaker.GitHubUrl)
                    ? original.GitHubUrl
                    : speaker.GitHubUrl,
            };

        public static SpeakerVm ToVm(this Speaker speaker)
            => new SpeakerVm
            {
                Id = speaker.ExportId,
                Name = speaker.Name,
                CompanyName = speaker.CompanyName,
                CompanyUrl = speaker.CompanyUrl,
                Description = speaker.Description,
                BlogUrl = speaker.BlogUrl,
                ContactsUrl = speaker.ContactsUrl,
                TwitterUrl = speaker.TwitterUrl,
                HabrUrl = speaker.HabrUrl,
                GitHubUrl = speaker.GitHubUrl,
                LastUpdateDate = speaker.LastUpdateDate.ToString()
            };
    }
}