using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DotNetRuServer.Integration.TimePad.Dto;

namespace DotNetRuServer.Integration.TimePad
{
    public class TimePadIntegrationService
    {
        private readonly IHttpClientFactory _clientFactory;

        public TimePadIntegrationService(IHttpClientFactory factory)
        {
            _clientFactory = factory;
        }

        public async Task CreateDraftEventAsync(CreateDraftEventDto dto, CancellationToken ct)
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
            var createEventBody = new CreateEvent
            {
                Organization = new OrganizationInclude{Id = dto.OrganizationId, Subdomain = dto.OrganizationSubdomain},
                Starts_at = dto.StartsAt,
                Ends_at = dto.EndsAt,
                Name = dto.EventName,
                Description_short = dto.ShortDescription,
                Description_html = dto.HtmlDescription,
                Location = new LocationInclude {City = dto.City, Address = dto.Address},
                Categories = new[] {new CategoryInclude {Id = 452, Name = "ИТ и интернет"}},
                Access_status = "draft",
                Tickets_limit = dto.TicketsLimit,
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