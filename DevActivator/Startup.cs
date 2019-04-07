using System;
using System.IO;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DevActivator.Common.BL.Caching;
using DevActivator.Common.BL.Config;
using DevActivator.Meetups.BL;
using DevActivator.Meetups.DAL.Database;
using DevActivator.Meetups.DAL.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Swashbuckle.AspNetCore.Swagger;

namespace DevActivator
{
    public class Startup
    {
        private string _staticVirtualFolderPath;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            StaticVirtualFolderPath = Configuration[$"{nameof(Settings)}:{nameof(Settings.AuditRepoDirectory)}"];
        }

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
            services.AddDbContext<DotNetRuServerContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Database")));
            
            services.AddMemoryCache();

            services.AddMvc();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = "DotNetRuAPI", Version = "v1"}); });

            var builder = new ContainerBuilder();
            builder.Populate(services);

            builder.RegisterType<MemCache>().As<ICache>().SingleInstance();


            var settings = new Settings();
            Configuration.Bind(nameof(Settings), settings);

            builder.RegisterModule(
                new MeetupModule<SpeakerProvider, TalkProvider, VenueProvider, FriendProvider, MeetupProvider, CommunityProvider>(
                    settings));

            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DotNetRuAPI V1");
                c.RoutePrefix = string.Empty;
            });


            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

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
            });
        }
    }
}