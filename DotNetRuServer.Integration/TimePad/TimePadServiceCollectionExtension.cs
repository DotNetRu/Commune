using System;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetRuServer.Integration.TimePad
{
    public static class TimePadServiceCollectionExtension
    {
        private const string TimePadUri = "https://api.timepad.ru/";
        
        public static void AddTimePadIntegration(this IServiceCollection services)
        {
            services.AddTransient<TimePadIntegrationService>();
            services.AddHttpClient("TimePad", c =>
            {
                c.BaseAddress = new Uri(TimePadUri);
            });
        }
    }
}