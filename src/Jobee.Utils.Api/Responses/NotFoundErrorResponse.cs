using Microsoft.AspNetCore.Mvc;

namespace Jobee.Utils.Api.Responses;

public class NotFoundErrorResponse : ProblemDetails, IErrorResponse
{
    public NotFoundErrorResponse()
    {
        Title = "Not Found";
        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
        Status = 404;
    }

    public required string TraceId { get; init; }

    public required DateTimeOffset Timestamp { get; init; }
    
    public required string EntityType { get; init; }
    
    public required string EntityId { get; init; }
}