using System;

namespace DotNetRu.MeetupManagement.Application.Contract.Exceptions
{
#pragma warning disable CA1032 // Implement standard exception constructors
    public class UnexpectedException : ApplicationLayerException
#pragma warning restore CA1032 // Implement standard exception constructors
    {
        public UnexpectedException(string message)
            : base(message)
        {
        }

        public UnexpectedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
