using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DotNetRuServer.Meetups.BL.Entities;
using Microsoft.Extensions.Configuration;
using RazorLight;

namespace DotNetRuServer.Integration.TimePad
{
    public class TimePadIntegrationService
    {
        private const string TimePadSection = "TimePad";
        private const string CommunitySection = "Community";
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly RazorLightEngine _razorEngine;

        public TimePadIntegrationService(IHttpClientFactory factory, IConfiguration configuration)
        {
            _clientFactory = factory;
            _configuration = configuration;
            var path = Directory.GetCurrentDirectory();
            _razorEngine = new RazorLightEngineBuilder()
                .UseFilesystemProject(path)
                .UseMemoryCachingProvider()
                .Build();
        }

        public async Task CreateDraftEventAsync(Meetup meetup, CancellationToken ct)
        {
            var communityOrganization = await GetOrganizationAsync(meetup.Community, ct);
            if (communityOrganization is null)
                return;

            var timePadClient = new TimePadClient(_clientFactory.CreateClient($"TimePad-{meetup.Community.Id}"));

            var startsAt = meetup.Sessions.Min(s => s.StartTime);
            var endsAt = meetup.Sessions.Max(s => s.EndTime);
            var shortDescription =
                $"{startsAt.Day} {startsAt.Month} в гостях у компании {meetup.Friends.First().Friend.Name} состоится встреча {meetup.Community.Name}";
            var htmlDescription = await _razorEngine.CompileRenderAsync(
                $"TimePad/Templates/Template-community-{meetup.Community.Id}.cshtml",
                new {Name = meetup.Name});
            var questions = new List<QuestionInclude>
            {
                new QuestionInclude {Field_id = "mail", Is_mandatory = true, Name = "E-mail"},
                new QuestionInclude {Field_id = "surname", Is_mandatory = true, Name = "Фамилия"},
                new QuestionInclude {Field_id = "name", Is_mandatory = true, Name = "Имя"},
                new QuestionInclude {Field_id = "company", Is_mandatory = false, Name = "Компания"},
                new QuestionInclude {Field_id = "position", Is_mandatory = false, Name = "Должность"}
            };

            var createEventBody = new CreateEvent
            {
                Organization = new OrganizationInclude
                    {Id = communityOrganization.Id, Subdomain = communityOrganization.Subdomain},
                Starts_at = startsAt,
                Ends_at = endsAt,
                Name = meetup.Name,
                Description_short = shortDescription,
                Description_html = htmlDescription,
                Location = new LocationInclude {City = meetup.Community.City, Address = meetup.Venue.Address},
                Categories = new[] {new CategoryInclude {Id = 452, Name = "ИТ и интернет"}},
                Access_status = "draft",
                Tickets_limit = 150,
                Questions = questions
            };

            await timePadClient.AddEventAsync(createEventBody, ct);
        }

        public async Task PublishEventAsync(Meetup meetup, CancellationToken ct)
        {
            var communityOrganization = await GetOrganizationAsync(meetup.Community, ct);
            if (communityOrganization is null)
                return;

            var timePadClient = new TimePadClient(_clientFactory.CreateClient($"TimePad-{meetup.Community.Id}"));

            var events = await timePadClient.GetEventsAsync(
                null,
                10,
                0,
                new[] {"+starts_at"},
                null,
                null,
                null,
                null,
                new[] {communityOrganization.Id},
                null,
                null,
                null,
                null,
                null,
                new[] {"draft"},
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                ct
            );
            var meetupEvent = events.Values.FirstOrDefault(e => e.Name == meetup.Name);
            if (meetupEvent is null)
                return;

            var editEventBody = new EditEvent {Access_status = "public"};
            await timePadClient.EditEventAsync(meetupEvent.Id, editEventBody, ct);
        }

        private async Task<OrganizationResponse> GetOrganizationAsync(Community community, CancellationToken ct)
        {
            var timePadSection = _configuration.GetSection($"{CommunitySection}-{community.Id}")
                .GetSection(TimePadSection);
            if (timePadSection is null)
                return null;

            var timePadClient = new TimePadClient(_clientFactory.CreateClient($"TimePad-{community.Id}"));
            var introspect = await timePadClient.IntrospectTokenAsync(timePadSection.Value, ct);

            var communityOrganization = introspect.Organizations?.SingleOrDefault(o => o.Name.Contains(community.Name));
            return communityOrganization;
        }
    }
}