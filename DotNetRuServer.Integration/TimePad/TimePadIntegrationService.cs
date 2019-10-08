using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace DotNetRuServer.Integration.TimePad
{
    public class TimePadIntegrationService
    {
        private const string TimePadApiUri = "https://api.timepad.ru/";
        
        private readonly TimePadClient _timePadClient;
        
        public TimePadIntegrationService(IConfiguration configuration, IHttpClientFactory factory)
        {
            _timePadClient = new TimePadClient(TimePadApiUri, factory.CreateClient());
        }
    }
}