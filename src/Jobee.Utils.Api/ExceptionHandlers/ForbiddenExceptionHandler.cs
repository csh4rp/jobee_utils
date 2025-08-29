using System.Diagnostics;
using Jobee.Utils.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Jobee.Utils.Api.ExceptionHandlers;

public class ForbiddenExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not ForbiddenException)
        {
            return false;
        }

        var currentActivity = Activity.Current;
        var traceId = currentActivity?.TraceId.ToString() ?? httpContext.TraceIdentifier;
        var response = new ProblemDetails
        {
            Instance = httpContext.Request.Path,
            Detail = "Access to the resource is denied",
            Title = "Access to the resource is denied",
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
            Status = StatusCodes.Status403Forbidden,
            Extensions = new Dictionary<string, object?>
            {
                { "traceId", traceId }, { "timestamp", TimeProvider.System.GetUtcNow() }
            }
        };

        httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
        return true;
    }
}