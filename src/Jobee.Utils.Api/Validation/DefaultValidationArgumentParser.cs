namespace Jobee.Utils.Api.Validation;

public class DefaultValidationArgumentParser : IValidationArgumentParser
{
    public virtual IReadOnlyList<string>? Parse(string errorCode, Dictionary<string, object> values)
    {
        switch (errorCode)
        {
            case "GreaterThanValidator":
            case "GreaterThanOrEqualValidator":
            case "LessThanValidator":
            case "LessThanOrEqualValidator":
                return [values["ComparisonValue"].ToString()!];
        }

        return null;
    }
}