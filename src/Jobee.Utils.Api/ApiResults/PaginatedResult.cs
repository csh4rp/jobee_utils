using Jobee.Utils.Contracts;

namespace Jobee.Utils.Api.ApiResults;

public abstract class PaginatedResult
{
    public static PaginatedResult<T> From<T>(IReadOnlyCollection<T> items, long totalCount) => new(items, totalCount);

    public static PaginatedResult<T> From<T>(PaginatedResponse<T> response) => new(response.Items, response.TotalCount);
}

public class PaginatedResult<T> : IResult
{
    private readonly IReadOnlyCollection<T> _items;
    private readonly long _totalCount;

    public PaginatedResult(IReadOnlyCollection<T> items, long totalCount)
    {
        _items = items;
        _totalCount = totalCount;
    }

    public Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.Response.Headers.Append("X-Total-Count", _totalCount.ToString());
        return httpContext.Response.WriteAsJsonAsync(_items);
    }
}