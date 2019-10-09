using System;

namespace DotNetRuServer.Integration.TimePad.Dto
{
    public class CreateDraftEventDto
    {
        public int OrganizationId { get; set; }
        public string OrganizationSubdomain { get; set; }
        public string EventName { get; set; }
        public string ShortDescription { get; set; }
        public string HtmlDescription { get; set; }
        public DateTimeOffset StartsAt { get; set; }
        public DateTimeOffset EndsAt { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int TicketsLimit { get; set; }
    }
}