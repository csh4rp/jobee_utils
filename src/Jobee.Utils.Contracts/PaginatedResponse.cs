using System.Collections;

namespace Jobee.Utils.Contracts;

public record PaginatedResponse<T> : IEnumerable<T>
{
    public required IReadOnlyList<T> Items { get; init; } 

    public required long TotalCount { get; init; }
    
    public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}