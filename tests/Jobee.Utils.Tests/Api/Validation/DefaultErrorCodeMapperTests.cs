using AwesomeAssertions;
using FluentValidation.Validators;
using Jobee.Utils.Api.Validation;

namespace Jobee.Utils.Tests.Api.Validation;

public class DefaultErrorCodeMapperTests
{
    private readonly DefaultErrorCodeMapper _errorCodeMapper = new();
    
    [Theory]
    [MemberData(nameof(ErrorCodes))]
    public void ShouldMapKnownErrorCodes(string errorCode, string expectedErrorCode)
    {
        // Act
        var result = _errorCodeMapper.TryMap(errorCode, out var memberCode);
        
        // Assert
        result.Should().BeTrue();
        memberCode.Should().Be(expectedErrorCode);
    }

    public static IEnumerable<object[]> ErrorCodes()
    {
        yield return [new GreaterThanOrEqualValidator<object, int>(0).Name, "GREATER_THAN_OR_EQUAL"];
        yield return [new GreaterThanValidator<object, int>(0).Name, "GREATER_THAN"];
        yield return [new LessThanOrEqualValidator<object, int>(0).Name, "LESS_THAN_OR_EQUAL"];
        yield return [new LessThanValidator<object, int>(0).Name, "LESS_THAN"];
        yield return [new RegularExpressionValidator<string>("").Name, "INVALID_FORMAT"];
        yield return [new NotNullValidator<object, string>().Name, "REQUIRED"];
        yield return [new NotEmptyValidator<object, string>().Name, "REQUIRED"];
    }
}