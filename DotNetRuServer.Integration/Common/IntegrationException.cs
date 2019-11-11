using System;

namespace DotNetRuServer.Integration.Common
{
    public class IntegrationException : Exception
    {
        public string IntegrationService { get; }

        public IntegrationException(string integrationServiceName, string message) : base(message)
        {
            IntegrationService = integrationServiceName;
        }
    }
}