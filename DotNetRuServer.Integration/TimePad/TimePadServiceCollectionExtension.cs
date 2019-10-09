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
            var accessToken = configuration.GetSection("TimePad")["AccessToken"];
            services.AddHttpClient("TimePad", c =>
            {
                c.BaseAddress = new Uri("https://api.timepad.ru/");
                c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            });
            services.AddTransient<TimePadIntegrationService>();
        }
    }
}