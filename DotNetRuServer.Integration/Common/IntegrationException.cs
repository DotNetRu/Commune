using System;

namespace DotNetRuServer.Integration.Common
{
    public class IntegrationException : Exception
    {
        public IntegrationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}