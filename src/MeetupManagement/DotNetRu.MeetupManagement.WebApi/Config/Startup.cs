using System;
using System.IO;
using System.Xml.XPath;
using DotNetRu.MeetupManagement.WebApi.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DotNetRu.MeetupManagement.WebApi.Config
{
    public class Startup
    {
        private readonly Contract.Startup _contractStartup = new Contract.Startup();

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .AddApiExplorer()
                .AddJsonOptions(_contractStartup.ConfigureJsonOptions);
            services.AddSwaggerGen(options =>
            {
                _contractStartup.ConfigureSwaggerGenOptions(options);
                IncludeXmlComments(options);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
            app.UseSwagger()
                .UseSwaggerUI(_contractStartup.ConfigureSwaggerUiOptions);
        }

        private void IncludeXmlComments(SwaggerGenOptions options)
        {
            var commentsPath = $"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{_contractStartup.ContractXmlCommentsFileName}";

            // options.IncludeXmlComments($"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}DotNetRu.MeetupManagement.WebApi.Contract.xml");
            var comments = new XPathDocument(commentsPath);
            var xmlCommentsFilter = new XmlCommentsOperationFilter(comments);
            options.OperationFilter<InheritXmlCommentOperationFilter>(xmlCommentsFilter);
        }
    }
}
