namespace Jobee.Utils.Contracts;

public record MemberError
{
    public MemberError(string code, string message, string target, IReadOnlyList<string>? args = null)
    {
        Target = target;
        Code = code;
        Message = message;
        Args = args;
    }

    public static MemberError InvalidValue(string target, IReadOnlyList<string> args) =>
        new(ErrorCodes.InvalidFormat, "Value is invalid", target, args);

    public static MemberError OutOfRange(string target, IReadOnlyList<string> args) =>
        new(ErrorCodes.OutOfRange, "Value is out of range", target, args);

    public static MemberError Conflict(string target, string? message = null) =>
        new(ErrorCodes.Conflict, message ?? "Conflict", target);
    
    public string Target { get; }

    public string Message { get; }
    
    public string Code { get; }
    
    public IReadOnlyList<string>? Args { get; }
}