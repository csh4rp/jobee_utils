using System.Diagnostics;
using FluentValidation;
using Jobee.Utils.Api.Responses;
using Jobee.Utils.Contracts;

namespace Jobee.Utils.Api.Validation;

public class ValidationEndpointFilter<TModel> : IEndpointFilter
{
    private readonly IValidator<TModel> _validator;
    private readonly IErrorCodeMapper _errorCodeMapper;
    private readonly IValidationArgumentParser _validationArgumentParser;
    private readonly TimeProvider _timeProvider;
    private readonly ILogger<ValidationEndpointFilter<TModel>> _logger;

    public ValidationEndpointFilter(IValidator<TModel> validator,
        IErrorCodeMapper errorCodeMapper,
        IValidationArgumentParser validationArgumentParser,
        TimeProvider timeProvider,
        ILogger<ValidationEndpointFilter<TModel>> logger)
    {
        _validator = validator;
        _errorCodeMapper = errorCodeMapper;
        _validationArgumentParser = validationArgumentParser;
        _timeProvider = timeProvider;
        _logger = logger;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var index = GetArgumentIndex(context.Arguments);
        if (index == -1)
        {
            throw new InvalidOperationException($"Could not find argument of type: {typeof(TModel)}");
        }

        var argument = context.GetArgument<TModel>(index);

        var validationResult = await _validator.ValidateAsync(argument, context.HttpContext.RequestAborted);
        if (validationResult.IsValid)
        {
            return await next(context);
        }

        _logger.LogTrace("Validation failed for: '{Path}' for model: '{Model}'",
            context.HttpContext.Request.Path,
            typeof(TModel).FullName);

        var errors = validationResult.Errors.Select(e =>
        {
            var codeMapped = _errorCodeMapper.TryMap(e.ErrorCode, out var errorCode);
            var code = codeMapped ? errorCode! : e.ErrorCode;
            var target = char.ToLower(e.PropertyName[0]) + e.PropertyName[1..];
            var args = _validationArgumentParser.Parse(e.ErrorCode, e.FormattedMessagePlaceholderValues);
            
            return new MemberError(code, e.ErrorMessage, target, args);
        }).ToList();

        var currentActivity = Activity.Current;
        var traceId = currentActivity?.TraceId.ToString() ?? context.HttpContext.TraceIdentifier;
        var response = new ValidationErrorResponse(errors)
        {
            Instance = context.HttpContext.Request.Path,
            TraceId = traceId,
            Timestamp = _timeProvider.GetUtcNow(),
        };

        return Results.BadRequest(response);
    }

    private static int GetArgumentIndex(IList<object?> arguments)
    {
        var index = 0;
        while (index < arguments.Count)
        {
            if (arguments[index] is TModel)
            {
                return index;
            }

            index++;
        }

        return -1;
    }
}