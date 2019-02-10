using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace DotNetRu.MeetupManagement.WebApi
{
    internal static class ControllerExtensions
    {
        public static ActionResult<T> GetCreatedActionResult<T>(this ControllerBase controller, string actionName, object routeValues, T value)
        {
            return controller.CreatedAtAction(actionName, routeValues, value);
        }

        public static ActionResult<T> GetActionCustomResult<T>(T value, HttpStatusCode statusCode)
        {
            return new ObjectResult(value)
            {
                StatusCode = (int)statusCode
            };
        }
    }
}
