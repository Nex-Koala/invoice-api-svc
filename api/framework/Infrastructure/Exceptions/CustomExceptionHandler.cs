using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using NexKoala.Framework.Core.Exceptions;
using NexKoala.Framework.Core.Wrappers;

namespace NexKoala.Framework.Infrastructure.Exceptions;
public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(httpContext);
        ArgumentNullException.ThrowIfNull(exception);

        // default fallback
        int statusCode = StatusCodes.Status500InternalServerError;
        string message = "An unexpected error occurred.";
        List<string> errors = new();
        bool succeeded = false;

        // handle known exception types
        if (exception is FluentValidation.ValidationException fluentException)
        {
            statusCode = StatusCodes.Status400BadRequest;
            message = "One or more validation errors occurred.";
            errors = fluentException.Errors.Select(e => e.ErrorMessage).ToList();
        }
        else if (exception is GenericException e)
        {
            statusCode = e.StatusCode == 0 ? StatusCodes.Status500InternalServerError : (int)e.StatusCode;
            message = e.Message;
            if (e.ErrorMessages != null && e.ErrorMessages.Any())
                errors = e.ErrorMessages.ToList();
        }
        else
        {
            message = exception.Message;
        }

        LogContext.PushProperty("StackTrace", exception.StackTrace);
        logger.LogError(exception, "Exception handled: {Message}", message);

        var response = new Response<object>
        {
            Succeeded = succeeded,
            Message = message,
            Errors = errors,
            Data = null
        };

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken).ConfigureAwait(false);
        return true;
    }
}
