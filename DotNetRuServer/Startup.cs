using System;
using System.Threading.Tasks;
using DotNetRuServer.Application;
using DotNetRuServer.Extensions;
using DotNetRuServer.Integration.TimePad;
using DotNetRuServer.Meetups.BL;
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

            services.AddInjection();
            services.AddMeetups<SpeakerProvider, TalkProvider, VenueProvider, FriendProvider, MeetupProvider,CommunityProvider, ImageProvider>(_configuration);
            services.AddTimePadIntegration();
            
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

            if (_currentEnvironment.IsDevelopment())
            {
                Task.Run(async () =>
                    {
                        var serviceProvider = app.ApplicationServices;
                        using (var scope = serviceProvider.CreateScope())
                        {
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
                    }
                );
            }
        }
    }
}