using System.Diagnostics;
using Jobee.Utils.Api.Responses;
using Jobee.Utils.Contracts;
using Jobee.Utils.Contracts.Responses;

namespace Jobee.Utils.Api.ApiResults;

public class ConflictResult : IResult
{
    private readonly string _reason;

    public ConflictResult(string reason) => _reason = reason;

    public Task ExecuteAsync(HttpContext httpContext)
    {
        var currentActivity = Activity.Current;
        var traceId = currentActivity?.TraceId.ToString() ?? httpContext.TraceIdentifier;

        var response = new ConflictErrorResponse
        {
            Instance = httpContext.Request.Path,
            TraceId = traceId,
            Timestamp = TimeProvider.System.GetUtcNow(),
            Detail = _reason
        };

        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        return httpContext.Response.WriteAsJsonAsync(response);
    }
}