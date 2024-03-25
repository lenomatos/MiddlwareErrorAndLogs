using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Serilog;
using Serilog.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

public class CorrelationIdMiddleWare
{
    private readonly RequestDelegate _next;

    public CorrelationIdMiddleWare(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        await _next(context);
        //var endpoint = context.GetEndpoint();
        //var controllerName = endpoint?.Metadata?.GetMetadata<ControllerActionDescriptor>()?.ControllerName;
        //var actionName = endpoint?.Metadata?.GetMetadata<ControllerActionDescriptor>()?.ActionName;

        //context.Request.Headers.TryGetValue("CorrelationId", out StringValues correlationIds);
        //var CorrelationId = correlationIds.FirstOrDefault() ?? Guid.NewGuid().ToString();


        //var controller = context.GetRouteValue("controller")?.ToString();
        //var action = context.GetRouteValue("action")?.ToString();

        ////var diagnosticContext =
        ////        context.RequestServices.GetService<IDiagnosticContext>();

        ////diagnosticContext.Set("Action", context.Request.Path);

        //if (controllerName != null && actionName != null)
        //{
        //    using (LogContext.PushProperty("Controller", controllerName))
        //    using (LogContext.PushProperty("Action", actionName))
        //    //using (LogContext.PushProperty("CorrelationId", CorrelationId))
        //    {
        //        await _next(context);
        //    }
        //}
        //else if (controller!= null && action != null)
        //{
        //    using (LogContext.PushProperty("Controller", controller))
        //    using (LogContext.PushProperty("Action", action))
        //    //using (LogContext.PushProperty("CorrelationId", CorrelationId))
        //    {
        //        await _next(context);
        //    }
        //}
        //else
        //{
        //    //using (LogContext.PushProperty("CorrelationId", CorrelationId))
        //    //{
        //    //    await _next(context);
        //    //}
        //    await _next(context);
        //}

    }
}

public static class CorrelationIdMiddleWareExtensions
{
    public static IApplicationBuilder CorrelationIdMiddleWare(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CorrelationIdMiddleWare>();
    }
}