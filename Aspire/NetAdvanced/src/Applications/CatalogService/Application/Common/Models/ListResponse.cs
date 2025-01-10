namespace CatalogService.Application.Common.Models;

public class ListResponse<TResult>
{
    public IEnumerable<TResult> Results { get; set; }

    public ListResponse()
    {
        Results = [];
    }

    public ListResponse(IEnumerable<TResult> results)
    {
        Results = results;
    }
}
