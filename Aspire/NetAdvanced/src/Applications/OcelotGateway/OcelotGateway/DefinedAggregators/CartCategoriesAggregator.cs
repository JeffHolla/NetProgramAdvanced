using System.Net;
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using OcelotGateway.Common;
using OcelotGateway.Utility;

namespace OcelotGateway.DefinedAggregators;

public class CartCategoriesAggregator : IDefinedAggregator
{
    public async Task<DownstreamResponse> Aggregate(List<HttpContext> responseHttpContexts)
    {
        var responseViewModel = await GetResponseViewModelAsync(responseHttpContexts);
        var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(responseViewModel)
        };
        return new DownstreamResponse(httpResponse);
    }

    private static async Task<CartCategoriesViewModel> GetResponseViewModelAsync(List<HttpContext> responseHttpContexts)
    {
        var responseViewModel = new CartCategoriesViewModel();

        var downstreamRoutes = responseHttpContexts.Select(httpContext => httpContext.Items.DownstreamRoute());
        foreach (var route in downstreamRoutes)
        {
            route.MetadataOptions.Metadata.TryGetValue("ServiceName", out var serviceName);
            switch (serviceName)
            {
                case ServiceNames.CartService:
                    var cartServiceData = await ServiceResponseHelper.GetServiceDataAsync(responseHttpContexts, ServiceNames.CartService);

                    responseViewModel.CartServiceResponseContent = cartServiceData.Content;
                    responseViewModel.CartServiceResponseHeaders = cartServiceData.Headers;
                    break;

                case ServiceNames.CatalogService:
                    var catalogServiceData = await ServiceResponseHelper.GetServiceDataAsync(responseHttpContexts, ServiceNames.CatalogService);

                    responseViewModel.CatalogServiceResponseContent = catalogServiceData.Content;
                    responseViewModel.CatalogServiceResponseHeaders = catalogServiceData.Headers;
                    break;

                default:
                    throw new Exception($"Service name was not found in '{route.Key}' route");
            }
        }

        return responseViewModel;
    }
}

public class CartCategoriesViewModel
{
    public string CartServiceResponseContent { get; set; }
    public List<Header> CartServiceResponseHeaders { get; set; }

    public string CatalogServiceResponseContent { get; set; }
    public List<Header> CatalogServiceResponseHeaders { get; set; }
}
