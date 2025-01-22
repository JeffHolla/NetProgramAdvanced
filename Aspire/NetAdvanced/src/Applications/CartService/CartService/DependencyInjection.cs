using System.Reflection;
using Asp.Versioning;
using CartService.BLL.CartLogic;
using CartService.Common.Entities;
using CartService.Common.Messaging;
using CartService.DAL.Repositories;
using CartService.DAL.Repositories.Common;
using CartService.PL.WebAPI;
using CatalogService.Infrastructure.Services.CartService;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CartService
{
    internal static class DependencyInjection
    {
        internal static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRepository<Cart>, CartRepository>();
            services.AddScoped<ICartLogicHandler, CartLogicHandler>();
            services.AddScoped<ICartEventHandler, CartEventHandler>();

            services.AddSingleton<IDbConnectionProvider, DbLiteConnectionProvider>(_ =>
                    new DbLiteConnectionProvider() { ConnectionString = configuration.GetConnectionString("DbLite") });

            // Since we don't have a "scope" to retrieve messages, maybe we should use a Singleton?
            // Another option is to split the receiving and sending of messages into two separate entities.
            // And another option is to split Queue on Top Level Handlers and subscribers with IQueueClient
            services.AddSingleton<IQueueClient, RabbitQueue>();
            services.Configure<CartQueueOptions>(configuration.GetSection("CartQueue"));
        }

        internal static void AddVersioning(this IServiceCollection services)
        {
            // https://www.milanjovanovic.tech/blog/api-versioning-in-aspnetcore
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddMvc() // ← bring in MVC (Core); not required for Minimal APIs, but required for controllers
            .AddApiExplorer(opt =>
            {
                // format the version as "'v'major[.minor][-status]"
                opt.GroupNameFormat = "'v'V";
                opt.SubstituteApiVersionInUrl = true;
            });
        }

        internal static void AddSwagger(this IServiceCollection services)
        {
            //https://github.com/dotnet/aspnet-api-versioning/wiki/API-Documentation#aspnet-core
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            services.AddEndpointsApiExplorer();
        }

        internal static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // https://nikiforovall.github.io/keycloak-authorization-services-dotnet/examples/auth-getting-started.html
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddKeycloakWebApi(
                         options =>
                         {
                             options.Resource = "cartservice";
                             options.Realm = "master";
                             options.AuthServerUrl = "http://localhost:8080/";
                             options.VerifyTokenAudience = false;

                             options.RoleClaimType = KeycloakConstants.RoleClaimType;
                         },
                        options =>
                        {
                            options.RequireHttpsMetadata = false;
                            options.Audience = "cartservice";
                        });
        }

        internal static void ConfigureAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            // https://nikiforovall.github.io/keycloak-authorization-services-dotnet/examples/auth-getting-started.html
            services.AddAuthorization()
                    .AddKeycloakAuthorization(options =>
                    {
                        options.EnableRolesMapping = RolesClaimTransformationSource.All;

                        // Note, this should correspond to role configured with KeycloakAuthenticationOptions
                        options.RoleClaimType = KeycloakConstants.RoleClaimType;

                        options.RolesResource = "cartservice";
                    });
        }
    }
}
