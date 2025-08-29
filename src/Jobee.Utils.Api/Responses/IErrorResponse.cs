namespace Jobee.Utils.Api.Responses;

public interface IErrorResponse
{
    string TraceId { get; }
    
    DateTimeOffset Timestamp { get; }
}