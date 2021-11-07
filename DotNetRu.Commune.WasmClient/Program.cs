using System.Threading.Tasks;
using DotNetRu.Commune.WasmClient.Model;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Radzen;
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

        /// <summary>
        /// Конфигурация DI-контейнера
        /// </summary>
        /// <param name="services">Коллекция служб - собственно контейнер</param>
        /// <param name="configuration">Конфигурация</param>
        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // registering radzen blazor services for using notifications, dialogs, tooltips and custom context menus
            services
                .AddScoped<DialogService>()
                .AddScoped<NotificationService>()
                .AddScoped<TooltipService>()
                .AddScoped<ContextMenuService>();

            services.Configure<AuditSettings>(configuration.GetSection(nameof(AuditSettings)));
            services.AddBizLogic();
        }

        /// <summary>
        /// Настройка логирования.
        /// По умолчанию используется Serilog. Конфигурация логгера задаётся через IConfiguration, из секции "Serilog"
        /// </summary>
        /// <param name="builderLogging">Билдер логгера</param>
        /// <param name="builderConfiguration">Конфигурация</param>
        private static void ConfigureLogging(ILoggingBuilder builderLogging, IConfiguration builderConfiguration)
        {
            builderLogging.ClearProviders();
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builderConfiguration, "Serilog").CreateLogger();
            builderLogging.AddSerilog();
        }
    }
}
