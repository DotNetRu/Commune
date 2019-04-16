using DevActivator.Common.BL.Enums;

namespace DevActivator.Meetups.BL.Models
{
    public class VenueVm
    {
        public string Id { get; set; }

        public City City { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string MapUrl { get; set; }
    }
}