using DevActivator.Common.BL.Config;

namespace DevActivator.Meetup.BL.Entities
{
    public class Friend : IEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }
    }
}