using System;

namespace DotNetRu.MeetupManagement.Application.Contract.Exceptions
{
#pragma warning disable CA1032 // Implement standard exception constructors
    public abstract class ApplicationLayerException : Exception
#pragma warning restore CA1032 // Implement standard exception constructors
    {
        protected ApplicationLayerException(string message)
            : base(message)
        {
        }

        protected ApplicationLayerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
