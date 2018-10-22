using System;
using System.IO;
using Autofac;
using DotNetRu.MeetupManagement.Infrastructure.DependencyInjection;
using DotNetRu.ServiceHost.Autofac;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DotNetRu.ServiceHost
{
    public static class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new DataLayerModule());
            try
            {
                Log.Information("Getting the motors running...");

                var webHost = BuildWebHost(args, containerBuilder);
                try
                {
                    webHost.Item1.Run();
                }
                finally
                {
                    webHost.Item2.Dispose();
                }

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

#pragma warning disable SA1008 // Opening parenthesis must be spaced correctly
        public static (IWebHost, IContainer) BuildWebHost(string[] args, ContainerBuilder containerBuilder)
#pragma warning restore SA1008 // Opening parenthesis must be spaced correctly
        {
            var startup = new Startup(Configuration, containerBuilder);
            var webHost = WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseConfiguration(Configuration)
                .ConfigureServices(services => services.AddSingleton(typeof(Startup), startup))
                .UseSerilog()
                .Build();
            return (webHost, startup.Container);
            /*return new WebHostBuilder()
                            .UseKestrel(options =>
                            {
                            })
                            .UseUrls("http://+:49212")
                            .UseContentRoot(Directory.GetCurrentDirectory())
                            .UseIISIntegration()+
                            .UseConfiguration(Configuration)
                            .UseSerilog()
                            .UseStartup<Startup>()
                            .Build();*/
        }
    }
}
