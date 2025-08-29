using Jobee.Utils.Contracts;

namespace Jobee.Utils.Api.Validation;

public class DefaultErrorCodeMapper : IErrorCodeMapper
{
    public virtual bool TryMap(string errorCode, out string? memberCode)
    {
        memberCode = errorCode switch
        {
            "NotNullValidator" or "NotEmptyValidator" => ErrorCodes.Required,
            "GreaterThanOrEqualValidator" => ErrorCodes.GreaterThanOrEqual,
            "GreaterThanValidator" => ErrorCodes.GreaterThan,
            "LessThanOrEqualValidator" => ErrorCodes.LessThanOrEqual,
            "LessThanValidator" => ErrorCodes.LessThan,
            "EmailAddress" => ErrorCodes.InvalidEmail,
            "RegularExpressionValidator" => ErrorCodes.InvalidFormat,
            _ => null
        };
        
        return memberCode is not null;
    }
}