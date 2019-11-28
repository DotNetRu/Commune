using System;
using System.Net;
using System.Threading.Tasks;
using DotNetRuServer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DotNetRuServer.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly IHostingEnvironment _env;

        public ErrorHandlingMiddleware( RequestDelegate next, 
                                        ILogger<ErrorHandlingMiddleware> logger,
                                        IHostingEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception is AggregateException aex && aex.InnerExceptions?.Count > 0)
                exception = aex.InnerExceptions[0];

            _logger.LogError(exception, nameof(ErrorHandlingMiddleware));

            var error = new ResponseErrorModel(exception.Message, (int)HttpStatusCode.InternalServerError);
            if (_env.IsDevelopment())
            {
                error.StackTrace = exception.StackTrace;
            }

            context.Response.StatusCode = error.StatusCode;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(error));
        }
    }
}
