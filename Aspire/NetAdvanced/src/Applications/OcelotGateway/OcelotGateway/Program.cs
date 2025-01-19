using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using OcelotGateway.DefinedAggregators;

namespace OcelotGateway;

public class Program
{
    public static void Main(string[] args)
    {
        // Swagger integration !
        // https://ocelot.readthedocs.io/en/latest/introduction/notsupported.html#swagger

        new WebHostBuilder()
            .UseKestrel()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config
                    .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                    .AddJsonFile("appsettings.json", true, true)
                    .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                    .AddJsonFile("ocelot.json")
                    .AddEnvironmentVariables();
            })
            .ConfigureServices(services =>
            {
                services.AddOcelot()
                        .AddSingletonDefinedAggregator<CartCategoriesAggregator>();
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddDebug();
                logging.AddConsole();
            })
            .UseIISIntegration()
            .Configure(app =>
            {
                app.UseOcelot().Wait();
            })
            .Build()
            .Run();
    }
}
