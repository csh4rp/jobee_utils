namespace Jobee.Utils.Api.Validation;

public interface IValidationArgumentParser
{
     IReadOnlyList<string>? Parse(string errorCode, Dictionary<string, object> values);
}