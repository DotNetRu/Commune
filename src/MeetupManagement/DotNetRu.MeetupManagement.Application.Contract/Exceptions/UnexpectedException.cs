using System;

namespace DotNetRu.MeetupManagement.Application.Contract.Exceptions
{
    public class UnexpectedException: ApplicationLayerException
    {
        public UnexpectedException(string message) : base(message)
        {
        }

        public UnexpectedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
