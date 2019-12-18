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
        private const string _logError = "Internal server error occurred";
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly bool _isDevelopment;

        public ErrorHandlingMiddleware( RequestDelegate next, 
                                        ILogger<ErrorHandlingMiddleware> logger,
                                        IHostingEnvironment env)
        {
            _next = next;
            _logger = logger;
            _isDevelopment = env.IsDevelopment();
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
            _logger.LogError(exception, _logError);
            if (exception is AggregateException aex && aex.InnerExceptions?.Count > 0)
            {
                foreach (Exception ex in aex.InnerExceptions)
                    _logger.LogError(ex, _logError);
            }

            var error = new ResponseErrorModel(exception.Message, (int)HttpStatusCode.InternalServerError);
            if (_isDevelopment) error.StackTrace = exception.StackTrace;

            context.Response.StatusCode = error.StatusCode;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(error));
        }
    }
}
