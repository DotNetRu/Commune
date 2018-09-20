
namespace DotNetRu.MeetupManagement.Domain.Contract.Exceptions
{
    public class CommunityNotFoundException: EntityNotFoundException
    {
        public CommunityNotFoundException(string communityId, string message): base(message)
        {
            CommunityId = communityId;
        }
        public string CommunityId { get; }
    }
}
