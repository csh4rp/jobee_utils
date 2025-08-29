using System.Diagnostics;
using Jobee.Utils.Api.Responses;
using Jobee.Utils.Contracts;
using Jobee.Utils.Contracts.Responses;

namespace Jobee.Utils.Api.ApiResults;

public class NotFoundResult : IResult
{
    private readonly string _entityType;
    private readonly string _entityId;

    public NotFoundResult(string entityType, string entityId)
    {
        _entityType = entityType;
        _entityId = entityId;
    }

    public Task ExecuteAsync(HttpContext httpContext)
    {
        var currentActivity = Activity.Current;
        var traceId = currentActivity?.TraceId.ToString() ?? httpContext.TraceIdentifier;

        var response = new NotFoundErrorResponse
        {
            Instance = httpContext.Request.Path,
            Detail = "An entity with the specified identifier was not found.",
            TraceId = traceId,
            Timestamp = TimeProvider.System.GetUtcNow(),
            EntityType = _entityType,
            EntityId = _entityId
        };

        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        return httpContext.Response.WriteAsJsonAsync(response);
    }
}