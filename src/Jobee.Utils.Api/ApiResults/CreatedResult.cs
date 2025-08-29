using Jobee.Utils.Contracts.Responses;

namespace Jobee.Utils.Api.ApiResults;

public abstract class CreatedAtResult
{
    public static CreatedAtResult<T> From<T>(CreatedResponse<T> response) => new(response);
}

public class CreatedAtResult<T> : IResult
{
    private readonly CreatedResponse<T> _response;

    public CreatedAtResult(CreatedResponse<T> response) => _response = response;

    public Task ExecuteAsync(HttpContext httpContext)
    {
        var url = httpContext.Request.Path.Add(new PathString($"/{_response.Id}"));

        httpContext.Response.Headers.Append("Location", url.ToString());
        httpContext.Response.StatusCode = StatusCodes.Status201Created;
        return httpContext.Response.WriteAsJsonAsync(_response);
    }
}
