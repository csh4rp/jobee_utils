using System.Diagnostics;
using Jobee.Utils.Api.Responses;
using Jobee.Utils.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Jobee.Utils.Api.ExceptionHandlers;

public class EntityNotFoundExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not EntityNotFoundException notFoundException)
        {
            return false;
        }

        var currentActivity = Activity.Current;
        var traceId = currentActivity?.TraceId.ToString() ?? httpContext.TraceIdentifier;
        var response = new NotFoundErrorResponse
        {
            Instance = httpContext.Request.Path,
            TraceId = traceId,
            Timestamp = TimeProvider.System.GetUtcNow(),
            EntityId = notFoundException.EntityId,
            EntityType = notFoundException.EntityType,
        };

        httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}