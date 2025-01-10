namespace CatalogService.Web.RestModels;

public class RestResponse<T>
{
    public T Response { get; set; }
    public ResourceLinkCollection Links { get; set; }
}
