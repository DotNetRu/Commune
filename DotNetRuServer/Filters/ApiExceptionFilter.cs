using System.Net;
using DotNetRuServer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace DotNetRuServer.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _environment;
        private readonly ILogger<ApiExceptionFilter> _logger;

        protected ApiExceptionFilter() { }

        public ApiExceptionFilter(IHostingEnvironment env, ILogger<ApiExceptionFilter> logger)
        {
            _environment = env;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, nameof(ApiExceptionFilter));

            var error = new ResponseErrorModel(context.Exception.Message, (int)HttpStatusCode.InternalServerError);
            if (_environment.IsDevelopment())
            {
                error.StackTrace = context.Exception.StackTrace;
            }

            context.HttpContext.Response.StatusCode = error.StatusCode;
            context.Result = new JsonResult(error);
        }
    }
}
