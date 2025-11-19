using System.Net;
using PersonAPI.DTOs;
using PersonAPI.Exceptions;

namespace PersonAPI.Middleware;

// Like @ControllerAdvice in Spring Boot
public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = new ErrorResponse
        {
            Path = context.Request.Path
        };

        switch (exception)
        {
            case ResourceNotFoundException:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Message = exception.Message;
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                break;

            case DuplicateResourceException:
                response.StatusCode = (int)HttpStatusCode.Conflict;
                response.Message = exception.Message;
                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                break;

            case ValidationException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = exception.Message;
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;

            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = "An internal server error occurred";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        context.Response.ContentType = "application/json";
        return context.Response.WriteAsJsonAsync(response);
    }
}
