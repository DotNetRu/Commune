using System.Threading.Tasks;
using DotNetRu.Commune.WasmClient.Model;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace DotNetRu.Commune.WasmClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            ConfigureLogging(builder.Logging, builder.Configuration);

            ConfigureServices(builder.Services, builder.Configuration);

            await builder.Build().RunAsync();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CustomMessage>(configuration.GetSection(nameof(CustomMessage)));
            services.AddBizLogic();
        }

        /// <summary>
        /// Настройка логгирования.
        /// По умолчанию используется Serilog. Конфигурация логгера задаётся через IConfiguration, из секции "Serilog"
        /// </summary>
        /// <param name="builderLogging">Билдер логгера</param>
        /// <param name="builderConfiguration">Конфигурация</param>
        private static void SetupLogging(ILoggingBuilder builderLogging, IConfiguration builderConfiguration)
        {
            builderLogging.ClearProviders();
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builderConfiguration, "Serilog").CreateLogger();
            builderLogging.AddSerilog();
        }
    }
}
