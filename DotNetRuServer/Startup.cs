using System;
using System.Threading.Tasks;
using DotNetRuServer.Application;
using DotNetRuServer.Comon.BL.Caching;
using DotNetRuServer.Filters;
using DotNetRuServer.Integration.TimePad;
using DotNetRuServer.Meetups.BL;
using DotNetRuServer.Meetups.BL.Interfaces;
using DotNetRuServer.Meetups.DAL.Database;
using DotNetRuServer.Meetups.DAL.Providers;
using DotNetRuServer.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DotNetRuServer
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _currentEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment currentEnvironment)
        {
            _configuration = configuration;
            _currentEnvironment = currentEnvironment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (_currentEnvironment.IsDevelopment())
            {
                services.AddDbContext<DotNetRuServerContext>(options =>
                    options.UseInMemoryDatabase("DotNetRu"));
            }
            else
            {
                services.AddDbContext<DotNetRuServerContext>(options =>
                    options.UseSqlServer(_configuration.GetConnectionString("Database")));
            }

            services.AddMemoryCache();

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ApiExceptionFilter));
            });

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
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "DotNetRuAPI", Version = "v1"}); });

            services.AddSingleton<ICache, MemCache>();
            services.AddScoped<IImporter, Application.Importer>();
            services.AddScoped<IExporter, Application.Exporter>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddMeetups<SpeakerProvider, TalkProvider, VenueProvider, FriendProvider, MeetupProvider,CommunityProvider, ImageProvider>(_configuration);
            services.AddTimePadIntegration();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("Everyone");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DotNetRuAPI V1");
                c.RoutePrefix = string.Empty;
            });


            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseStaticFiles();

            // app.UseMvc(routes =>
            // {
            //     routes.MapRoute(
            //         "default",
            //         "{controller=Home}/{action=Index}/{id?}");
            // });

            if (_currentEnvironment.IsDevelopment())
            {
                Task.Run(async () =>
                    {
                        var serviceProvider = app.ApplicationServices;
                        using var scope = serviceProvider.CreateScope();
                        var importer = scope.ServiceProvider.GetService<IImporter>();
                        var token = Environment.GetEnvironmentVariable("GITHUB_TOKEN");
                        if (string.IsNullOrEmpty(token))
                            return;

                        try
                        {
                            await importer.Import(token);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
                    }
                );
            }
        }
    }
}
