namespace Jobee.Utils.Api.Validation;

public interface IErrorCodeMapper
{
    bool TryMap(string errorCode, out string? memberCode);
}