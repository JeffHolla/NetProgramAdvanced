using Ocelot.Cache.CacheManager;
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
            .ConfigureServices((hostBuilder, services) =>
            {
                // Ocelot
                services.AddOcelot()
                        .AddSingletonDefinedAggregator<CartCategoriesAggregator>()
                        .AddCacheManager(options => options.WithDictionaryHandle());

                // Swagger
                services.AddSwaggerForOcelot(
                    hostBuilder.Configuration,
                    options =>
                    {
                        options.GenerateDocsForAggregates = false;
                        options.GenerateDocsForGatewayItSelf = true;
                    });

                services.AddControllers();
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddDebug();
                logging.AddConsole();
            })
            .UseIISIntegration()
            .Configure(app =>
            {
                app.UseRouting();

                // For Gateway itself
                app.UseSwagger();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });

                // https://github.com/Burgyn/MMLib.SwaggerForOcelot
                app.UseSwaggerForOcelotUI(opt =>
                {
                    opt.PathToSwaggerGenerator = "/swagger/docs";
                });

                app.UseOcelot().Wait();
            })
            .Build()
            .Run();
    }
}
