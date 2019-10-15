using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetRuServer.Integration.TimePad
{
    public static class TimePadServiceCollectionExtension
    {
        private const string TimePadSection = "TimePad";
        private const string TimePadUri = "https://api.timepad.ru/";
        private const string CommunitySectionTemplate = @"Community-(\d*)";
        private static readonly Regex CommunitySectionRegex = new Regex(CommunitySectionTemplate);
        
        public static void AddTimePadIntegration(this IServiceCollection services, IConfiguration configuration)
        {
            foreach (var configurationSection in configuration.GetChildren().Where(c => CommunitySectionRegex.IsMatch(c.Key)))
            {
                var timePadSection = configurationSection.GetSection(TimePadSection);
                if (timePadSection is null)
                    continue;
                
                var communityId = CommunitySectionRegex.Match(configurationSection.Key).Groups[1];
                services.AddHttpClient($"TimePad-{communityId}", c =>
                {
                    c.BaseAddress = new Uri(TimePadUri);
                    c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", timePadSection.Value);
                });
            }
            
            services.AddTransient<TimePadIntegrationService>();
        }
    }
}