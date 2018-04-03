using System;
using System.IO;
using System.Xml.XPath;
using DotNetRu.MeetupManagement.WebApi.Contract.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DotNetRu.MeetupManagement.WebApi.Config
{
    public class Startup
    {
        public Startup()
        {
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .AddApiExplorer()
                .AddJsonOptions(contractStartup.ConfigureJsonOptions);
            services.AddSwaggerGen(options =>
            {
                contractStartup.ConfigureSwaggerGenOptions(options);
                IncludeXmlComments(options);
            });
        }

        private void IncludeXmlComments(SwaggerGenOptions options)
        {            
            var commentsPath = $"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{contractStartup.ContractXmlCommentsFileName}";
            //options.IncludeXmlComments($"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}DotNetRu.MeetupManagement.WebApi.Contract.xml");
            var comments = new XPathDocument(commentsPath);
            var xmlCommentsFilter = new XmlCommentsOperationFilter(comments);
            options.OperationFilter<InheritXmlCommentOperationFilter>(xmlCommentsFilter);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
            app.UseSwagger()
              .UseSwaggerUI(contractStartup.ConfigureSwaggerUIOptions);
        }

        private Contract.Startup contractStartup = new Contract.Startup();
    }
}
