using Jobee.Utils.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Jobee.Utils.Api.Responses;

public class ConflictErrorResponse : ProblemDetails, IErrorResponse
{
    public ConflictErrorResponse()
    {
        Title = "An conflict occurred that prevents the request from being processed.";
        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
        Status = 409;
    }
    
    public required string TraceId { get; init; }

    public required DateTimeOffset Timestamp { get; init; }
    
    public string? Target { get; set; }
}