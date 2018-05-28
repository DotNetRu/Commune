namespace DotNetRu.MeetupManagement.Core.Drafts
{
    public class TalkTryout
    {
        public long Id { get; set; }
        public long DraftTalkId { get; set; }
        public string Comment { get; set; }
        //public byte[] Artifacts { get; set; }
    }
}