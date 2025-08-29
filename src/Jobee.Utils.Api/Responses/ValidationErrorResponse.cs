using Jobee.Utils.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Jobee.Utils.Api.Responses;

public class ValidationErrorResponse : ProblemDetails, IErrorResponse
{
    public ValidationErrorResponse(IReadOnlyList<MemberError> errors)
    {
        Status = 400;
        Title = "One or more validation errors occurred";
        Detail = errors.Count > 1 ? "One or more validation errors occurred" : errors[0].Message;
        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
        Timestamp = TimeProvider.System.GetUtcNow();
        Errors = errors;
    }
    
    public required string TraceId { get; init; }

    public DateTimeOffset Timestamp { get; init; }

    public IReadOnlyList<MemberError> Errors { get; init; }
}
