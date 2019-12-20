using System.Net;
using DotNetRuServer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DotNetRuServer.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        private readonly bool _isDevelopment;
        private readonly ILogger<ApiExceptionFilter> _logger;

        protected ApiExceptionFilter() { }

        public ApiExceptionFilter(IWebHostEnvironment env, ILogger<ApiExceptionFilter> logger)
        {
            _isDevelopment = env.IsDevelopment();
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Exception filter called");

            var error = new ResponseErrorModel(context.Exception.Message, (int)HttpStatusCode.InternalServerError);
            if (_isDevelopment) error.StackTrace = context.Exception.StackTrace;

            context.HttpContext.Response.StatusCode = error.StatusCode;
            context.Result = new JsonResult(error);
        }
    }
}
