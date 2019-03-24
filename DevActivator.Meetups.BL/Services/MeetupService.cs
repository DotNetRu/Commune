using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevActivator.Common.BL.Config;
using DevActivator.Meetups.BL.Entities;
using DevActivator.Meetups.BL.Extensions;
using DevActivator.Meetups.BL.Interfaces;
using DevActivator.Meetups.BL.Models;

namespace DevActivator.Meetups.BL.Services
{
    public class MeetupService : IMeetupService
    {
        private readonly Settings _settings;
        private readonly IMeetupProvider _meetupProvider;

        public MeetupService(Settings settings, IMeetupProvider meetupProvider)
        {
            _settings = settings;
            _meetupProvider = meetupProvider;
        }

        public async Task<List<AutocompleteRow>> GetAllMeetupsAsync()
        {
            var meetups = await _meetupProvider.GetAllMeetupsAsync().ConfigureAwait(false);
            return meetups
                .Select(x => new AutocompleteRow {Id = x.Id, Name = x.Name})
                .ToList();
        }

        public async Task<MeetupVm> GetMeetupAsync(string meetupId)
        {
            var meetup = await _meetupProvider.GetMeetupOrDefaultAsync(meetupId).ConfigureAwait(false);
            return meetup?.ToVm();
        }

        public async Task<MeetupVm> AddMeetupAsync(MeetupVm meetup)
        {
            meetup.EnsureIsValid();

            var original = await _meetupProvider.GetMeetupOrDefaultAsync(meetup.Id).ConfigureAwait(false);
            if (original != null)
            {
                throw new FormatException($"Данный {nameof(meetup.Id)} \"{meetup.Id}\" уже занят");
            }

            var entity = new Meetup {Id = meetup.Id}.Extend(meetup);
            var res = await _meetupProvider.SaveMeetupAsync(entity).ConfigureAwait(false);
            return res.ToVm();
        }

        public async Task<MeetupVm> UpdateMeetupAsync(MeetupVm meetup)
        {
            meetup.EnsureIsValid();
            var original = await _meetupProvider.GetMeetupOrDefaultAsync(meetup.Id).ConfigureAwait(false);
            var res = await _meetupProvider.SaveMeetupAsync(original.Extend(meetup)).ConfigureAwait(false);
            return res.ToVm();
        }
    }
}