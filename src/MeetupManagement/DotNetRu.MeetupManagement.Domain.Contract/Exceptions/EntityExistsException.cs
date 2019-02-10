using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetRu.MeetupManagement.Domain.Contract.Exceptions
{
    public class EntityExistsException : DomainException
    {
        public EntityExistsException()
        {
        }

        public EntityExistsException(string message)
            : base(message)
        {
        }

        public EntityExistsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
