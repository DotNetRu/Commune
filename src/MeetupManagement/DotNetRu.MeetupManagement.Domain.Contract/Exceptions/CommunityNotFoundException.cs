namespace DotNetRu.MeetupManagement.Domain.Contract.Exceptions
{
#pragma warning disable CA1032 // Implement standard exception constructors
    public class CommunityNotFoundException : EntityNotFoundException
#pragma warning restore CA1032 // Implement standard exception constructors
    {
        public CommunityNotFoundException(string communityId, string message)
            : base(message)
        {
            CommunityId = communityId;
        }

        public CommunityNotFoundException(string communityId, string message, System.Exception innerException)
            : base(message, innerException)
        {
            CommunityId = communityId;
        }

        public string CommunityId { get; }
    }
}
