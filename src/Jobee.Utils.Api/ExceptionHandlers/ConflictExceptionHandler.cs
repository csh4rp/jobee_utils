using System.Diagnostics;
using Jobee.Utils.Api.Responses;
using Jobee.Utils.Application.Exceptions;
using Jobee.Utils.Contracts;
using Microsoft.AspNetCore.Diagnostics;

namespace Jobee.Utils.Api.ExceptionHandlers;

internal sealed class ConflictExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not ConflictException conflictException)
        {
            return false;
        }

        var currentActivity = Activity.Current;
        var traceId = currentActivity?.TraceId.ToString() ?? httpContext.TraceIdentifier;
        var response = new ConflictErrorResponse
        {
            Instance = httpContext.Request.Path,
            TraceId = traceId,
            Timestamp = TimeProvider.System.GetUtcNow(),
            Target = conflictException.Target,
            Detail = conflictException.Message,
        };

        httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}