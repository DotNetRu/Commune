using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DotNetRuServer.Comon.BL.Caching;
using DotNetRuServer.Comon.BL.Config;
using DotNetRuServer.Meetups.BL;
using DotNetRuServer.Meetups.BL.Interfaces;
using DotNetRuServer.Meetups.DAL.Database;
using DotNetRuServer.Meetups.DAL.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace DotNetRuServer
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _currentEnvironment;

        public Startup(IConfiguration configuration, IHostingEnvironment currentEnvironment)
        {
            _configuration = configuration;
            _currentEnvironment = currentEnvironment;
        }


        public IContainer ApplicationContainer { get; private set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)

        {
            if (_currentEnvironment.IsDevelopment())
                services.AddDbContext<DotNetRuServerContext>(options =>
                    options.UseInMemoryDatabase("DotNetRu"));
            else
                services.AddDbContext<DotNetRuServerContext>(options =>
                    options.UseSqlServer(_configuration.GetConnectionString("Database")));


            services.AddMemoryCache();

            services.AddMvc();


            //CORS
            services.AddCors(options =>
            {
                options.AddPolicy("Everyone",
                    new CorsPolicyBuilder()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin().Build());
            });


            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = "DotNetRuAPI", Version = "v1"}); });

            var builder = new ContainerBuilder();
            builder.Populate(services);

            builder.RegisterType<MemCache>().As<ICache>().SingleInstance();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();


            var settings = new Settings();
            _configuration.Bind(nameof(Settings), settings);

            builder.RegisterModule(
                new MeetupModule<SpeakerProvider, TalkProvider, VenueProvider, FriendProvider, MeetupProvider,
                    CommunityProvider>(
                    settings));

            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("Everyone");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DotNetRuAPI V1");
                c.RoutePrefix = string.Empty;
            });


            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

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