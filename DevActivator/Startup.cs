using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DevActivator.Common.BL.Caching;
using DevActivator.Common.BL.Config;
using DevActivator.Meetups.BL;
using DevActivator.Meetups.DAL.Providers;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace DevActivator
{
    public class Startup
    {
        private static BrowserWindow _browserWindow;

        private static PhysicalFileProvider _fileProvider;
        private static Timer _ticker;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            StaticVirtualFolderPath = Configuration[$"{nameof(Settings)}:{nameof(Settings.AuditRepoDirectory)}"];
        }

        private string _staticVirtualFolderPath;

        private string StaticVirtualFolderPath
        {
            get => _staticVirtualFolderPath;
            set => _staticVirtualFolderPath = Directory.Exists(value)
                ? value
                : throw new DirectoryNotFoundException(value);
        }

        public IContainer ApplicationContainer { get; private set; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddMvc();

            var builder = new ContainerBuilder();
            builder.Populate(services);

            builder.RegisterType<MemCache>().As<ICache>().SingleInstance();


            var settings = new Settings();
            Configuration.Bind(nameof(Settings), settings);

            builder.RegisterModule(new MeetupModule<SpeakerProvider, TalkProvider, VenueProvider, FriendProvider, MeetupProvider>(settings));

            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                if (HybridSupport.IsElectronActive)
                {
                    //the below is a hacky way to get hot module loading working for the ElectronNet app.
                    _fileProvider = new PhysicalFileProvider(env.WebRootPath);
                    _ticker = new Timer(TimerMethod, null, 1000, 1000);
                }
                else
                {
                    app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                    {
                        HotModuleReplacement = true
                    });
                }
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(StaticVirtualFolderPath),
                RequestPath = new PathString("/static"),
                EnableDirectoryBrowsing = false
            });

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    "spa-fallback",
                    new {controller = "Home", action = "Index"});
            });

            if (HybridSupport.IsElectronActive)
                ElectronBootstrap();
        }


        private async void ElectronBootstrap()
        {
            _browserWindow = await Electron.WindowManager.CreateWindowAsync(new BrowserWindowOptions
            {
                Show = false
            }).ConfigureAwait(false);

            _browserWindow.OnReadyToShow += () => _browserWindow.Show();
        }

        private static void TimerMethod(object state)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            var token = _fileProvider.Watch("**/*");
            var source = new TaskCompletionSource<object>();
            token.RegisterChangeCallback(state =>
                ((TaskCompletionSource<object>) state).TrySetResult(null), source);
            await source.Task.ConfigureAwait(false);
            _browserWindow.Reload();
        }
    }
}