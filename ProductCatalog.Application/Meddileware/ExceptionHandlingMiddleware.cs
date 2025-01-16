using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace ProductCatalog.Application.Meddileware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
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

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        Console.WriteLine($"Error: {exception.Message}");

        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync(new
        {
            StatusCode = context.Response.StatusCode,
            Message = "An internal server error occurred. Please try again later."
        }.ToString());
    }
}

