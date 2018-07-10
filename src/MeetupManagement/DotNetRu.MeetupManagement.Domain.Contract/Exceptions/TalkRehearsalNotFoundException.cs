
namespace DotNetRu.MeetupManagement.Domain.Contract.Exceptions
{
    public class TalkRehearsalNotFoundException :  EntityNotFoundException
    {
        public TalkRehearsalNotFoundException(string rehearsalId, string message) : base(message)
        {
        }

        public string Id { get; }
    }
}
