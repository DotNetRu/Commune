namespace DevActivator.Meetups.BL.NewModels
{
    public class Friend
    {
        public int Id { get; set; }
        public string ExportId { get; set; }

        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }

        public string LogoUrl { get; set; }
        public string SmallLogoUrl { get; set; }
    }
}