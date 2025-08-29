using AwesomeAssertions;
using FluentValidation;
using FluentValidation.Validators;
using Jobee.Utils.Api.Validation;

namespace Jobee.Utils.Tests.Api.Validation;

public class DefaultValidationArgumentParserTests
{
    private readonly TestClass _testClass = new();
    private readonly DefaultValidationArgumentParser _parser = new();

    [Fact]
    public void ShouldParseArguments_WhenUsingGreaterThanValidator()
    {
        // Arrange
        var validator = new GreaterThanValidator<TestClass, int>(1);
        var context = new ValidationContext<TestClass>(_testClass);

        var isValid = validator.IsValid(context, _testClass.Amount);

        // Act
        var args = _parser.Parse(validator.Name, context.MessageFormatter.PlaceholderValues);

        // Assert
        isValid.Should().BeFalse();
        args.Should().NotBeNull();
        args[0].Should().Be("1");
    }

    [Fact]
    public void ShouldParseArguments_WhenUsingGreaterThanOrEqualValidator()
    {
        // Arrange
        var validator = new GreaterThanOrEqualValidator<TestClass, int>(1);
        var context = new ValidationContext<TestClass>(_testClass);

        var isValid = validator.IsValid(context, _testClass.Amount);

        // Act
        var args = _parser.Parse(validator.Name, context.MessageFormatter.PlaceholderValues);

        // Assert
        isValid.Should().BeFalse();
        args.Should().NotBeNull();
        args[0].Should().Be("1");
    }

    [Fact]
    public void ShouldParseArguments_WhenUsingLessThanValidator()
    {
        // Arrange
        var validator = new LessThanValidator<TestClass, int>(-1);
        var context = new ValidationContext<TestClass>(_testClass);

        var isValid = validator.IsValid(context, _testClass.Amount);

        // Act
        var args = _parser.Parse(validator.Name, context.MessageFormatter.PlaceholderValues);

        // Assert
        isValid.Should().BeFalse();
        args.Should().NotBeNull();
        args[0].Should().Be("-1");
    }

    [Fact]
    public void ShouldParseArguments_WhenUsingLessThanOrEqualValidator()
    {
        // Arrange
        var validator = new LessThanOrEqualValidator<TestClass, int>(-1);
        var context = new ValidationContext<TestClass>(_testClass);

        var isValid = validator.IsValid(context, _testClass.Amount);

        // Act
        var args = _parser.Parse(validator.Name, context.MessageFormatter.PlaceholderValues);

        // Assert
        isValid.Should().BeFalse();
        args.Should().NotBeNull();
        args[0].Should().Be("-1");
    }

    private class TestClass
    {
        public int Amount { get; set; }
    }
}