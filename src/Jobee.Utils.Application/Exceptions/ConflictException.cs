namespace Jobee.Utils.Application.Exceptions;

public class ConflictException : Exception
{
    public ConflictException(string? message, string? target) : base(message)
    {
        Target = target;
    }

    public string? Target { get; }
}