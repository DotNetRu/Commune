using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DotNetRu.MeetupManagement.WebApi.Filters
{
    public class InheritXmlCommentOperationFilter : IOperationFilter
    {
        private readonly XmlCommentsOperationFilter _filter;

        // ReSharper disable once UnusedMember.Global
        public InheritXmlCommentOperationFilter(XmlCommentsOperationFilter filter)
        {
            _filter = filter;
        }

        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.ActionDescriptor is ControllerActionDescriptor actionDescriptor)
            {
                var prev = actionDescriptor.MethodInfo;
                actionDescriptor.MethodInfo = actionDescriptor.MethodInfo.GetBaseDefinition();
                _filter.Apply(operation, new OperationFilterContext(context.ApiDescription, context.SchemaRegistry, actionDescriptor.MethodInfo));
                actionDescriptor.MethodInfo = prev;
            }
        }
    }
}
