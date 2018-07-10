using System;

namespace DotNetRu.MeetupManagement.Domain.Contract.Exceptions
{
    public class EntityNotFoundException: DomainException
    {
        public EntityNotFoundException(string message) : base(message)
        {
        }

        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
