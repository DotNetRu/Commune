namespace DevActivator.Meetups.BL.Entities
{
    public class SeeAlsoTalk
    {
        public int ParentTalkId { get; set; }
        public Talk ParentTalk { get; set; }
        public int ChildTalkId { get; set; }
        public Talk ChildTalk { get; set; }
    }
}