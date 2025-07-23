using System.Net;
using System.Text.Json;
using Netwise.Exceptions;

namespace Netwise.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }



    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context); 
        }
        catch (ExternalApiException ex)
        {
            _logger.LogWarning(ex, "External API exception occurred");
            await HandleExceptionAsync(context, ex.Message, HttpStatusCode.InternalServerError);
        }
    }


    private static async Task HandleExceptionAsync(HttpContext context, string message, HttpStatusCode statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            error = message
        };

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }

}