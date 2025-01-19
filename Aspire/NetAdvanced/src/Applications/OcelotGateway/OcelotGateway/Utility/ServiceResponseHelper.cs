using Ocelot.Middleware;

namespace OcelotGateway.Utility;

public class ServiceResponseHelper
{
    public static async Task<ServiceData> GetServiceDataAsync(List<HttpContext> httpContexts, string serviceName)
    {
        //var serviceContext = httpContexts.Find(context => IsContextServiceEqualsService(context, serviceName));
        var serviceContext = httpContexts.Find(FindServiceByMetadata);
        var serviceResponse = serviceContext.Items.DownstreamResponse();

        var serviceData = new ServiceData()
        {
            Content = await serviceResponse.Content.ReadAsStringAsync(),
            Headers = serviceResponse.Headers
        };
        return serviceData;

        bool FindServiceByMetadata(HttpContext context)
        {
            var metadataServiceName = context.Items.DownstreamRoute().MetadataOptions.Metadata["ServiceName"];
            return metadataServiceName == serviceName;
        }
    }
}

public class ServiceData
{
    public string Content { get; set; }
    public List<Header> Headers { get; set; }
}