namespace DevActivator.Meetups.BL.NewModels
{
    public class SpeakerTalk
    {
        public int SpeakerId { get; set; }
        public Speaker Speaker { get; set; }

        public int TalkId { get; set; }
        public Talk Talk { get; set; }
    }
}