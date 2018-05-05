using System;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Autofac.Extensions.DependencyInjection;

namespace DotNetRu.ServiceHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration, ContainerBuilder containerBuilder)
        {
            Configuration = configuration ?? throw new System.ArgumentNullException(nameof(configuration));
            _containerBuilder = containerBuilder ?? throw new System.ArgumentNullException(nameof(containerBuilder));
            _meetupServiceStartup = new MeetupManagement.WebApi.Config.Startup();
        }

        public IConfiguration Configuration { get; }

        public IContainer Container { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            _meetupServiceStartup.ConfigureServices(services);
            services.AddMvcCore().AddControllersAsServices();
            _containerBuilder.Populate(services);
            return BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            _meetupServiceStartup.Configure(app);
        }

        private IServiceProvider BuildServiceProvider()
        {
            var container = _containerBuilder.Build();
            var result = new AutofacServiceProvider(container);
            Container = container;
            return result;
        }
        private MeetupManagement.WebApi.Config.Startup _meetupServiceStartup;
        private readonly ContainerBuilder _containerBuilder;
    }
}
