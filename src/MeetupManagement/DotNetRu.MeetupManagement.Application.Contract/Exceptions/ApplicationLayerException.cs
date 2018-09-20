using System;

namespace DotNetRu.MeetupManagement.Application.Contract.Exceptions
{
    public abstract class ApplicationLayerException : Exception 
    {
        protected ApplicationLayerException(string message) : base(message) 
        {
        }
        protected ApplicationLayerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
