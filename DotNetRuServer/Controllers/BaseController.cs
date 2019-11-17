using System;
using System.Runtime.CompilerServices;
using DotNetRuServer.Controllers.Interfaces;
using DotNetRuServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetRuServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : Controller, IBaseController
    {
        private readonly IBaseService _service;
        private readonly ILogger _logger;

        protected BaseController() { }

        protected BaseController(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger(GetType());
        }

        public BaseController(IBaseService service,
            ILoggerFactory logger) : this(logger)
        {
            _service = service;
        }

        protected void LogMethodBegin(object arg = null, [CallerMemberName] string methodName = "")
        {
            _logger.LogDebug($"Call {methodName}. Argument: {arg}. RequestId: {ControllerContext.HttpContext.TraceIdentifier}.");
        }

        protected void LogMethodEnd(object result = null, [CallerMemberName] string methodName = "")
        {
            _logger.LogDebug($"Call {methodName}. Result: {result}. RequestId: {ControllerContext.HttpContext.TraceIdentifier}.");
        }

        protected void LogMethodError(Exception ex, [CallerMemberName] string methodName = "")
        {
            _logger.LogError(ex, $"Call {methodName}. RequestId: {ControllerContext.HttpContext.TraceIdentifier}. Exception: ");
        }
    }
}
