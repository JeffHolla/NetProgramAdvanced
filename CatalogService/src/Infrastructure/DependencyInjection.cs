using CatalogService.Application.Common.Interfaces.Database;
using CatalogService.Application.Common.Interfaces.Messaging;
using CatalogService.Application.Common.Interfaces.Services;
using CatalogService.Infrastructure.Data;
using CatalogService.Infrastructure.Messaging;
using CatalogService.Infrastructure.Services.CartService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace CatalogService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        //var connectionString = configuration.GetConnectionString("DefaultConnection");
        //services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddScoped<IReadOnlyApplicationDbContext, ReadOnlyApplicationDbContext>();

        services.AddSingleton(TimeProvider.System);

        services.AddScoped<IQueueClient, RabbitQueue>();

        services.AddScoped<ICartClientService, CartClientService>();
        services.Configure<CartQueueOptions>(configuration.GetSection("CartQueue"));

        // Authentication, Authorization
        const string oidcScheme = OpenIdConnectDefaults.AuthenticationScheme;
        services.AddAuthentication(oidcScheme)
                .AddKeycloakOpenIdConnect("keycloak", realm: "master", oidcScheme, options =>
                {
                    options.ClientId = "catalogservice";
                    options.ResponseType = OpenIdConnectResponseType.Code;
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.GetClaimsFromUserInfoEndpoint = true;

                    options.MapInboundClaims = false; // Don't rename claim types (to Microsoft type names)

                    options.ClaimActions.MapJsonKey("role", "role", "role");    //rename the role claim to get this claim from IdP
                    options.TokenValidationParameters.NameClaimType = "name";   //rename the name claim to get this claim from IdP
                    options.TokenValidationParameters.RoleClaimType = "role";   //rename the role claim to get this claim from IdP

                    options.SaveTokens = true;

                    options.RequireHttpsMetadata = false; // Disable HTTPS, need to be enabled in the production
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

        services.AddAuthorization();

        return services;
    }
}
