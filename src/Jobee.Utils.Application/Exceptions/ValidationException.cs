using Jobee.Utils.Contracts;

namespace Jobee.Utils.Application.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(string message, IReadOnlyList<MemberError> errors) : base(message) => Errors = errors;

    public IReadOnlyList<MemberError> Errors { get; }
}