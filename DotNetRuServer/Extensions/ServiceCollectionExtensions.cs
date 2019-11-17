using DotNetRuServer.Application;
using DotNetRuServer.Comon.BL.Caching;
using DotNetRuServer.Meetups.BL.Interfaces;
using DotNetRuServer.Meetups.DAL.Providers;
using DotNetRuServer.Services;
using DotNetRuServer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetRuServer.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        internal static void AddInjection(this IServiceCollection services)
        {
            AddTransient(services);
            AddScoped(services);
            AddSingleton(services);
        }

        private static void AddTransient(IServiceCollection services)
        {
        }

        private static void AddScoped(IServiceCollection services)
        {

            services.AddScoped<IImporter, Application.Importer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBaseService, BaseService>();
        }

        private static void AddSingleton(IServiceCollection services)
        {
            services.AddSingleton<ICache, MemCache>();
        }
    }
}
