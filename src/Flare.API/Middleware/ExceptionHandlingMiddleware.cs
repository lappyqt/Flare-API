using System.Text.Json;
using Flare.Application.Exceptions;
using Flare.Application.Models;

namespace Flare.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception.Message);

        HttpResponse response = context.Response;

        var code = StatusCodes.Status500InternalServerError;

        code = exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            ForbiddenException => StatusCodes.Status403Forbidden,
            UnprocessableEntityException => StatusCodes.Status422UnprocessableEntity,
            _ => code
        };

        response.ContentType = "application/json";
        context.Response.StatusCode = code;

        RequestErrorModel requestError = new RequestErrorModel
        {
            Message = exception.Message,
            StatusCode = response.StatusCode
        };

        string result = JsonSerializer.Serialize(requestError);
        await response.WriteAsJsonAsync(result);
    }
}