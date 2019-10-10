using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DotNetRuServer.Integration.Common;
using DotNetRuServer.Meetups.BL.Entities;
using Microsoft.Extensions.Configuration;
using RazorLight;

namespace DotNetRuServer.Integration.TimePad
{
    public class TimePadIntegrationService
    {
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

        public async Task CreateDraftEventAsync(Meetup meetup, string shortDescription, int ticketsLimit,
            CancellationToken ct)
        {
            var communityCity = meetup.Venue.City.ToString();
            if (_configuration.GetSection("TimePad").GetChildren().All(c => c.Key != communityCity))
            {
                return;
            }
            
            var timePadClient = new TimePadClient(_clientFactory.CreateClient($"{communityCity}-TimePad"));

            var meetupStartsAt = meetup.Sessions.Min(s => s.StartTime);
            var meetupEndsAt = meetup.Sessions.Max(s => s.EndTime);
            var htmlDescription = await _razorEngine.CompileRenderAsync(
                $"TimePad/Templates/{communityCity}-template.cshtml",
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
                Organization = new OrganizationInclude {Id = null, Subdomain = null}, //todo: fill
                Starts_at = meetupStartsAt,
                Ends_at = meetupEndsAt,
                Name = meetup.Name,
                Description_short = shortDescription,
                Description_html = htmlDescription,
                Location = new LocationInclude {City = meetup.Venue.GetCityTitle(), Address = meetup.Venue.Address},
                Categories = new[] {new CategoryInclude {Id = 452, Name = "ИТ и интернет"}},
                Access_status = "draft",
                Tickets_limit = ticketsLimit,
                Questions = questions
            };

            try
            {
                await timePadClient.AddEventAsync(createEventBody, ct);
            }
            catch (Exception e)
            {
                throw new IntegrationException("Невозможно создать событие на TimePad", e);
            }
        }

        public void PublishEvent()
        {
            throw new NotImplementedException();
        }
    }
}