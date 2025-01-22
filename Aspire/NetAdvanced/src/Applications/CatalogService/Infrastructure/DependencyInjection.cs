using CatalogService.Application.Common.Interfaces.Database;
using CatalogService.Application.Common.Interfaces.Messaging;
using CatalogService.Application.Common.Interfaces.Services;
using CatalogService.Infrastructure.Data;
using CatalogService.Infrastructure.Messaging;
using CatalogService.Infrastructure.Services.CartService;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddScoped<IReadOnlyApplicationDbContext, ReadOnlyApplicationDbContext>();

        services.AddSingleton(TimeProvider.System);

        services.AddScoped<IQueueClient, RabbitQueue>();

        services.AddScoped<ICartClientService, CartClientService>();
        services.Configure<CartQueueOptions>(configuration.GetSection("CartQueue"));

        // Authentication, Authorization
        // https://nikiforovall.github.io/keycloak-authorization-services-dotnet/examples/auth-getting-started.html
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddKeycloakWebApi(
                    options =>
                    {
                        options.Resource = "catalogservice";
                        options.Realm = "master";
                        options.AuthServerUrl = "http://localhost:8080/";
                        options.VerifyTokenAudience = false;

                        options.RoleClaimType = KeycloakConstants.RoleClaimType;
                    },
                    options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.Audience = "catalogservice";
                        options.TokenValidationParameters.RequireExpirationTime = false;
                    });

        services.AddAuthorization()
                .AddKeycloakAuthorization(options =>
                {
                    options.EnableRolesMapping = RolesClaimTransformationSource.All;

                    // Note, this should correspond to role configured with KeycloakAuthenticationOptions
                    options.RoleClaimType = KeycloakConstants.RoleClaimType;

                    options.RolesResource = "catalogservice";
                });

        return services;
    }
}
