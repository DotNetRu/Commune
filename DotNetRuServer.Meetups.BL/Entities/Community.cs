using System.Collections.Generic;

namespace DotNetRuServer.Meetups.BL.Entities
{
    public class Community
    {
        public int Id { get; set; }
        public string ExportId { get; set; }

        public string Name { get; set; }
        public string City { get; set; }
        public string TimeZone { get; set; }

        public string Vk { get; set; }
        public string TelegramChannel { get; set; }
        public string TelegramChat { get; set; }
        public string TimePad { get; set; }

        public List<Meetup> Meetups { get; set; }
    }
}
