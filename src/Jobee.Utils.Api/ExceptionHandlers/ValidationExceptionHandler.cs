using System.Diagnostics;
using Jobee.Utils.Api.Responses;
using Jobee.Utils.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Jobee.Utils.Api.ExceptionHandlers;

public class ValidationExceptionHandler : IExceptionHandler
{
    private readonly TimeProvider _timeProvider;

    public ValidationExceptionHandler(TimeProvider timeProvider) => _timeProvider = timeProvider;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ValidationException validationException)
        {
            return false;
        }

        var currentActivity = Activity.Current;
        var traceId = currentActivity?.TraceId.ToString() ?? httpContext.TraceIdentifier;
        var response = new ValidationErrorResponse(validationException.Errors)
        {
            Instance = httpContext.Request.Path,
            TraceId = traceId,
            Timestamp = _timeProvider.GetUtcNow(),
        };

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        
        return true;
    }
}