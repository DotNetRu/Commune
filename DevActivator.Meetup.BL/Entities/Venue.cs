using DevActivator.Common.BL.Config;

namespace DevActivator.Meetup.BL.Entities
{
    public class Venue : IFlatEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string MapUrl { get; set; }
    }
}