using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DotNetRu.MeetupManagement.WebApi.Filters
{
    public class InheritXmlCommentOperationFilter : IOperationFilter
    {
        private readonly XmlCommentsOperationFilter _filter;

        /// <inheritdoc />
        // ReSharper disable once UnusedMember.Global
        public InheritXmlCommentOperationFilter(XmlCommentsOperationFilter filter)
        {
            _filter = filter;
        }

        public void Apply(Operation operation, OperationFilterContext context)
        {
            //_filter.Apply(operation, context);
            if (context.ApiDescription.ActionDescriptor is ControllerActionDescriptor descr)
            {
                var prev = descr.MethodInfo;
                descr.MethodInfo = descr.MethodInfo.GetBaseDefinition();
                _filter.Apply(operation, context);
                descr.MethodInfo = prev;
            }


        }
    }
}
