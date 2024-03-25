using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex}");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "MyCustomMessage";

            context.Response.Headers.Add("X-Status-Code", "500 Internal Server Error");

            string errorMessage = "An unexpected error occurred";

            var response = new { message = errorMessage };

            var jsonResponse = JsonConvert.SerializeObject(response);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}

public static class ErrorHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}
