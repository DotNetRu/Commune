using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DotNetRuServer.Meetups.BL.Entities;
using RazorLight;

namespace DotNetRuServer.Integration.TimePad
{
    public class TimePadIntegrationService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly RazorLightEngine _razorEngine;

        public TimePadIntegrationService(IHttpClientFactory factory)
        {
            _clientFactory = factory;
            var path = Directory.GetCurrentDirectory();
            _razorEngine = new RazorLightEngineBuilder()
                .UseFilesystemProject(path)
                .UseMemoryCachingProvider()
                .Build();
        }

        public async Task CreateDraftEventAsync(Meetup meetup, CancellationToken ct)
        {
            var timePadClient = new TimePadClient(_clientFactory.CreateClient("TimePad"));
            var questions = new List<QuestionInclude>
            {
                new QuestionInclude{Field_id = "mail", Is_mandatory = true, Name = "E-mail"},
                new QuestionInclude{Field_id = "surname", Is_mandatory = true, Name = "Фамилия"},
                new QuestionInclude{Field_id = "name", Is_mandatory = true, Name = "Имя"},
                new QuestionInclude{Field_id = "company", Is_mandatory = false, Name = "Компания"},
                new QuestionInclude{Field_id = "position", Is_mandatory = false, Name = "Должность"}
            };
            var htmlDescription = await _razorEngine.CompileRenderAsync("TimePad/EventTemplate.cshtml", new { Name = meetup.Name });
            
            var createEventBody = new CreateEvent
            {
                Organization = new OrganizationInclude{Id = null, Subdomain = null}, //todo: fill
                Starts_at = DateTimeOffset.MinValue, //todo:fill
                Ends_at = DateTimeOffset.MinValue, //todo:fill
                Name = meetup.Name,
                Description_short = null, //todo: fill
                Description_html = htmlDescription,
                Location = new LocationInclude {City = null, Address = meetup.Venue.Address}, //todo: fill
                Categories = new[] {new CategoryInclude {Id = 452, Name = "ИТ и интернет"}},
                Access_status = "draft",
                Tickets_limit = null, //todo: fill
                Questions = questions
            };
            
            await timePadClient.AddEventAsync(createEventBody, ct);
        }

        public void PublishEvent()
        {
            throw new NotImplementedException();
        }
    }
}