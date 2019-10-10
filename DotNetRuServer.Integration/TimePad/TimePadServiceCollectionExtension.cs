using System;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetRuServer.Integration.TimePad
{
    public static class TimePadServiceCollectionExtension
    {
        public static void AddTimePadIntegration(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("TimePad");
            foreach (var child in section.GetChildren())
            {
                services.AddHttpClient($"{child.Key}-TimePad", c =>
                {
                    c.BaseAddress = new Uri("https://api.timepad.ru/");
                    c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", child.Value);
                });
            }
            services.AddTransient<TimePadIntegrationService>();
        }
    }
}