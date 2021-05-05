using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
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

            SetupLogging(builder.Logging, builder.Configuration);

            builder.Services.AddBizLogic();

            await builder.Build().RunAsync();
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
