namespace Jobee.Utils.Contracts.Responses;

public record CreatedResponse<T>
{
    public required T Id { get; init; }
}